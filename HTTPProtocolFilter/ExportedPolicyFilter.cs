using HTTPProtocolFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


public class ExportedPolicyFilter : FilterPolicy
{
    public static List<string> getWords(string text)
    {
        return FilterPolicy.getWords(text);
    }
}

