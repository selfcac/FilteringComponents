using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProtocolFilter
{
    public enum BlockPhraseType // CONTENT \ EP
    {
        CONTAIN = 0, EXACTWORD, WORDCONTAINING, REGEX
    }

    public enum AllowEPType //  EP
    {
        CONTAIN = 0, STARTWITH, REGEX
    }

    public enum AllowDomainType // DOMAIN
    {
        EXACT = 0, SUBDOMAINS
    }

    public enum WorkingMode
    {
        ENFORCE = 0, MAPPING
    }

    public class PhraseFilter
    {
        public BlockPhraseType Type;
        public string Phrase;


    }

    public class AllowEP
    {
        public AllowEPType Type;
        public string EpFormat;
    }

    public class AllowDomain
    {
        public AllowDomainType Type;
        public string DomainFormat;
        public List<AllowEP> WhiteListEP = new List<AllowEP>();

        public static implicit operator AllowDomain (string input)
        {
            return new AllowDomain() {
                DomainFormat = input,
                Type = ((input[0] == '.') ? AllowDomainType.SUBDOMAINS : AllowDomainType.EXACT)
                };
        }
    }

    public interface IHTTPFilter
    {
        WorkingMode getMode();

        bool isWhitelistedURL(Uri uri);
        bool isWhitelistedURL(string host, string pathAndQuery);

        bool isWhitelistedHost(string host);
        AllowDomain findAllowedDomain(string host);

        bool isWhitelistedEP(AllowDomain domainObj, string ep);

        bool checkPhrase(string Content);

        void reloadPolicy(string filename);
        void savePolicy(string filename);
    }
}
