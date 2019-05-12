using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortsOwners;

namespace RestartWhenPortUsed
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkWatcher nw = new NetworkWatcher();
            nw.Start(5000);

            bool isOutsideUserSpace = nw.isLocalAddressAdmin("0.0.0.0:8080",
                /* When not foud: */ true /* Assume not in user space => admin=true*/);

            nw.Stop();

            Console.WriteLine("User took it? " + !isOutsideUserSpace);
            if (!isOutsideUserSpace)
            {
                System.Diagnostics.Process.Start("cmd", "/c \"shutdown /r /f /t 0 /y\"");
            }
        }
    }
}
