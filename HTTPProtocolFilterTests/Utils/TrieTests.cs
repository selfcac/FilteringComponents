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
        public void Found(object o) { Assert.AreNotEqual(null, o); }
        public void NotFound(object o) { Assert.AreEqual(null, o); }

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
            

            Found(t.PostfixDomain("go.red.com"));
            Found(t.PostfixDomain("a.b.c.go.red.com"));
            Found(t.SearchDomain("reddit.com"));

            Found( t.CheckDomain("reddit.com"));
            NotFound( t.CheckDomain("go.reddit.com"));
            NotFound( t.CheckDomain("a.b.c.go.reddit.com"));
            NotFound( t.CheckDomain("hack-reddit.com"));

            Found( t.CheckDomain("red.com"));
            Found( t.CheckDomain("go.red.com"));
            Found( t.CheckDomain("a.b.c.d.go.red.com"));
            NotFound( t.CheckDomain("hack-red.com"));

            NotFound( t.CheckDomain("rrrred.com"));
            NotFound( t.CheckDomain("rrrreddit.com"));

            // Check tag contain the info.
            Assert.AreEqual(AllowEPType.CONTAIN, t.CheckDomain("go.gogo.com").Tag.WhiteListEP[0].Type);
        }

       
    }
}