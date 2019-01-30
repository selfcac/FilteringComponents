using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SimpleHTTPSProxyFilter
{
    class TcpServerThread
    {
        Logger log = new Logger("server");
        TcpListener tcpHttpServer;

        public static object blockLogLock = new object();
        public static string whitelist;

        public void StartServer(int port)
        {
            FileInfo whitelistFile = Common.Config.Instance.whitelistFile;
            if (whitelistFile.Exists)
            {
                whitelist = File.ReadAllText(whitelistFile.FullName);
            }

            tcpHttpServer = new TcpListener(IPAddress.Any, port);
            tcpHttpServer.Start();
            log.i("Started tcp server, port " + port);

            tcpHttpServer.BeginAcceptTcpClient(AcceptTcpClientAsync, tcpHttpServer);

        }

        public void StopServer()
        {
            tcpHttpServer.Stop();
            log.i("Stopped tcp server");
        }


        void AcceptTcpClientAsync(IAsyncResult ar)
        {
            TcpListener listener = ar.AsyncState as TcpListener;
            if (listener == null) return;

            TcpClient client = null;
            try
            {
                client = listener.EndAcceptTcpClient(ar);
                log.i("Got new client @" + ((IPEndPoint)client.Client.RemoteEndPoint).Port);

                // Start processing:
                TcpSession.Start(client);
            }
            catch (ObjectDisposedException ex) {
                // When stopping the listener (no actual client)
                return;
            }
            catch (Exception ex)
            {
                log.e("Error accepting new client",ex);
            }

            // Get Next client:
            listener.BeginAcceptTcpClient(AcceptTcpClientAsync, listener);
        }

    }
}
