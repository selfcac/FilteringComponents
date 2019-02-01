﻿using System;
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

        public object blockLogLock = new object();
        public string[] whitelist = new string[0];
        public bool mappingMode;

        public void LogBlocked(string domain)
        {
            lock (blockLogLock)
            {
                File.AppendAllText( Config.Instance.blocklogFile.FullName, domain + Environment.NewLine);
            }
        }

        public bool IsWhitelisted(string domain)
        {
            foreach (string line in whitelist)
            {
                if (domain.Equals(line)) return true; // (exact case 2)
                else if (!string.IsNullOrEmpty(line))
                {
                    int position = domain.IndexOf(line);
                    if (position < 0)
                        continue;

                    int lengthDiff = domain.Length - line.Length;

                    if (
                        line[0] == '.' &&
                        (
                            (position == lengthDiff) ||         //  *  + ".domain" (subdomain)
                            (position == 1 && lengthDiff == 1 ) // "domain" (exact case 1)
                        ))
                            return true; 
                }
            }
            return false;
        }

        public void StartServer(int port)
        {
            // Data about proxy filtering:
            FileInfo whitelistFile = Common.Config.Instance.whitelistFile;
            if (whitelistFile.Exists)
            {
                whitelist = File.ReadAllLines(whitelistFile.FullName);
            }
            mappingMode = Config.Instance.proxyMappingMode;

            log.i("Whitelist has " + whitelist.Length + " domains.");
            log.i("Mapping mode? " + mappingMode);

            // Proxy main thread start:
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
                TcpSession.Start(client,this);
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
