using ProcessTerminationProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                foreach(string a in args)
                {
                    int processid = 0;
                    if (int.TryParse(a, out processid))
                    {
                        Console.WriteLine("Trying to protect id: " + processid);
                        ProcessProtect.ProtectFromUsersByID(processid);
                    }
                }
                Console.WriteLine("[DONE]");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}
