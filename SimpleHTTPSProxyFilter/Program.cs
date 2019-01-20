using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SimpleHTTPSProxyFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger log = new Logger("main");
            log.i("Using config created " + Common.Config.Instance.created);
            log.i("Starting proxy...");


            TcpServerThread myServer = new TcpServerThread();
            myServer.StartServer(Common.Config.Instance.ProxyPort);

            Console.Read();

            myServer.StopServer();
        }
    }
}
