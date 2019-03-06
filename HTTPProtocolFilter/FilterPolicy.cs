using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using HTTPProtocolFilter.Utils;

namespace HTTPProtocolFilter
{
    public class FilterPolicy : JSONBaseClass, IHTTPFilter
    {
        WorkingMode proxyMode = WorkingMode.ENFORCE;

        public WorkingMode getMode()
        {
            return proxyMode;
        }

        #region DomainsFilter

        private Utils.Trie<AllowDomain> allowedDomainsTrie;
        private void initDomains(List<AllowDomain> newDomains)
        {
            // Fast search

            allowedDomainsTrie = new Utils.Trie<AllowDomain>();
            foreach (AllowDomain domain in newDomains)
            {
                allowedDomainsTrie.Insert(domain.DomainFormat, domain);
            }
        }

        private List<AllowDomain> _allDomains = new List<AllowDomain>();
        List<AllowDomain> AllowedDomains
        {
            get
            {
                return _allDomains;
            }
            set
            {
                initDomains(value);
            }
        }

        public AllowDomain findDomain(string host)
        {
            TrieNode<AllowDomain> result = allowedDomainsTrie.CheckDomain(host);
            if (result == null)
            {
                return null;
            }
            return result.Tag;
        }

        #endregion

        #region Phrases

        List<PhraseFilter> BlockedPhrases = new List<PhraseFilter>();

        public static List<string> getWords(string text)
        {
            text = text.ToLower(); // all compares are done in little case

            List<string> words = new List<string>();
            bool insideWord = char.IsLetter(text[0]);
            StringBuilder currentWord = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    if (insideWord)
                    {
                    }
                    else
                    {
                        insideWord = true;
                    }
                    currentWord.Append(text[i]);
                }
                else // Not char
                {
                    if (insideWord)
                    {
                        insideWord = false;
                        words.Add(currentWord.ToString());
                        currentWord.Clear();
                    }
                    else
                    {
                        // Nothing
                    }
                }
            }

            if (insideWord) // Word at the end of text
            {
                words.Add(currentWord.ToString());
            }

            return words;
        }

        public static bool checkPhraseFoundSimple(string Content, PhraseFilter filter)
        {
            bool found = false;
            switch (filter.Type)
            {
                case BlockPhraseType.CONTAIN:
                    found = Content.IndexOf(filter.Phrase) > -1;
                    break;
                case BlockPhraseType.REGEX:
                    found = Regex.IsMatch(Content, filter.Phrase);
                    break;
            }
            return found;
        }

        public static bool checkPhraseFoundWord(List<string> words, PhraseFilter filter)
        {
            bool found = false;
            switch (filter.Type)
            {
                case BlockPhraseType.EXACTWORD:
                    found = words.Contains(filter.Phrase.ToLower());
                    break;
                case BlockPhraseType.WORDCONTAINING:
                    found = words.FindIndex((word) => word.Contains(filter.Phrase.ToLower())) > -1;
                    break;
            }
            return found;
        }

        public bool checkPhrase(string Content)
        {
            bool allowed = true;
            List<string> Words = getWords(Content);

            for (int i = 0; i < BlockedPhrases.Count; i++)
            {
                switch (BlockedPhrases[i].Type)
                {
                    case BlockPhraseType.CONTAIN:
                    case BlockPhraseType.REGEX:
                        if (checkPhraseFoundSimple(Content, BlockedPhrases[i]))
                        {
                            allowed = false;
                        }
                        break;

                    case BlockPhraseType.EXACTWORD:
                    case BlockPhraseType.WORDCONTAINING:
                        if (checkPhraseFoundWord(Words, BlockedPhrases[i]))
                        {
                            allowed = false;
                        }
                        break;


                    default:
                        break;
                }
            }
            return allowed;
        }

        #endregion 

        public bool isWhitelistedEP(AllowDomain domainObj, string ep)
        {
            throw new NotImplementedException();
        }

        public bool isWhitelistedHost(string host)
        {
            throw new NotImplementedException();
        }

        public bool isWhitelistedURL(Uri uri)
        {
            throw new NotImplementedException();
        }

        public void reloadPolicy(string filename)
        {
            throw new NotImplementedException();
        }

        public void savePolicy(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
