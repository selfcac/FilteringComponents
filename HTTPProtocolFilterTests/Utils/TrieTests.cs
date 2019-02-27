using Microsoft.VisualStudio.TestTools.UnitTesting;
using HTTPProtocolFilter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProtocolFilter.Utils.Tests
{
    [TestClass()]
    public class TrieTests
    {
        

        [TestMethod()]
        public void TrieTest()
        {
            Trie<AllowDomain> t = new Trie<AllowDomain>();
            t.InsertDomainRange(new List<AllowDomain>()
            {
                "reddit.com",  ".red.com"
            });

            AllowDomain ad = new AllowDomain()
            {
                Type = AllowDomainType.SUBDOMAINS,
                DomainFormat = ".gogo.com",
                WhiteListEP = new List<AllowEP>()
                 {
                     new AllowEP()
                     {
                         Type = AllowEPType.CONTAIN,
                         EpFormat = "Contain"
                     }
                 }
            };
            t.InsertDomain(ad);
            

            Assert.AreNotEqual(null, t.PostfixDomain("go.red.com"));
            Assert.AreNotEqual(null, t.PostfixDomain("a.b.c.go.red.com"));
            Assert.AreNotEqual(null, t.SearchDomain("reddit.com"));

            Assert.AreNotEqual(null, t.CheckDomain("reddit.com"));
            Assert.AreEqual(null, t.CheckDomain("go.reddit.com"));
            Assert.AreEqual(null, t.CheckDomain("a.b.c.go.reddit.com"));
            Assert.AreEqual(null, t.CheckDomain("hack-reddit.com"));

            Assert.AreNotEqual(null, t.CheckDomain("red.com"));
            Assert.AreNotEqual(null, t.CheckDomain("go.red.com"));
            Assert.AreNotEqual(null, t.CheckDomain("a.b.c.d.go.red.com"));
            Assert.AreEqual(null, t.CheckDomain("hack-red.com"));

            Assert.AreEqual(null, t.CheckDomain("rrrred.com"));
            Assert.AreEqual(null, t.CheckDomain("rrrreddit.com"));

            Assert.AreEqual(AllowEPType.CONTAIN, t.CheckDomain("go.gogo.com").Tag.WhiteListEP[0].Type);
        }

       
    }
}