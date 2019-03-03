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
        [TestMethod()]
        public void getWordsTest()
        {
            string text = "<input value=\"gogo\"></input>";
            Assert.AreEqual(true, FilterPolicy.getWords(text).Contains("gogo"));

            text = "go home \"gogo\" g";
            Assert.AreEqual(true, FilterPolicy.getWords(text).Contains("gogo"));

            text = "first second thidr122";
            var words = FilterPolicy.getWords(text);
            Assert.AreEqual(true, words.Contains("first"));
            Assert.AreEqual(true, words.Contains("second"));
            Assert.AreEqual(true, words.Contains("thidr"));
            Assert.AreEqual(false, words.Contains("thidr122"));
        }
    }
}