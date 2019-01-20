using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ControlPanelForSafeControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger log = new Logger("main");
            log.i("Using config created " + Common.Config.Instance.created);
            log.i("Starting Control panel server...");

            TcpServer myServer = new TcpServer();
            myServer.StartServer(Common.Config.Instance.ControlPanelPort);

            Console.Read();

            myServer.StopServer();
        }
    }
}
