# Python x86 - pip 3
#python -m pip install --upgrade pip
#pip install msl-loadlib
#pip install wheel
#pip install git+https://github.com/pythonnet/pythonnet
#pip install mitmproxy
#pip install pydivert
#pip install adblockparser psutil

# System
import os, sys
import argparse
import typing
import json , datetime

# All you need for c#:
import clr

# Mitmproxy
from mitmproxy import ctx
from mitmproxy import http

# Bypass programs:
import hashlib

#Adblock
from AdblockHelpers import AdblockHelpers

class PluginConfig:
    # C# DLL:
    AllCSharpDLLs = [   
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

    #Policies:
    PoilcyPath = r"C:\Users\Yoni\Desktop\selfcac\CitadelCore.Windows.Divert.Proxy\CitadelCore.Windows.Example\bin\Debug\policy.json"
    TimePolicyPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\MitmprxyPlugin\timeblock.v2.json"

    # Templates:
    BlockHtmlPath = r"C:\Users\Yoni\Desktop\selfcac\BlockedPage.html"
    TimeBlockReasonText = "Current Time is blocked"

    # Logs:
    BlockLogPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\MitmprxyPlugin\block_log.json"

    # Global objects:
    FilterObj = None;
    TimeBlockObj = None;
    BlockHTMLTemplate = "<unloaded-template>";
    AdblockObj = None;
    
    # Browser pass:
    BypassHeaderPass = "123123";

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

def printHTTPVersion():
    from HTTPProtocolFilter import GitInfo
    print("HTTP filter version: \n"+ "\n".join(GitInfo.AllGitInfo()))

def printTimeVersion():
    from TimeBlockFilter import GitInfo
    print("Time filter version: \n"+ "\n".join(GitInfo.AllGitInfo()))

def hash_string(hash_string):
    sha_signature = \
        hashlib.sha256(hash_string.encode()).hexdigest()
    return sha_signature

def writeBlockLog(tag, url, referer, mimetype, reason):
    jsonObj = { "time":str(datetime.datetime.now()), "tag":tag, "url":url, "referer":referer, "mimetype":mimetype, "reason":reason};
    _log(f"Blocks {tag} {mimetype}\n{reason}\n{url}")
    with open(PluginConfig.BlockLogPath, "a") as f:
        json.dump(jsonObj,f)
        f.write('\n')

def _log(text):
    print("[PLUGIN] " + str(text));

def _err(text):
    print("[ERROR] [PLUGIN] " + str(text));

def init():
    # Debug log:
    try:
        _log("Python version: " + sys.version)
        _log("Exe path: " + sys.executable)
        _log("Command: " + " ".join(sys.argv))

        loadAssemblies(PluginConfig.AllCSharpDLLs)
        protectProcess()

        printHTTPVersion();
        printTimeVersion();

        from HTTPProtocolFilter import FilterPolicy
        PluginConfig.FilterObj = FilterPolicy() # (new Class) in python
        http_policy_content = readAllText(PluginConfig.PoilcyPath);
        PluginConfig.FilterObj.reloadPolicy(http_policy_content);

        from TimeBlockFilter import TimeFilterObject
        PluginConfig.TimeBlockObj = TimeFilterObject() # (new Class) in python
        time_policy_content = readAllText(PluginConfig.TimePolicyPath);
        PluginConfig.TimeBlockObj.reloadPolicy(time_policy_content);

        with open(PluginConfig.BlockHtmlPath,'r',encoding="utf-8") as file:
            PluginConfig.BlockHTMLTemplate = file.read()
            
        _log("All C# DLLs Loaded")

        PluginConfig.AdblockObj = AdblockHelpers();
        _log("All Adblock Loaded")

        return True;
    except Exception as ex:
        _err("Problem loading plugin. stopping. Error: " + str(ex))
        
    return False

def findInHeaders(headers, headername):
    result = ""
    headernamelower = headername.lower()
    for k, v in headers.items():
            headername = k.lower()
            headervalue = v
            if headername == headernamelower:
                result = headervalue
                break;
    return result;

def make_google_safe(flow):
    host = flow.request.pretty_host.lower()

    # Native way:
    # https://github.com/mitmproxy/mitmproxy/blob/master/examples/simple/modify_querystring.py
    if host.find("google") > -1:
        flow.request.query["safe"] = "active"


def applyF2Cookie(original):
    #### TEST: original = "VISITOR_INFO1_LIVE=k0Fsssssk0Sg;  SAPISID=BH; YSC=ASDsa16; s_gl=ASDACCE==; PrEf=f1=50000000&F2=55&hl=en&al=iw; cvdm=grid&f5=30000&f4=4000000;"
    new_cookie = "";

    try:
        # Split into cookies
        k = original.replace('; ',';').split(';')
        # Get "pref" cookie
        p = [x for x in k if x.lower().find("pref")==0]
        # Remove "pref="
        pv = (p[0])[p[0].find('=')+1:]
        # Make dictionary from values in "pref"
        pvDict = {i[0].lower(): i[1] for i in [x.split('=') for x in pv.split('&')]}
        # Apply filter:
        pvDict["f2"] = "8000000"
        p_new = "PREF=" + '&'.join([key +"="+pvDict[key] for key in pvDict])
        # Get other cookies
        k_new = [x for x in k if x.lower().find("pref")!=0]
        k_new.append(p_new)
        # Get new cookie with new pref
        new_cookie = '; '.join(k_new)
    except Exception as ex:
        _err(ex);
       
    return new_cookie

def make_youtube_safe(flow):
    isRelevant = False;
    isChanged = False;

    host = flow.request.pretty_host.lower()

    if host.find("youtube.com") > -1:
        isRelevant = True;
        cookie = findInHeaders(flow.request.headers, "cookie");
        if cookie != "":
            isChanged = True;
            flow.request.headers["cookie"] = applyF2Cookie(cookie);
        else:
            isChanged = False;
    
    if isRelevant:
        if isChanged:
            _log("Youtube Filtered!!!");
        else:
            _log("Youtube not filtered :(")
        
def check_bypass_pass(flow):
    byPassed = False;

    url = flow.request.pretty_url.lower()

    pass_hash = findInHeaders(flow.request.headers, "mitm-secret");

    if pass_hash != "":
        computed_pass_hash = hash_string(url + PluginConfig.BypassHeaderPass).lower() 
        if computed_pass_hash == pass_hash.lower():
            byPassed = True;
        else:
            _log("[HASH] " + url + "  Got:" + pass_hash + " Need:" + computed_pass_hash)
    return byPassed;

def shouldFilterRequest(flow): # return (Filter? , accept-type)
    acceptvalue = findInHeaders(flow.request.headers, "accept").lower();
    if acceptvalue == "":
        return (False, None);

    filteredTypes = ["html","json","x-javascript","*/*"]; # some here because facebok use them
    if len([x for x in filteredTypes if  acceptvalue.find(x) > -1]) > 0:
        return (True, acceptvalue);

    return (False, acceptvalue);

def shouldFilterResponse(flow): # return (Filter? , nosniff?, content-type)

    # Putting here bypass will avoid time-blocking. I know. It would stop on request.
    #       but only here the changes make the content stream. (vs. processResponse())
    if check_bypass_pass(flow):
        _log ("[BYPASS-RES] " + flow.request.pretty_url)
        return (False,False,None);

    contenttype = findInHeaders(flow.response.headers, "content-type").lower();
    if contenttype == "":
        return (False,False,None);

    ignoredTypes = ["image/","text/css","text/javascript"]
    filteredTypes = ["text/","json","x-javascript"]
    if len([x for x in ignoredTypes if  contenttype.find(x) > -1]) == 0:
        if len([x for x in filteredTypes if  contenttype.find(x) > -1]) > 0:
            return (True,False,contenttype);
        elif contenttype.find("/") < 0: # type 'nosniff' used by google
            return (False, True,contenttype);

    return (False, False,contenttype);

def formatReasonHTML(reason):
    return reason.replace("<*", "<b>").replace("*>", "</b>").replace("_<", "<u>").replace(">_", "</u>");

def blockWithReason(flow, reason):
     flow.response = http.HTTPResponse.make(
            202,    # Accepted status code
            PluginConfig.BlockHTMLTemplate.replace('{0}',formatReasonHTML(reason)).encode('utf8'),       # content
            {"Content-Type": "text/html;charset=utf8"}  # (optional) headers
    );

def processRequest(flow, mimetype):
    url = flow.request.pretty_url
    host = flow.request.pretty_host
    path = flow.request.path
    _filter = PluginConfig.FilterObj;
    _timeblock = PluginConfig.TimeBlockObj;

    ENFORCE = 0,
    MAPPING = 1

    referer = findInHeaders(flow.request.headers, "referer");
    if (_filter.getMode() == MAPPING) :
        writeBlockLog("mapping-req",url, referer, mimetype, "mapping");
    else:
        if _timeblock.isBlockedNow():
            blockWithReason(flow, PluginConfig.TimeBlockReasonText);
            # No time block reason!
            _log(f"Request time block url: {url}")
        else:
            if check_bypass_pass(flow):
                if PluginConfig.AdblockObj.should_block(flow.request.pretty_url):
                    blockWithReason(flow,"Divert Filter Adblock Filter");
                else:
                    _log ("[BYPASS-REQ] " + flow.request.pretty_url)
                    return;

            (whitelisted, reason) = _filter.isWhitelistedURL(host,path, None);
            if not whitelisted:
                writeBlockLog("block-req-url",url, referer, mimetype, reason);
                blockWithReason(flow,reason);

def processResponse(flow, mimetype):
    url = flow.request.pretty_url
    host = flow.request.pretty_host
    path = flow.request.path
    _filter = PluginConfig.FilterObj;
    _timeblock = PluginConfig.TimeBlockObj;

    ENFORCE = 0,
    MAPPING = 1

    referer = findInHeaders(flow.request.headers, "referer");
    if (_filter.getMode() == MAPPING) :
        writeBlockLog("mapping-resp",url, referer, mimetype, "mapping");
    else:
        if _timeblock.isBlockedNow():
            blockWithReason(flow, PluginConfig.TimeBlockReasonText);
            # No time block reason!
            _log(f"Response time block url: {url}")
        else:
            (whitelisted, reason) = _filter.isWhitelistedURL(host,path, None);
            if not whitelisted:
                writeBlockLog("block-resp-url",url, referer, mimetype, reason);
                blockWithReason(flow,reason);
            else:
                try:
                    if not flow.response.content is None:
                        htmlResponse = flow.response.content.decode('utf8')
                        (bodyblocked, reason) = _filter.isBodyBlocked(htmlResponse, None);
                        if bodyblocked:
                            writeBlockLog("block-resp-bdy",url, referer, mimetype, reason);
                            blockWithReason(flow,reason);
                except Exception as ex:
                    _err("Error in body processing: " + str(ex))
                    pass


class MitmFilterPlugin():
    def __init__(self):
        _log ("Plugin init.");

    def request(self, flow):
        url = flow.request.pretty_url
        host = flow.request.pretty_host
        query = flow.request.query # (x in query)? ==> query[x]
        
        make_google_safe(flow);
        make_youtube_safe(flow);

        # Disable websocket compression (not in mitm yet)
        flow.request.headers["Sec-WebSocket-Extensions"] = ""
    
        shouldFilter, req_type = shouldFilterRequest(flow);

        if shouldFilter:
            processRequest(flow, req_type);
        else:
            _log(f"Not filtering accept-type {req_type} for {url}");

    def responseheaders(self, flow):
        """
        Enables streaming if only read headers
        """
        url = flow.request.pretty_url
        host = flow.request.pretty_host
        query = flow.request.query # (x in query)? ==> query[x]

        shouldFilter, nosniffexists, resp_type = shouldFilterResponse(flow);

        if shouldFilter: # type 'nosniff' used by google, look back to req
            flow.response.stream = False
        else:
            if nosniffexists:
                shouldFilterByReq, req_type = shouldFilterRequest(flow);
                if shouldFilterByReq:
                    flow.response.stream = False
                else:
                    flow.response.stream = True
                    _log(f"Not filtering types '{req_type}'->'{resp_type}' for {url}");    
            else:
                flow.response.stream = True    
                _log(f"Not filtering content-type {resp_type} for {url}");

    def response(self, flow):
        # If we are here - block! (also we get here from custom response)
        if  flow.response.stream:
            return;
        
        status_code = flow.response.status_code;
        if status_code == 202:
            # Already blocked by request!
            return;

        acceptvalue = findInHeaders(flow.request.headers, "accept").lower();
        contenttype = findInHeaders(flow.response.headers, "content-type").lower();
        processResponse(flow, f"accept '{acceptvalue}' -> content-type '{contenttype}'")
        
if __name__ == "__main__":
    print ("Init ok? " + str(init()))
else:
    if init():
        addons = [
            MitmFilterPlugin()
        ]
