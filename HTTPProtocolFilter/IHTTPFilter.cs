using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProtocolFilter
{
    public enum BlockPhraseType // CONTENT \ EP
    {
        CONTAIN = 0, WORD, REGEX
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

    public class PhaseFilter
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
    }

    public interface IHTTPFilter
    {
        void blockPhrase(PhaseFilter phase);
        void whitelistEpOfDomain(AllowDomain domain, List<AllowEP> EPs);
        void allowEntireDomain(AllowDomain domain);
        void setWorkingMode(WorkingMode mode);

        void reloadPolicy(string filename);
        void savePolicy(string filename);
    }
}
