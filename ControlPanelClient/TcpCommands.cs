using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using C = Common.ConnectionHelpers;

namespace ControlPanelClient
{
    public class TcpCommands
    {
        public static TcpClient getClient()
        {
            TcpClient client = new TcpClient("127.0.0.1", C.ControlPanelPort);
            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;

            return client;
        }

        public static string evilPayLoad()
        {
            string s20 = "01234567890123456789";
            string result = "";
            for (int i = 0; i < 100; i++) result += s20;
            return result;
        }

        async public static Task<string> Echo()
        {
            using (TcpClient client = getClient())
            {
                string result = "";
                try
                {
                    C.TaskInfo task = await C.SendCommand(C.CommandType.ECHO, "ECHO", client);
                    if (task)
                    {
                        C.TaskInfo headerTask = await C.RecieveCommandHeader(client);
                        if (headerTask)
                        {
                            C.CommandInfo cmdInfo = (headerTask as C.TaskInfoResult<C.CommandInfo>).result;
                            if (cmdInfo.dataLength < 0)
                                throw new Exception("Task is corrupted (data length is -1)");

                            if (cmdInfo.dataLength > 1024)
                            {
                                result = "Command data is more than 1KB\n Got:" + cmdInfo.dataLength;
                            }
                            else
                            {
                                C.TaskInfo dataTask = await C.RecieveCommandData(client, cmdInfo);
                                if (dataTask)
                                {
                                    result = cmdInfo.data;
                                }
                                else
                                {
                                    result = "Can't read command data\n" + dataTask.error;
                                }
                            }
                        }
                        else
                        {
                            result = "Header could not recieved\n" + headerTask.error;
                        }
                    }
                    else
                    {
                        result = "Task not sent\n" + task.error;
                    }

                }
                catch (Exception ex)
                {
                    result = ex.ToString();
                }
                finally
                {
                    client.Close();
                }

                return result;
            }
        }
    }
}
