from msl.loadlib import LoadLibrary
import clr
import os, sys, json
import argparse
import typing
from mitmproxy import ctx
from mitmproxy import http

class PluginConfig:
    CSCommonPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\HTTPProtocolFilter\bin\x86\Debug\Common.dll"
    CSFilterPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\HTTPProtocolFilter\bin\x86\Debug\HTTPProtocolFilter.dll"
    CSTimeblockPath = r"C:\Users\Yoni\Desktop\selfcac\FilteringComponents\TimeBlock_GuiHelper\bin\Debug\TimeBlock_GuiHelper.exe"
    PoilcyPath = r""
    TimePolicyPath = r""

    FilterObj = None;
    TimeBlockObj = None;


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

        PluginConfig.FilterObj = filterDLL._lib.HTTPProtocolFilter.FilterPolicy();
        PluginConfig.TimeBlockObj = timeblockDLL._lib.TimeBlock_GuiHelper.TimeFilterObject();

        return True;
    except Exception as ex:
        _err("Problem loading plugin. stopping. Error: " + str(ex))
        
    return False

def resp200(flow):
    flow.response = http.HTTPResponse.make(
            200,  # (optional) status code
            b"{\"sucess\":true}",  # (optional) content
            {"Content-Type": "application/json"}  # (optional) headers
        );

def resp200Bad(flow):
    flow.response = http.HTTPResponse.make(
            200,  # (optional) status code
            b"{\"sucess\":false}",  # (optional) content
            {"Content-Type": "application/json"}  # (optional) headers
        );

def giveFile(flow, filename, type):
    _log("Serving: " + filename);
    with open(filename, "rb") as file:
        flow.response = http.HTTPResponse.make(
            200,  # (optional) status code
            file.read(),  # content
            {"Content-Type": type}  #  headers
            );

def findInHeaders(headers, hadername):
    result = ""
    headernamelower = headername.lower()
    for k, v in headers.items():
            headername = k.lower()
            headervalue = v
            if headername == headernamelower:
                result = headervalue
                break;
    return result;

class MitmFilterPlugin():
    def __init__(self):
        #TODO: Load policies and CS-DLLs

        _log ("Plugin init.");

    def request(self, flow):
        url = flow.request.pretty_url
        host = flow.request.host
        query = flow.request.query # (x in query)? ==> query[x]

    def response(self, flow):
        url = flow.request.pretty_url
        host = flow.request.host
        query = flow.request.query # (x in query)? ==> query[x]

        contenttype = findInHeaders(flow.response.headers, "content-type");
        if contenttype == "":
            return


if __name__ == "__main__":
    print ("Init ok? " + str(init()))
else:
    if init():
        addons = [
            MitmFilterPlugin()
        ]