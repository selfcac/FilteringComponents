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
import json , datetime, time

# C# Load
import clr
import pydivert

import threading
import traceback

class ScriptConfig:
    # DLLs:
    # C# DLL:
    AllCSharpDLLs = [   
        (
            "TimeBlockFilter",
            r"C:\Users\Yoni\Desktop\selfcac\FilteringComponentsStandard\TimeBlockFilter\bin\Debug\netstandard2.0\TimeBlockFilter.dll"
        ),
        (
            "ProcessTerminationProtection",
            r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\ProcessTerminationProtection\bin\Debug\ProcessTerminationProtection.dll"
        ),
        (   
            "PortOwnersDLL",  
            r"C:\Users\Yoni\Desktop\selfcac\PortsOwners\PortOwnersDLL\bin\x86\Debug\PortOwnersDLL.dll" 
        )
    ] 

    # Policies:
    TimePolicyPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\MitmprxyPlugin\timeblock.v2.json"

    # Windivert filters:
    catchTcp = "outbound and tcp and tcp.DstPort != 80 and tcp.DstPort != 443 and tcp.DstPort != 445 and tcp.SrcPort != 445 and tcp.PayloadLength > 7"
    dropProblematic = "outbound and ((udp and (udp.DstPort == 80 || udp.DstPort == 443)) || (tcp and tcp.DstPort=3389))"

    # Global objects:
    TimeBlockObj = None;
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
        self.value.send(packet, recalculate_checksum=True);

__print__ = print;
printLock = threading.Lock();

def loadAssemblies(pathList):
    for t in pathList:
        path = t[1];
        if not os.path.exists(path) or not os.path.isfile(path):
            print("Can't find file: '" + path + "'");
            continue
        split = path.rsplit('\\',1);
        directoryName = split[0];
        fileName = split[1];

        # Add Directory to search dependent assemblies:
        sys.path.append(directoryName)

        # Add Assemblie
        clr.FindAssembly(path)
        clr.AddReference(t[0])

        print("Loaded " + t[0] + " successfully");
    print("Done loading CLR (.NET) Assemblies");

def readAllText(path, read_encoding="utf-8"):
    result = "";
    with open(path,'r',encoding=read_encoding) as file:
        result = file.read()
    return result;

def protectProcess():
    from ProcessTerminationProtection import ProcessProtect
    ProcessProtect.ProtectCurrentFromUsers();
    print("Protected process from users");

def printTimeVersion():
    from TimeBlockFilter import GitInfo
    print("Time filter version: \n"+ "\n".join(GitInfo.AllGitInfo()))

def log( text):
    global __print__, printLock;
    llog = "[" + str(datetime.datetime.now()) +"] "  + text ;
    with printLock:
        __print__(llog)

def logPacket(packet):
    log(getAddressString(packet))

def getAddressString(packet):
    if packet.tcp != None:
        return "[TCP] size: " + str(len(packet.payload)) + ", " + packet.src_addr + ":" + str(packet.src_port) + "->" + packet.dst_addr + ":" + str(packet.dst_port);
    else:
        return "[UDP] size: " + str(len(packet.payload)) + ", " + packet.src_addr + ":" + str(packet.src_port) + "->" + packet.dst_addr + ":" + str(packet.dst_port);

def logDrop(packet, tag):
    global log, getAddressString
    text = "[" + str(tag) + "] Dropping " + getAddressString(packet);    
    log(text);

def dropHTTPUdp(state):
    global logDrop;
    with pydivert.WinDivert(ScriptConfig.dropProblematic) as w:
        while keepThreadOpen:
            try:
                for packet in w:
                    logDrop(packet, "UDP-QUICk\RDP");
            except Exception as ex:
                if not packet is None:
                    logPacket(packet);
                log(str(ex));
                traceback.print_tb(ex.__traceback__)

keepThreadOpen = True
def windivertWorker(safeDivert):
    while keepThreadOpen:
        packet = None
        try:
            packet = safeDivert.getNextPacket();
            handlePacket(packet, safeDivert);
        except Exception as ex:
            if not packet is None:
                logPacket(packet);
            log(str(ex));
            traceback.print_tb(ex.__traceback__)

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
    isTimeBlocked = ScriptConfig.TimeBlockObj.isBlockedNow();

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
    
    # Ignore time block when admin or local:
    if isAdmin or packet.dst_addr == "127.0.0.1":
        isTimeBlocked = False;

    if not isBlocked and not isTimeBlocked:

        #debug data:
        #if packet.dst_addr == "127.0.0.1": 
        #    print("Admin? " + str(isAdmin) + " HTTPS? " + str(isHTTPS) + " " + getAddressString(packet) )
        # logPacket(packet);
        safeDivert.reinjectPacket(packet);
    else:
        if isTimeBlocked:
            logDrop(packet, "TIME");
        else:
            logDrop(packet, "PROXY");

log("Python version: " + sys.version)
log("Exe path: " + sys.executable)
log("Command: " + " ".join(sys.argv))


try:
    log("Block proxy Started!");

    # Stop process kill unless you admin:
    log("Removing kill permission...");
    
    loadAssemblies(ScriptConfig.AllCSharpDLLs)
    protectProcess()

    


    from TimeBlockFilter import TimeFilterObject
    ScriptConfig.TimeBlockObj = TimeFilterObject() # (new Class) in python
    time_policy_content = readAllText(ScriptConfig.TimePolicyPath);
    ScriptConfig.TimeBlockObj.reloadPolicy(time_policy_content);

    log("Starting proxy helper")
    from PortsOwners import ProxyDetection
    ScriptConfig.proxyDetectHelper = ProxyDetection();

    log("Starting network watcher")
    from PortsOwners import NetworkWatcher
    ScriptConfig.netHelper = NetworkWatcher();
    ScriptConfig.netHelper.Start(5); # Update ip tables every 5 sec

    # Dropping  QUIC ProtoolFilter (UDP)
    # run as daemon so it will close when program ends.
    udpThread = threading.Thread(target=dropHTTPUdp, args=(0,), daemon=True);
    udpThread.start();

    # Processing all other outbound ports (both SOCKS and HTTPs can be identified in the first 8 bytes)
    maindivert = pydivert.WinDivert(ScriptConfig.catchTcp)
    try:
        log("Starting main divert");
        maindivert.open();
        safeDivert = SafeAccessDivert(maindivert);

        my_threads = [];

        # Give the thread-safe object to 3 threads to handle it:
        log("Starting 3 handler for tcp packets")
        for i in range(0,3):
            t = threading.Thread(target=windivertWorker, args=(safeDivert,), daemon=True);
            my_threads.append(t);
            t.start();

        try:
            #for i in range(0, len(my_threads)):
            #    my_threads[i].join();
            while True:
                time.sleep(10);
                with printLock:
                    sys.stdout.flush()
        except KeyboardInterrupt:
            keepThreadOpen = False
            print("Ctrl-C");
        
        keepThreadOpen = False
        log("Existing....")
    except Exception as ex:
        log("Error diverting: " + str(ex))
    finally:
        keepThreadOpen = False
        ScriptConfig.netHelper.Stop();
        maindivert.close();

except Exception as ex:
    log("Problem in main program: " + str(ex))
