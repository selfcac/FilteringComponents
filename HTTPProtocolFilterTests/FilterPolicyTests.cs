using Microsoft.VisualStudio.TestTools.UnitTesting;
using HTTPProtocolFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProtocolFilter.Tests
{
    [TestClass()]
    public class FilterPolicyTests
    {
        void areTrue(object actual) { Assert.AreEqual(true, actual); }
        void areFalse(object actual) { Assert.AreEqual(false, actual); }

        [TestMethod()]
        public void getWordsTest()
        {
            string text = "<input value=\"gogo\"></input>";
            Assert.AreEqual(true, FilterPolicy.getWords(text).Contains("gogo"));

            text = "go home \"gogo\" g";
            Assert.AreEqual(true, FilterPolicy.getWords(text).Contains("gogo"));

            text = "first second thidr122";
            var words = FilterPolicy.getWords(text);
            areTrue(words.Contains("first"));
            areTrue(words.Contains("second"));
            areTrue(words.Contains("thidr"));
            areFalse(words.Contains("thidr122"));
        }

        [TestMethod()]
        public void checkPhraseFoundSimpleTest()
        {
            string BodyContent = "This text has baDword and \"2wronGword\" and nonOword3 wroNgworK";

            areFalse(FilterPolicy.checkPhraseFoundSimple(BodyContent, new PhraseFilter()
            {
                Phrase = "text",
                Type = BlockPhraseType.EXACTWORD // Not supported in checkPhraseFoundSimple
            }));

            areFalse(FilterPolicy.checkPhraseFoundSimple("", new PhraseFilter()
            {
                Phrase = "notfound",
                Type = BlockPhraseType.CONTAIN
            }));

            areFalse(FilterPolicy.checkPhraseFoundSimple("", new PhraseFilter()
            {
                Phrase = "notfound",
                Type = BlockPhraseType.REGEX
            }));

            areFalse(FilterPolicy.checkPhraseFoundSimple(BodyContent, new PhraseFilter()
            {
                Phrase = "notfound",
                Type = BlockPhraseType.CONTAIN
            }));

            areTrue(FilterPolicy.checkPhraseFoundSimple(BodyContent, new PhraseFilter()
            {
                Phrase = "badword",
                Type = BlockPhraseType.CONTAIN
            }));

            areTrue(FilterPolicy.checkPhraseFoundSimple(BodyContent, new PhraseFilter()
            {
                Phrase = "wrongword",
                Type = BlockPhraseType.CONTAIN
            }));

            areTrue(FilterPolicy.checkPhraseFoundSimple(BodyContent, new PhraseFilter()
            {
                Phrase = "nonoword",
                Type = BlockPhraseType.CONTAIN
            }));

            areTrue(FilterPolicy.checkPhraseFoundSimple(BodyContent, new PhraseFilter()
            {
                Phrase = "wrongwor[tk]",
                Type = BlockPhraseType.REGEX
            }));
        }

        [TestMethod()]
        public void checkPhraseFoundWordTest()
        {

            string BodyContent = "This text has baDword and \"2wronGword\" and nonOword3 wroNgworK";
            List<string> words = FilterPolicy.getWords(BodyContent);

            areFalse(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "text",
                Type = BlockPhraseType.CONTAIN // Not supported in checkPhraseFoundWord
            }));

            areFalse(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "",
                Type = BlockPhraseType.WORDCONTAINING
            }));

            areFalse(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "",
                Type = BlockPhraseType.EXACTWORD
            }));

            areFalse(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "notfound",
                Type = BlockPhraseType.WORDCONTAINING
            }));

            areTrue(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "badword",
                Type = BlockPhraseType.EXACTWORD
            }));

            areTrue(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "wrongword",
                Type = BlockPhraseType.EXACTWORD
            }));

            areTrue(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "nonoword",
                Type = BlockPhraseType.EXACTWORD
            }));

            areTrue(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "rongwor",
                Type = BlockPhraseType.WORDCONTAINING
            }));

        }

        [TestMethod()]
        public void checkEPRuleTest()
        {
            areTrue(FilterPolicy.checkEPRuleMatch(new EPPolicy()
            {
                Type = AllowEPType.STARTWITH,
                EpFormat = "/r/collect"
            }, "/r/coLleCt"));

            areTrue(FilterPolicy.checkEPRuleMatch(new EPPolicy()
            {
                Type = AllowEPType.CONTAIN,
                EpFormat = "&safe=1"
            }, "/searcH?q=anyword&safe=1"));

            areTrue(FilterPolicy.checkEPRuleMatch(new EPPolicy()
            {
                Type = AllowEPType.REGEX,
                EpFormat = "\\/search\\?q=.*&safe=1"
            }, "/searCh?q=anyword&safe=1"));

            areFalse(FilterPolicy.checkEPRuleMatch(new EPPolicy()
            {
                Type = AllowEPType.REGEX,
                EpFormat = "\\/search\\?q=.*&safe=1"
            }, "/search?q=anyword"));

            areFalse(FilterPolicy.checkEPRuleMatch(new EPPolicy()
            {
                Type = AllowEPType.REGEX,
                EpFormat = "\\/search\\?q=.*&safe=1"
            }, ""));


        }

        [TestMethod()]
        public void isWhitelistedEPTest()
        {
            string reason_throwaway = "";

            IHTTPFilter filter = new FilterPolicy()
            {
                BlockedPhrases = new List<PhraseFilter>()
                {
                    new PhraseFilter()
                    {
                        Type = BlockPhraseType.WORDCONTAINING ,
                        Phrase= "bad",
                        Scope = BlockPhraseScope.ALL_SCOPES
                    },
                    new PhraseFilter()
                    {
                        Type = BlockPhraseType.REGEX,
                        Phrase = "wor[dk]",
                        Scope = BlockPhraseScope.ALL_SCOPES
                    }
                }

                ,
                AllowedDomains = new List<DomainPolicy>()
                {
                    new DomainPolicy()
                    {
                        DomainBlocked = false,
                        DomainFormat = "e.com",
                        Type = AllowDomainType.EXACT,
                        AllowEP = new List<EPPolicy>()
                        {
                           new EPPolicy() {
                                Type = AllowEPType.STARTWITH,
                                 EpFormat = "/i-am-whitelisted"
                           }
                        }
                    }
                }
            };

            string ep1 = "/search?q=verybadword";
            string ep2 = "/i-am-whitelisted";
            string ep3 = "/i-am-whitelisted/badword";

            // any ep except bad phrases:
            areTrue(filter.isWhitelistedEP(new DomainPolicy()
            {
                DomainBlocked = false,
                DomainFormat = "",
                Type = AllowDomainType.EXACT,
                AllowEP = new List<EPPolicy>()
            }, ep1, out reason_throwaway));
            areFalse(filter.isContentAllowed(ep1, BlockPhraseScope.URL, out reason_throwaway));

            // only ep that are whitelisted
            var domain1 = ((FilterPolicy)filter).AllowedDomains[0];

            areTrue(filter.isWhitelistedEP(domain1, ep2, out reason_throwaway));
            areFalse(filter.isWhitelistedEP(domain1, "/not-whitelisted", out reason_throwaway));

            areTrue(filter.isWhitelistedEP(domain1, ep2 + "/work", out reason_throwaway));
            areFalse(filter.isContentAllowed(ep2 + "/work", BlockPhraseScope.URL, out reason_throwaway));
            areFalse(filter.isWhitelistedURL(new Uri("http://e.com" + ep2 + "/work"), out reason_throwaway)); // does both checks

            areFalse(filter.isContentAllowed(ep3, BlockPhraseScope.URL, out reason_throwaway));
            areFalse(filter.isWhitelistedURL(new Uri("http://e.com" + ep3), out reason_throwaway)); // does both checks

            areFalse(filter.isWhitelistedEP(null, "", out reason_throwaway));
        }

        [TestMethod()]
        public void URLTest()
        {
            string reason_throwaway = "";

            FilterPolicy filter = new FilterPolicy()
            {
                BlockedPhrases = new List<PhraseFilter>()
                {
                    new PhraseFilter()
                    {
                        Type = BlockPhraseType.WORDCONTAINING ,
                        Phrase= "bad",
                        Scope = BlockPhraseScope.URL
                    },
                    new PhraseFilter()
                    {
                        Type = BlockPhraseType.REGEX,
                        Phrase = "wor[dk]",
                        Scope = BlockPhraseScope.URL
                    }
                }
            };

            filter.AllowedDomains = new List<DomainPolicy>()
            {
                new DomainPolicy()
                {
                    DomainBlocked = false,
                    DomainFormat = "go.com",
                    Type = AllowDomainType.SUBDOMAINS,
                    AllowEP = new List<EPPolicy>()
                    {
                       new EPPolicy() {
                            Type = AllowEPType.STARTWITH,
                             EpFormat = "/i-am-whitelisted"
                       }
                    }
                }
            };

            string ep1 = "/search?q=verybadword";
            string ep2 = "/i-am-whitelisted";
            string ep3 = "/i-am-whitelisted/badword";

            areTrue(filter.isWhitelistedHost("g.go.com"));
            areTrue(filter.isWhitelistedURL("go.com", ep2, out reason_throwaway));
            areFalse(filter.isWhitelistedURL("go.com", "/not-ok", out reason_throwaway));
            areFalse(filter.isWhitelistedURL("go-go.com", ep2, out reason_throwaway));

            areTrue(filter.isWhitelistedHost("go.com"));
            areFalse(filter.isWhitelistedURL("g.go.com", ep2 + "/work", out reason_throwaway));

        }

        [TestMethod()]
        public void nullAtInit()
        {
            IHTTPFilter filter = new FilterPolicy();
            try
            {
                filter.isWhitelistedHost("try.com");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void emptyContent()
        {
            FilterPolicy filter = new FilterPolicy()
            {
                BlockedPhrases = new List<PhraseFilter>()
                {
                    new PhraseFilter()
                    {
                        Type = BlockPhraseType.WORDCONTAINING ,
                        Phrase= "bad"
                    },
                    new PhraseFilter()
                    {
                        Type = BlockPhraseType.REGEX,
                        Phrase = "wor[dk]"
                    }
                }
            };


            Assert.AreEqual(null, filter.findBlockingPhrase("", BlockPhraseScope.ALL_SCOPES));
        }
    }
}