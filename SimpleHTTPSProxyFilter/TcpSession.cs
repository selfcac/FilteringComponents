using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SimpleHTTPSProxyFilter
{
    static class TcpSession
    {
        const int bufferSize = 5 * 1024 * 1024;
        public class Bundle
        {
            public Logger log;

            public bool isSSL = false;
            public bool isClosed = false;

            // Browser to Proxy Connections:
            public TcpClient client;
            public NetworkStream cStream;
            public byte[] clientBuffer = new byte[bufferSize];
            public string ReqHeaders = "";
            public string[] HEADER;

            // Proxy to server Connection :
            public TcpClient remote;
            public NetworkStream rStream;
            public byte[] remoteBuffer = new byte[bufferSize];

            public void CleanAfter(TcpClient toClose)
            {
                isClosed = true;
                toClose.Close();

                client = null;
                cStream = null;
                remote = null;
                rStream = null;

                clientBuffer = null;
                remoteBuffer = null;

                ReqHeaders = null;
            }
        }

        enum HEADER_INFO 
        {
            METHOD = 0, HOST_URL = 1, HTTP_VERSION=2
        }

        public static void Start(TcpClient client)
        {
            Bundle clientBundle = new Bundle()
            {
                log = new Logger("c-" + ((IPEndPoint)client.Client.RemoteEndPoint).Port),
                client = client,
                cStream = client.GetStream(),
            };

            clientBundle.cStream.BeginRead(
                clientBundle.clientBuffer, 0, bufferSize,
                FirstHeaderReadAsync, clientBundle);
        }


        static void FirstHeaderReadAsync(IAsyncResult ar)
        {
            Bundle b = ar.AsyncState as Bundle;

            try
            {
                int bytesRead = b.cStream.EndRead(ar);
                if (bytesRead == 0) return;
                b.ReqHeaders += Encoding.ASCII.GetString(b.clientBuffer, 0, bytesRead);

                if (b.ReqHeaders.EndsWith("\r\n\r\n"))
                {
                    string[] rows = b.ReqHeaders.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    b.HEADER = rows[0].Split(' ');

                    if (b.HEADER[(int)HEADER_INFO.METHOD] == "CONNECT")
                    {
                        b.isSSL = true;
                        HandleSSL(b);
                    }
                    else
                    {
                        b.isSSL = false;
                        HandlePlain(b);
                    }
                }
                else
                {
                    // Read more: 
                    b.cStream.BeginRead(
                        b.clientBuffer, 0, bufferSize,
                        FirstHeaderReadAsync, b);
                }
            }
            catch (Exception ex)
            {
                b.log.e("Error Reading first headers\n", ex);
            }
        }


        static void HandlePlain(Bundle b)
        {
            try
            {
                // In http the url is full url (because we are proxy)
                Uri uri = new Uri(b.HEADER[(int)HEADER_INFO.HOST_URL]);

                // Open TCP to remote 
                MakeRemoteConnection(b, uri.Authority);

                byte[] HeadersBytes = Encoding.ASCII.GetBytes(b.ReqHeaders);

                // Send headers to remote server
                b.log.i("Sending plain headers");
                b.cStream.Write(HeadersBytes, 0, HeadersBytes.Length);

                //  Remote ==> Proxy ==> Client
                b.rStream.BeginRead(b.remoteBuffer, 0, bufferSize,
                    forwardToClient, b);
            }
            catch (Exception ex)
            {
                b.log.e("Error handle plain\n", ex);
            }
        }

        static void HandleSSL(Bundle b)
        {
            try
            {
                // Send OK, we are ready to secure the connection.
                b.log.i("Sending CONNECT ok");
                byte[] CONNECT_RESPONSE =
                    Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\n\r\n");
                b.cStream.Write(CONNECT_RESPONSE, 0, CONNECT_RESPONSE.Length);

                // In connect the url is "<domain>:443"
                string remoteHost = b.HEADER[(int)HEADER_INFO.HOST_URL].Split(':').First();

                // Open TCP to remote 
                MakeRemoteConnection(b, remoteHost);

                // Client ==> Proxy ==> Remote
                b.cStream.BeginRead(b.clientBuffer, 0, bufferSize,
                    forwardToRemote, b);

                // Remote ==> Proxy ==> Client
                b.rStream.BeginRead(b.remoteBuffer, 0, bufferSize,
                   forwardToClient, b);
            }
            catch (Exception ex)
            {
                b.log.e("Error handle SSL\n", ex);
            }
        }

        static void MakeRemoteConnection(Bundle b, string host)
        {
            b.log.w("Conencting to : " + host);
            b.remote = new TcpClient(host, b.isSSL ? 443 : 80);
            b.rStream = b.remote.GetStream();
        }

        static void forwardToClient(IAsyncResult ar)
        {
            Bundle b = ar.AsyncState as Bundle;
            if (b.isClosed) return;
            try
            {
                int bytesRead = b.rStream.EndRead(ar);
                if (bytesRead == 0)
                {
                    b.CleanAfter(b.client);
                    b.log.i("Remote connection closed");
                    return;
                }

                b.log.i("Forward to client " + ((b.isSSL) ? "[SSL] " : "") + bytesRead + "B");
                b.cStream.WriteAsync(b.remoteBuffer, 0, bytesRead);

                if (b.rStream.CanRead)
                    b.rStream.BeginRead(b.remoteBuffer, 0, bufferSize,
                        forwardToClient, b);
            }
            catch (Exception ex)
            {

                b.log.e("Error forward to client\n", ex);
            }
        }

        static void forwardToRemote(IAsyncResult ar)
        {
            Bundle b = ar.AsyncState as Bundle;
            if (b.isClosed) return;
            try
            {
                int bytesRead = b.cStream.EndRead(ar);
                if (bytesRead == 0)
                {
                    b.CleanAfter(b.remote);
                    b.log.i("Local connection closed");
                    return;
                }

               b.log.i("Forward to remote " + ((b.isSSL) ? "[SSL] " : "") + bytesRead + "B");
                b.rStream.WriteAsync(b.clientBuffer, 0, bytesRead);

                if (b.cStream.CanRead)
                    b.cStream.BeginRead(b.clientBuffer, 0, bufferSize,
                        forwardToRemote, b);
            }
            catch (Exception ex)
            {
                b.log.e("Error forward to remote\n", ex);
            }
        }

    }
}
