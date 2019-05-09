######################################################################################

# 1) Protect process 

# 2) drop UDP 80,443 (Tor)

# 3) divert outbound tcp & port != http(S) & payload > 10

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

import threading

class ScriptConfig:
    #DLLs:
    CSProtectProcessPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\ProcessTerminationProtection\bin\Debug\ProcessTerminationProtection.dll"
    CSNetworkHelper = r"C:\Users\Yoni\Desktop\selfcac\PortsOwners\PortOwnersDLL\bin\x86\Debug\PortOwnersDLL.dll"

    # Global objects:
    netHelper = None

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

def log( text):
    log = "[" + str(datetime.datetime.now()) +"] "  + text;
    print(log)

def protectProcess():
    pprotectDLL = LoadLibrary(ScriptConfig.CSProtectProcessPath,'net')
    pprotectDLL._lib.ProcessTerminationProtection.ProcessProtect.ProtectCurrentFromUsers();

def logDrop(packet):
    global log;
    text = "Dropping "
    if packet['tcp'] != None:
        text += "[TCP] " + packet["src_addr"] + ":" + str(packet["src_port"]) + "->" + packet["dst_addr"] + ":" + str(packet["dst_port"]);
    else:
        text += "[UDP] "+ packet["src_addr"] + ":" + str(packet["src_port"]) + "->" + packet["dst_addr"] + ":" + str(packet["dst_port"]);
    log(text);

def dropHTTPUdp():
    global logDrop;
    with pydivert.WinDivert("outbound and udp and (udp.DstPort == 80 || udp.DstPort == 443)") as w:
        for packet in w:
            logDrop(packet);
            # Drop so no w.send(packet)

def windivertWorker(safeDivert):
    while True:
        packet = safeDivert.getNextPacket();

def handlePacket(packet):
    

_log("Python version: " + sys.version)
_log("Exe path: " + sys.executable)
_log("Command: " + " ".join(sys.argv))

try:
    log("Block proxy Started!");

    # Stop process kill unless you admin:
    log("Removing kill permission...");
    protectProcess();

    log("Starting network watcher")
    netHelperDLL = LoadLibrary(ScriptConfig.CSNetworkHelper,'net')
    ScriptConfig.netHelper = netHelperDLL._lib.PortsOwners.NetworkWatcher();
    ScriptConfig.netHelper.Start(5); # Update ip tables every 5 sec

    # Dropping  QUIC ProtoolFilter (UDP)
    # run as adeamon so it will close when program ends.
    udpThread = threading.Thread(target=dropHTTPUdp, args=Nonem, daemon=True);
    udpThread.start();

    # Processing all other outbound ports
    maindivert = pydivert.WinDivert("outbound and tcp and tcp.PayloadLength > 0")
    try:
        log("Statring main divert");
        maindivert.open();
        safeDivert = SafeAccessDivert(maindivert);

        my_threads = [];

        # Give the thread-safe object to 3 threads to handle it:
        log("Starting 3 handler for tcp packets")
        for i in range(0,3):
            t = threading.Thread(target=windivertWorker, args=safeDivert, daemon=False);
            my_threads.append(t);
            t.start();

        for i in range(0, len(my_threads)):
            my_threads[i].join();
        
        log("Existing....")
    except Exception as ex:
        _log("Error diverting: " + str(ex))
    finally:
        ScriptConfig.netHelper.Stop();
        maindivert.close();

except Exception as ex:
    _log("Problem in main program: " + str(ex))