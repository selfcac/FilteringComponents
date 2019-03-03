using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using HTTPProtocolFilter.Utils;

namespace HTTPProtocolFilter
{
    class FilterPolicy : JSONBaseClass, IHTTPFilter
    {
        WorkingMode proxyMode = WorkingMode.ENFORCE;

        public WorkingMode getMode()
        {
            return proxyMode;
        }

        List<PhraseFilter> BlockedPhrases = new List<PhraseFilter>();

        Utils.Trie<AllowDomain> allowedDomainsTrie;
        private void initDomains()
        {
            // Fast search

            allowedDomainsTrie = new Utils.Trie<AllowDomain>();
            foreach(AllowDomain domain in _allDomains)
            {
                allowedDomainsTrie.Insert(domain.DomainFormat, domain);
            }
        }

        private List<AllowDomain> _allDomains = new List<AllowDomain>();
        List<AllowDomain> AllowedDomains 
        {
            get {
                return _allDomains;
            }
            set
            {
                _allDomains = value;
            }
        }

        public AllowDomain getDomain(string host)
        {
            TrieNode<AllowDomain> result = allowedDomainsTrie.CheckDomain(host);
            if ( result == null)
            {
                return null;
            }
            return result.Tag;
        }

        public bool checkPhraseFound(string Content, PhraseFilter filter)
        {
            bool found = false;
            switch (filter.Type)
            {
                case BlockPhraseType.CONTAIN:
                    break;
            }
            return found;
        }

        public bool hasPhrase(string Content)
        {
            bool allowed = true;
            for (int i=0; i< BlockedPhrases.Count; i++)
            {
                if (checkPhraseFound(Content, BlockedPhrases[i]))
                {
                    allowed = false;
                    break;
                }
            }
            return allowed;
        }

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
