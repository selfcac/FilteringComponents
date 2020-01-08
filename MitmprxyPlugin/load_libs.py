import clr
import sys
import os

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

loadAssemblies( 
    [   
        (   
            "HTTPProtocolFilter",  
            r"C:\Users\Yoni\Desktop\selfcac\FilteringComponentsStandard\HTTPProtocolFilter\bin\Debug\netstandard2.0\HTTPProtocolFilter.dll" 
        ),
        (
            "TimeBlockFilter",
            r"C:\Users\Yoni\Desktop\selfcac\FilteringComponentsStandard\TimeBlockFilter\bin\Debug\netstandard2.0\TimeBlockFilter.dll"
        ),
        (
            "ProcessTerminationProtection",
            r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\ProcessTerminationProtection\bin\Debug\ProcessTerminationProtection.dll"
        )
    ] 
)

def printHTTPVersion():
    from HTTPProtocolFilter import GitInfo
    print("HTTP filter version: "+ GitInfo.GetInfo())

def printTimeVersion():
    from TimeBlockFilter import GitInfo
    print("Time filter version: "+ GitInfo.GetInfo())

def protectProcess():
    from ProcessTerminationProtection import ProcessProtect
    ProcessProtect.ProtectCurrentFromUsers();
    print("Protected process from users");

def readAllText(path, read_encoding="utf-8"):
    result = "";
    with open(path,'r',encoding=read_encoding) as file:
        result = file.read()
    return result;

def getHttpFilterObject(policy_path):
    from HTTPProtocolFilter import FilterPolicy
    result = FilterPolicy() # (new Class) in python
    policy_content = readAllText(policy_path);
    result.reloadPolicy(policy_content);
    return result;


def getTimeFilterObject(policy_path):
    from TimeBlockFilter import TimeFilterObject
    result = TimeFilterObject() # (new Class) in python
    policy_content = readAllText(policy_path);
    result.reloadPolicy(policy_content);
    return result;


protectProcess();
printHTTPVersion();
printTimeVersion();

http_path = r"C:\Users\Yoni\Desktop\selfcac\policy_repo\proxy-level\http.json"
time_path = r"C:\Users\Yoni\Desktop\selfcac\policy_repo\proxy-level\time.json"

httpFilter = getHttpFilterObject(http_path);
time_path = getTimeFilterObject(time_path);
