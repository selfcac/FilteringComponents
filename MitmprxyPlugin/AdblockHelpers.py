#pip install adblockparser psutil

class MemoryInfo:
  UNITS = {1000: ['KB', 'MB', 'GB'],  1024: ['KiB', 'MiB', 'GiB']}

  def approximate_size(size, flag_1024_or_1000=True):
      mult = 1024 if flag_1024_or_1000 else 1000
      for unit in MemoryInfo.UNITS[mult]:
          size = size / mult
          if size < mult:
              return '{0:.3f} {1}'.format(size, unit)

  def printMemory():
    import os
    import psutil
    process = psutil.Process(os.getpid())
    print("Mem " + str(MemoryInfo.approximate_size(process.memory_info().rss)))  # in bytes   

class AdblockHelpers:
  block_lists = [
    r"https://easylist.to/easylist/easylist.txt",
    r"https://raw.githubusercontent.com/uBlockOrigin/uAssets/master/filters/filters.txt",
    r"https://raw.githubusercontent.com/uBlockOrigin/uAssets/master/filters/privacy.txt",
    r"https://raw.githubusercontent.com/uBlockOrigin/uAssets/master/filters/badware.txt",
    r"https://raw.githubusercontent.com/uBlockOrigin/uAssets/master/filters/resource-abuse.txt",
    r"https://raw.githubusercontent.com/uBlockOrigin/uAssets/master/filters/unbreak.txt",
    r"https://filters.adtidy.org/extension/ublock/filters/2_without_easylist.txt",
    r"https://filters.adtidy.org/extension/ublock/filters/11.txt",
    r"https://filters.adtidy.org/extension/ublock/filters/3.txt",
    r"https://easylist.to/easylist/easyprivacy.txt",
    r"https://easylist-downloads.adblockplus.org/easyprivacy.txt",
    r"https://www.fanboy.co.nz/enhancedstats.txt",
    r"https://gitcdn.xyz/repo/NanoMeow/MDLMirror/master/hosts.txt",
		r"https://raw.githubusercontent.com/NanoMeow/MDLMirror/master/hosts.txt",
		r"https://www.malwaredomainlist.com/hostslist/hosts.txt",
		r"https://gitcdn.xyz/repo/NanoMeow/MDLMirror/master/filter.txt",
		r"https://raw.githubusercontent.com/NanoMeow/MDLMirror/master/filter.txt",
    r"https://mirror.cedia.org.ec/malwaredomains/justdomains",
		r"https://mirror1.malwaredomains.com/files/justdomains",
    r"https://raw.githubusercontent.com/Spam404/lists/master/adblock-list.txt",
    r"https://easylist.to/easylist/fanboy-annoyance.txt",
  ];
  myFiles = []
  savePath = ""
  rules = None

  def downloadFile(url, savepath):
    print (url + " -> " + savepath)
    import urllib.request
    import shutil
    ...
    # Download the file from `url` and save it locally under `file_name`:
    req = urllib.request.Request(url,headers={'User-Agent': 'Mozilla/5.0'})
    with urllib.request.urlopen(req) as response, open(savepath, 'wb') as out_file:
        shutil.copyfileobj(response, out_file)

  def combine_lines(filelist, fileencoding='utf8'):
    for filename in filelist:
      with open(filename, encoding=fileencoding) as file:
        for line in file:
          yield line

  def updateLists(self,printMemory, shouldDownload):
    if shouldDownload:
      for i in range(0, len(self.block_lists)):
        splits = self.block_lists[i].rsplit('/',1);
        print ("Downloads file: " + self.block_lists[i])
        tmp_save_path =  self.savePath + str(i) + "_" +  splits[1];
        try:
          AdblockHelpers.downloadFile(self.block_lists[i],tmp_save_path);
          self.myFiles.append(tmp_save_path)      
        except:
          print("Error downloading: " +  self.block_lists[i])
    else:
      import glob
      self.myFiles=glob.glob(self.savePath + "\\*.*")
      
    if printMemory:
      MemoryInfo.printMemory()

    from adblockparser import AdblockRules
    self.rules = AdblockRules(
      AdblockHelpers.combine_lines(self.myFiles),
      use_re2=False,
      max_mem=100*1024*1024 # 100mb
      #,supported_options=['script', 'domain', 'image', 'stylesheet', 'object']
    )
    
    if printMemory:
      MemoryInfo.printMemory()

  def __init__(self, savePath="adblock_filters/", download=False, printMemory=True):
    self.savePath = savePath
    self.updateLists(printMemory,download)

  def should_block(self, url, options = {'third-party' : True} ):
      return self.rules.should_block(url,options=options);
    




  

