using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using C = Common.ConnectionHelpers;

namespace ControlPanelForSafeControl
{
    static class CommandHandler
    {
        async public static void HandleClient(TcpClient client) {
            Logger log = new Logger("c-" + ((IPEndPoint)client.Client.RemoteEndPoint).Port);

            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;

            try
            {
                log.i("Getting header");
                C.TaskInfo task = await C.RecieveCommandHeader(client);
                if (task)
                {
                    C.CommandInfo cmdInfo = (task as C.TaskInfoResult<C.CommandInfo>).result;
                    if (cmdInfo.dataLength < 0)
                        throw new Exception("Task is corrupted (data length is -1)");

                    if (cmdInfo.dataLength > 1024)
                    {
                        log.e("Command data is more than 1KB\n Got:" + cmdInfo.dataLength);
                    }
                    else
                    {
                        log.i("Getting data");
                        C.TaskInfo dataTask = await C.RecieveCommandData(client, cmdInfo);
                        if (dataTask)
                        {
                            log.i("[OK] " + cmdInfo.cmd.ToString() + ": " + cmdInfo.data);

                            // Return echo of command:
                            // TODO: handle each command:
                            await C.SendCommand(cmdInfo.cmd, cmdInfo.data + " " + DateTime.Now, client);

                            await Task.Delay(5000); // wait for client to read response;
                        }
                        else
                        {
                            log.e("Can't read command data\n" + dataTask.error);
                        }
                    }
                }
                else
                {
                    log.e("Can't read header\n" + task.error);
                }
            }
            catch (Exception ex)
            {
                log.e("Client had errors", ex);
            }
            finally
            {
                client.Close();
            }
            
        }


        
    }
}
