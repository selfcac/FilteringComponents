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

class ScriptConfig:
    #DLLs:
    CSProtectProcessPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\ProcessTerminationProtection\bin\Debug\ProcessTerminationProtection.dll"
    CSNetworkHelper = r"C:\Users\Yoni\Desktop\selfcac\PortsOwners\PortOwnersDLL\bin\x86\Debug\PortOwnersDLL.dll"

    # Global objects:

def log(tag, text):
    log = "[" + str(datetime.datetime.now()) +"] " + tag + "\t" + text;
    print(log)

def protectProcess():
    pprotectDLL = LoadLibrary(ScriptConfig.CSProtectProcessPath,'net')
    pprotectDLL._lib.ProcessTerminationProtection.ProcessProtect.ProtectCurrentFromUsers();

protectProcess();
log("main","started!");