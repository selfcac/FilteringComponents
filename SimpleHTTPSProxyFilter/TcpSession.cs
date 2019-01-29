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

            // Proxy Commands:
            public byte[] commandBuffer;
            public int commandIndex = 0;

            public void CleanAfter(TcpClient toClose)
            {
                log.i("Cleaning up");

                isClosed = true;
                toClose.Close();

                client = null;
                cStream = null;
                remote = null;
                rStream = null;

                clientBuffer = null;
                remoteBuffer = null;
                commandBuffer = null;

                ReqHeaders = null;
            }
        }

        enum HEADER_INFO 
        {
            METHOD = 0, HOST_URL = 1, HTTP_VERSION=2
        }

        public static void Start(TcpClient client)
        {
            Bundle b = new Bundle()
            {
                log = new Logger("c-" + ((IPEndPoint)client.Client.RemoteEndPoint).Port),
                client = client,
                cStream = client.GetStream(),
            };

            b.log.i("Waiting for headers data.");
            b.cStream.BeginRead(
                b.clientBuffer, 0, bufferSize,
                FirstHeaderReadAsync, b);
        }


        static void FirstHeaderReadAsync(IAsyncResult ar)
        {
            Bundle b = ar.AsyncState as Bundle;
            b.log.i("Reading request");
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
                // O.W (direct access) we get relative path
                string requestPath = b.HEADER[(int)HEADER_INFO.HOST_URL];

                if (requestPath.StartsWith("/"))
                {
                    b.log.i("Got command: '" + requestPath + "'");
                    string result = "<Init command response>";
                    FileInfo blocklog = Common.Config.Instance.blocklogFile;

                    if (requestPath == "/blocklog")
                    {
                        try
                        {
                            if (blocklog.Exists)
                            {
                                result = File.ReadAllText(blocklog.FullName);
                            }
                            else
                            {
                                result = "Block log doesn't exist.";
                            }
                        }
                        catch (Exception ex)
                        {
                            result = ex.Message;
                        }
                    }
                    else if (requestPath == "/clearlog")
                    {
                        result = "Log cleared.";
                        try
                        {
                            if (blocklog.Exists)
                                blocklog.Delete();
                            File.WriteAllText(
                                blocklog.FullName,
                                "Log cleared at " + DateTime.Now.ToString() + Environment.NewLine);
                        }
                        catch (Exception ex)
                        {
                            result = ex.Message;                            
                        }
                    }
                    else
                    {
                        result = "Allowd paths: '/blocklog' and '/clearlog'.";
                    }
                    SendHttpResponse(b, result);
                }
                else
                {
                    b.log.i("Got requst: '" + requestPath + "'");
                    Uri uri = new Uri(requestPath);

                    // Open TCP to remote 
                    MakeRemoteConnection(b, uri.Host); // authority may contain ports! --> <host>:<port>

                    byte[] HeadersBytes = Encoding.ASCII.GetBytes(b.ReqHeaders);

                    // Send headers to remote server
                    b.log.i("Sending plain headers");
                    b.cStream.Write(HeadersBytes, 0, HeadersBytes.Length);

                    //  Remote ==> Proxy ==> Client
                    b.rStream.BeginRead(b.remoteBuffer, 0, bufferSize,
                        forwardToClient, b); 
                }
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

        static void SendHttpResponse(Bundle b, string TextContent)
        {
            b.commandBuffer = Encoding.ASCII.GetBytes(TextContent);

            string headers = "HTTP/1.1 200 OK\r\n" +
                "Server: SimpleHTTPSProxyFilter\r\n" +
                "Content-Type: text/plain\r\n" +
                "Connection: Closed\r\n" +
                "Content-Length: $\r\n\r\n".Replace("$", b.commandBuffer.Length.ToString());

            byte[] headersBytes = Encoding.ASCII.GetBytes(headers);

            b.cStream.Write(headersBytes, 0, headersBytes.Length);

            b.cStream.BeginWrite(b.commandBuffer, 0, b.commandBuffer.Length, SendHttpBody, b);
        }

        static void SendHttpBody(IAsyncResult ar)
        {
            Bundle b = ar.AsyncState as Bundle;
            b.cStream.EndWrite(ar);

            b.CleanAfter(b.client);
        }
    }
}
