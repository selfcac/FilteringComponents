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
            log.i("Starting Control panel server...");

            TcpServer myServer = new TcpServer();
            myServer.StartServer(Common.ConnectionHelpers.ControlPanelPort);

            Console.Read();

            myServer.StopServer();
        }
    }
}
