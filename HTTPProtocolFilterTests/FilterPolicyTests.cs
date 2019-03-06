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
            string BodyContent = "This text has badword and \"2wrongword\" and nonoword3 wrongwork";

            areFalse(FilterPolicy.checkPhraseFoundSimple(BodyContent, new PhraseFilter()
            {
                Phrase = "text",
                Type = BlockPhraseType.EXACTWORD // Not supported in checkPhraseFoundSimple
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
            
            string BodyContent = "This text has badword and \"2wrongword\" and nonoword3 wrongwork";
            List<string> words = FilterPolicy.getWords(BodyContent);

            areFalse(FilterPolicy.checkPhraseFoundWord(words, new PhraseFilter()
            {
                Phrase = "text",
                Type = BlockPhraseType.CONTAIN // Not supported in checkPhraseFoundWord
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
    }
}