using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanelForSafeControl
{
    class TcpServer
    {
        Logger log = new Logger("server");
        TcpListener tcpHttpServer;

        public void StartServer(int port)
        {
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
                log.i("Got new client");

                // Start processing:
                CommandHandler.HandleClient(client);
            }
            catch (ObjectDisposedException ex)
            {
                // When stopping the listener (no actual client)
                return;
            }
            catch (Exception ex)
            {
                log.e("Error accepting new client", ex);
            }

            // Get Next client:
            listener.BeginAcceptTcpClient(AcceptTcpClientAsync, listener);
        }


        
    }
}
