######################################################################################

# 1) Protect process 

# 2) drop UDP 80,443 (Tor)

# 3) divert outbound tcp & port != http(S)\socks & payload > 7

#   if conn is user:
#       if has proxy http\s and not going to localhost (such has fiddler)
#               or going to anywhere and it's SOCKS proxy
#           drop;
#   o.w. Reinjet

######################################################################################

# System
import os, sys
import argparse
import typing
import json , datetime

# C# Load
from msl.loadlib import LoadLibrary
import clr
import pydivert

import threading

class ScriptConfig:
    #DLLs:
    CSProtectProcessPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\ProcessTerminationProtection\bin\Debug\ProcessTerminationProtection.dll"
    CSNetworkHelper = r"C:\Users\Yoni\Desktop\selfcac\PortsOwners\PortOwnersDLL\bin\x86\Debug\PortOwnersDLL.dll"

    # Global objects:
    netHelper = None
    proxyDetectHelper = None

class SafeAccessDivert(object):
    def __init__(self, divertObj):
        self.lock = threading.Lock()
        self.value =  divertObj

    def getNextPacket(self):
        self.lock.acquire()
        try:
            # Read next packet:
            return self.value.recv();
        finally:
            self.lock.release()

    def reinjectPacket(self, packet):
        self.value.send(packet);

def log( text):
    log = "[" + str(datetime.datetime.now()) +"] "  + text;
    print(log)

def protectProcess():
    pprotectDLL = LoadLibrary(ScriptConfig.CSProtectProcessPath,'net')
    pprotectDLL._lib.ProcessTerminationProtection.ProcessProtect.ProtectCurrentFromUsers();

def getAddressString(packet):
    if packet.tcp != None:
        return "[TCP] " + packet.src_addr + ":" + str(packet.src_port) + "->" + packet.dst_addr + ":" + str(packet.dst_port);
    else:
        return "[UDP] "+ packet.src_addr + ":" + str(packet.src_port) + "->" + packet.dst_addr + ":" + str(packet.dst_port);

def logDrop(packet):
    global log, getAddressString
    text = "Dropping " + getAddressString(packet);    
    log(text);

def dropHTTPUdp(state):
    global logDrop;
    with pydivert.WinDivert("outbound and udp and (udp.DstPort == 80 || udp.DstPort == 443)") as w:
        for packet in w:
            logDrop(packet);
            # Drop so no w.send(packet)

def windivertWorker(safeDivert):
    while True:
        packet = safeDivert.getNextPacket();
        handlePacket(packet, safeDivert);

def isPacketSOCKS(packet):
    return ScriptConfig.proxyDetectHelper.IsSocksProxyConnect(packet.payload)

def isPacketHTTP(packet):
    return ScriptConfig.proxyDetectHelper.IsHttpsConnect(packet.payload)

def handlePacket(packet, safeDivert):
    global logDrop, isPacketHTTP, isPacketSOCKS, getAddressString ;
    conn_addr =  packet.src_addr + ":" + str(packet.src_port);
    isAdmin = ScriptConfig.netHelper.isLocalAddressAdmin(conn_addr, False); # False- new connection need to be checked
    isSocks = isPacketSOCKS(packet);
    isHTTPS = isPacketHTTP(packet);

    isBlocked = False;

    if not isAdmin:
        if isSocks:
            isBlocked =True; # Anyway cause chrome->tor happens on loopback in socks
        elif isHTTPS:
            # Ignore looped (internal https proxies)
            # Because, http proxy do not query data from internet and is not encrypted 
            #       so we will stop the proxy itself like all others.
            if not packet.dst_addr == "127.0.0.1": 
                isBlocked =True;
    
    if not isBlocked:
        #debug data:
        #if packet.dst_addr == "127.0.0.1": 
        #    print("Admin? " + str(isAdmin) + " HTTPS? " + str(isHTTPS) + " " + getAddressString(packet) )
        safeDivert.reinjectPacket(packet);
    else:
        logDrop(packet);


log("Python version: " + sys.version)
log("Exe path: " + sys.executable)
log("Command: " + " ".join(sys.argv))

try:
    log("Block proxy Started!");

    # Stop process kill unless you admin:
    log("Removing kill permission...");
    protectProcess();

    netHelperDLL = LoadLibrary(ScriptConfig.CSNetworkHelper,'net')

    log("Starting proxy helper")
    ScriptConfig.proxyDetectHelper = netHelperDLL._lib.PortsOwners.ProxyDetection();

    log("Starting network watcher")
    ScriptConfig.netHelper = netHelperDLL._lib.PortsOwners.NetworkWatcher();
    ScriptConfig.netHelper.Start(5); # Update ip tables every 5 sec

    # Dropping  QUIC ProtoolFilter (UDP)
    # run as adeamon so it will close when program ends.
    udpThread = threading.Thread(target=dropHTTPUdp, args=(0,), daemon=True);
    udpThread.start();

    # Processing all other outbound ports (both SOCKS and HTTPs can be identified in the first 8 bytes)
    maindivert = pydivert.WinDivert("outbound and tcp and tcp.DstPort != 80 and tcp.DstPort != 443 and tcp.PayloadLength > 7")
    try:
        log("Starting main divert");
        maindivert.open();
        safeDivert = SafeAccessDivert(maindivert);

        my_threads = [];

        # Give the thread-safe object to 3 threads to handle it:
        log("Starting 3 handler for tcp packets")
        for i in range(0,3):
            t = threading.Thread(target=windivertWorker, args=(safeDivert,), daemon=False);
            my_threads.append(t);
            t.start();

        for i in range(0, len(my_threads)):
            my_threads[i].join();
        
        log("Existing....")
    except Exception as ex:
        log("Error diverting: " + str(ex))
    finally:
        ScriptConfig.netHelper.Stop();
        maindivert.close();

except Exception as ex:
    log("Problem in main program: " + str(ex))