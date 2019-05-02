# Python x86
#pip install msl-loadlib
#pip install git+https://github.com/pythonnet/pythonnet
#pip(3) install mitmproxy

# System
import os, sys
import argparse
import typing
import json , datetime

# C# Load
from msl.loadlib import LoadLibrary
import clr

# Mitmproxy
from mitmproxy import ctx
from mitmproxy import http

class PluginConfig:
    #DLLs:
    CSCommonPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\HTTPProtocolFilter\bin\x86\Debug\Common.dll"
    CSFilterPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\HTTPProtocolFilter\bin\x86\Debug\HTTPProtocolFilter.dll"
    CSTimeblockPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\TimeBlockFilter\bin\Debug\TimeBlockFilter.dll"
    CSProtectProcessPath = r"C:\Users\Yoni\Desktop\selfcac\ProcessTerminationProtection\ProcessTerminationProtection\bin\Debug\ProcessTerminationProtection.dll"

    #Policies:
    PoilcyPath = r"C:\Users\Yoni\Desktop\selfcac\CitadelCore.Windows.Divert.Proxy\CitadelCore.Windows.Example\bin\Debug\policy.json"
    TimePolicyPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\MitmprxyPlugin\timeblock.v2.json"

    # Templates:
    BlockHtmlPath = r"C:\Users\Yoni\Desktop\selfcac\CitadelCore.Windows.Divert.Proxy\CitadelCore.Windows.Example\bin\Debug\BlockedPage.html"
    TimeBlockReasonText = "Current Time is blocked"

    # Logs:
    BlockLogPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\MitmprxyPlugin\block_log.json"

    # Global objects:
    FilterObj = None;
    TimeBlockObj = None;
    BlockHTMLTemplate = "<unloaded-template>";

def writeBlockLog(tag, url, referer, mimetype, reason):
    jsonObj = { "time":str(datetime.datetime.now()), "tag":tag, "url":url, "referer":referer, "mimetype":mimetype, "reason":reason};
    _log(f"Blocks {tag} {mimetype} {url}")
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

        commonDLL = LoadLibrary(PluginConfig.CSCommonPath,'net')
        filterDLL = LoadLibrary(PluginConfig.CSFilterPath,'net')
        timeblockDLL = LoadLibrary(PluginConfig.CSTimeblockPath,'net')

        pprotectDLL = LoadLibrary(PluginConfig.CSProtectProcessPath,'net')
        pprotectDLL._lib.ProcessTerminationProtection.ProcessProtect.ProtectCurrentFromUsers();

        PluginConfig.FilterObj = filterDLL._lib.HTTPProtocolFilter.FilterPolicy();
        PluginConfig.FilterObj.reloadPolicy(PluginConfig.PoilcyPath);

        PluginConfig.TimeBlockObj = timeblockDLL._lib.TimeBlockFilter.TimeFilterObject();
        PluginConfig.TimeBlockObj.reloadPolicy(PluginConfig.TimePolicyPath);

        with open(PluginConfig.BlockHtmlPath,'r',encoding="utf-8") as file:
            PluginConfig.BlockHTMLTemplate = file.read()
            
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

    #if host.find("google") > -1:
    #    path = flow.request.path;
    #    _log("Adding safe=active to : " + path)
    #    if path.find("?") > -1:
    #        path += "&";
    #    else:
    #        path += "?"
    #    path += "safe=active";

    # Native way:
    # https://github.com/mitmproxy/mitmproxy/blob/master/examples/simple/modify_querystring.py
    if host.find("google") > -1:
        flow.request.query["safe"] = "active"

def shouldFilterRequest(flow): # return (Filter? , accept-type)
    acceptvalue = findInHeaders(flow.request.headers, "accept").lower();
    if acceptvalue == "":
        return (False, None);

    filteredTypes = ["html","json","x-javascript","*/*"]; # some here because facebok use them
    if len([x for x in filteredTypes if  acceptvalue.find(x) > -1]) > 0:
        return (True, acceptvalue);

    return (False, acceptvalue);

def shouldFilterResponse(flow): # return (Filter? , nosniff?, content-type)
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
                    htmlResponse = flow.response.content.decode('utf8')
                    (bodyblocked, reason) = _filter.isBodyBlocked(htmlResponse, None);
                    if bodyblocked:
                        writeBlockLog("block-resp-bdy",url, referer, mimetype, reason);
                        blockWithReason(flow,reason);
                except Exception as ex:
                    pass


class MitmFilterPlugin():
    def __init__(self):
        _log ("Plugin init.");

    def request(self, flow):
        url = flow.request.pretty_url
        host = flow.request.pretty_host
        query = flow.request.query # (x in query)? ==> query[x]
        
        make_google_safe(flow);
    
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
