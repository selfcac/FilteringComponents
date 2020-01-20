from AdblockHelpers import AdblockHelpers

adblock = AdblockHelpers(download=False);

print("1) " + str(adblock.should_block("graph.facebook.com")))
print("2) " + str(adblock.should_block("graph.facebook.com",)))
print("3) " + str(adblock.should_block("www.googletagmanager.com", {'third-party' : True})))

test4 = "https://securepubads.g.doubleclick.net/tag/js/gpt.js"
print("4) " + str(adblock.should_block(test4, )))
print("5) " + str(adblock.should_block(test4, {'third-party' : True})))
print("6) " + str(adblock.should_block(test4, {'third-party' : True, 'script' : True})))

test5 = "https://www.facebook.com/tr/?id=00000000&ev=fb_page_view&dl=some_url"
print("7) " + str(adblock.should_block(test5, )))
print("8) " + str(adblock.should_block(test5, {'third-party' : True})))
print("9) " + str(adblock.should_block(test5, {'third-party' : True, 'script' : True})))

MemoryInfo.printMemory()