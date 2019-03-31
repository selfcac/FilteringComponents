﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using HTTPProtocolFilter.Utils;
using static Common.ConnectionHelpers;

namespace HTTPProtocolFilter
{
    public class FilterPolicy : JSONBaseClass, IHTTPFilter
    {
        public WorkingMode proxyMode = WorkingMode.ENFORCE;

        public WorkingMode getMode()
        {
            return proxyMode;
        }

        #region DomainsFilter

        private Utils.Trie<AllowDomain> allowedDomainsTrie;
        private List<AllowDomain> _allDomains = new List<AllowDomain>();

        private void initDomains(List<AllowDomain> newDomains)
        {
            // Fast search

            allowedDomainsTrie = new Utils.Trie<AllowDomain>();
            foreach (AllowDomain domain in newDomains)
            {
                allowedDomainsTrie.InsertDomain( domain);
            }

            _allDomains = newDomains;
        }

        public List<AllowDomain> AllowedDomains
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

        public FilterPolicy()
        {
            AllowedDomains = new List<AllowDomain>();
        }

        public AllowDomain findAllowedDomain(string host)
        {
            return allowedDomainsTrie.CheckDomain(host)?.Tag;
        }

        #endregion

        #region Phrases

        public List<PhraseFilter> BlockedPhrases = new List<PhraseFilter>();

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
                        if (currentWord.Length > 0)
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
                if (currentWord.Length > 0)
                    words.Add(currentWord.ToString());
            }

            return words;
        }

        public static bool checkPhraseFoundSimple(string Content, PhraseFilter filter)
        {
            if ((filter.Phrase ?? "") == "")
                return false;

            bool found = false;
            Content = Content.ToLower();
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
            if ((filter.Phrase ?? "") == "")
                return false;

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
            return (findBlockingPhrase(Content) == null);
        }

        public PhraseFilter findBlockingPhrase(string Content)
        {
            PhraseFilter result = null;
            List<string> Words = getWords(Content);

            for (int i = 0; i < BlockedPhrases.Count && (result == null); i++)
            {
                switch (BlockedPhrases[i].Type)
                {
                    case BlockPhraseType.CONTAIN:
                    case BlockPhraseType.REGEX:
                        if (checkPhraseFoundSimple(Content, BlockedPhrases[i]))
                        {
                            result = BlockedPhrases[i];
                        }
                        break;

                    case BlockPhraseType.EXACTWORD:
                    case BlockPhraseType.WORDCONTAINING:
                        if (checkPhraseFoundWord(Words, BlockedPhrases[i]))
                        {
                            result = BlockedPhrases[i];
                        }
                        break;
                }
            }

            return result;
        }

        #endregion 

        public static bool checkEPRule(AllowEP ep, string epPath )
        {
            bool allowed = false;
            epPath = epPath.ToLower();

            switch(ep.Type)
            {
                case AllowEPType.CONTAIN:
                    allowed = epPath.IndexOf(ep.EpFormat) > -1; // not using contain cause HTTP is ASCII only
                    break;

                case AllowEPType.REGEX:
                    allowed = Regex.IsMatch(epPath, ep.EpFormat);
                    break;

                case AllowEPType.STARTWITH:
                    allowed = epPath.StartsWith(ep.EpFormat);
                    break;
            }

            return allowed;
        }

        public bool isWhitelistedEP(AllowDomain domainObj, string ep)
        {
            if (domainObj == null)
                return false;

            bool allowed = false;

            // check if ep in domain
            if (domainObj.WhiteListEP.Count > 0 )
            {
                // Whitelist : All EP are blocked unless some rule allow.
                for (int i=0;i<domainObj.WhiteListEP.Count;i++)
                {
                    if (checkEPRule(domainObj.WhiteListEP[i], ep))
                    {
                        allowed = true;
                        break;
                    }
                }
            }
            else
            {
                // All EP allowed if none specified
                allowed = true;
            }

            if (allowed) // Finally check for banned phrases.
                allowed = checkPhrase(ep); 

            return allowed;
        }

        public bool isWhitelistedHost(string host)
        {
            AllowDomain domain = allowedDomainsTrie.CheckDomain(host)?.Tag;
            return domain != null;
        }

        public bool isWhitelistedURL(Uri uri)
        {
            return isWhitelistedURL(uri.Host, uri.PathAndQuery);
        }

        public bool isWhitelistedURL(string host, string pathAndQuery)
        {
            bool allowed = false;
            AllowDomain domainRule = findAllowedDomain(host ?? "");
            if (domainRule != null)
            {
                allowed = isWhitelistedEP(domainRule, pathAndQuery ?? "");
            }

            return allowed;
        }

        public void reloadPolicy(string filename)
        {
            TaskInfo newPolicyLoad = FilterPolicy.FromFile<FilterPolicy>(filename);
            if (newPolicyLoad)
            {
                FilterPolicy newPolicy = ((TaskInfoResult<FilterPolicy>)newPolicyLoad).result;
                proxyMode = newPolicy.proxyMode;
                BlockedPhrases = newPolicy.BlockedPhrases;
                AllowedDomains = newPolicy.AllowedDomains;
            }
        }

        public void savePolicy(string filename)
        {
            ToFile(filename);
        }
    }
}
