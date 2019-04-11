using System;
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

        private Utils.Trie<DomainPolicy> allowedDomainsTrie;
        private List<DomainPolicy> _allDomains = new List<DomainPolicy>();

        private void initDomains(List<DomainPolicy> newDomains)
        {
            // Fast search

            allowedDomainsTrie = new Utils.Trie<DomainPolicy>();
            foreach (DomainPolicy domain in newDomains)
            {
                allowedDomainsTrie.InsertDomain( domain);
            }

            _allDomains = newDomains;
        }

        public List<DomainPolicy> AllowedDomains
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
            AllowedDomains = new List<DomainPolicy>();
        }

        public DomainPolicy findAllowedDomain(string host)
        {
            return allowedDomainsTrie.CheckDomain(host)?.Tag;
        }

        #endregion

        #region Phrases

        public List<PhraseFilter> BlockedPhrases = new List<PhraseFilter>();

        public static List<string> getWords(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

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

        /// <summary>
        /// Check if content clean of bad words
        /// </summary>
        /// <param name="Content"></param>
        /// <returns>True if content is allowed under policy</returns>
        public bool isContentAllowed(string Content, BlockPhraseScope scope)
        {
            return (findBlockingPhrase(Content,scope) == null);
        }

        /// <summary>
        /// Find the phrase that the content is blocked from using.
        /// </summary>
        /// <param name="Content"></param>
        /// <returns>Null if no phrase rule is applicabalbe (allowed)</returns>
        public PhraseFilter findBlockingPhrase(string Content, BlockPhraseScope scope)
        {
            PhraseFilter result = null;
            List<string> Words = getWords(Content);

            for (int i = 0; i < BlockedPhrases.Count && (result == null); i++)
            {
                if (BlockedPhrases[i].Scope == scope)
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
            }

            return result;
        }

        #endregion 

       
        public static bool checkEPRuleMatch(EPPolicy ep, string epPath )
        {
            bool match = false;
            epPath = epPath.ToLower();

            switch(ep.Type)
            {
                case AllowEPType.CONTAIN:
                    match = epPath.IndexOf(ep.EpFormat) > -1; // not using contain cause HTTP is ASCII only
                    break;

                case AllowEPType.REGEX:
                    match = Regex.IsMatch(epPath, ep.EpFormat);
                    break;

                case AllowEPType.STARTWITH:
                    match = epPath.StartsWith(ep.EpFormat);
                    break;
            }

            return match;
        }

        /// <summary>
        /// Check if ep is in policy
        /// </summary>
        /// <returns>true if allowed</returns>
        public bool isWhitelistedEP(DomainPolicy domainObj, string ep)
        {
            if (domainObj == null)
                return false;

            if (!domainObj.DomainBlocked)
                return false;

            bool allowed = false;

            // check if ep in domain
            if (domainObj.AllowEP.Count > 0 )
            {
                // Whitelist : All EP are blocked unless some ep allow (and no block ep later).
                for (int i=0;i<domainObj.AllowEP.Count;i++)
                {
                    if (checkEPRuleMatch(domainObj.AllowEP[i], ep))
                    {
                        allowed = true;
                        break;
                    }
                }
            }
            else
            {
                // All EP allowed unless found by block ep
                allowed = true;
            }

            if (allowed)
            {
                for (int i = 0; i < domainObj.BlockEP.Count; i++)
                {
                    if (checkEPRuleMatch(domainObj.BlockEP[i], ep))
                    {
                        allowed = false;
                        break;
                    }
                }
            }


            return allowed;
        }

        /// <summary>
        /// Check if Host (domain) is in policy
        /// </summary>
        /// <returns>true if allowed</returns>
        public bool isWhitelistedHost(string host)
        {
            DomainPolicy domain = allowedDomainsTrie.CheckDomain(host)?.Tag;
            return domain != null;
        }

        /// <summary>
        /// Check if complete URL is in policy (Host+EP+Phrase checks)
        /// </summary>
        /// <returns>true if allowed</returns>
        public bool isWhitelistedURL(Uri uri)
        {
            return isWhitelistedURL(uri.Host, uri.PathAndQuery);
        }

        public bool isWhitelistedURL(string host, string pathAndQuery)
        {
            bool allowed = false;
            DomainPolicy domainRule = findAllowedDomain(host ?? "");
            if (domainRule != null)
            {
                allowed = isWhitelistedEP(domainRule, pathAndQuery ?? "");

                if (allowed) // Finally check for banned phrases.
                    allowed = isContentAllowed(pathAndQuery, BlockPhraseScope.URL);
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
