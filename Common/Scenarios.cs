﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Common.ConnectionHelpers;

namespace Common
{
    public static class Scenarios
    {
        public static TcpClient getTcpClient()
        {
            TcpClient client = new TcpClient("127.0.0.1", Config.ControlPanelPort);
            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;

            return client;
        }

        async public static Task<string> runCommand(CommandType type, string Data)
        {
            using (TcpClient client = getTcpClient())
            {
                string result = "";
                try
                {
                    TaskInfo task = await SendCommand(type, Data, client);
                    if (task)
                    {
                        TaskInfo headerTask = await RecieveCommandHeader(client);
                        if (headerTask)
                        {
                            CommandInfo cmdInfo = (headerTask as TaskInfoResult<CommandInfo>).result;
                            if (cmdInfo.dataLength < 0)
                                throw new Exception("Task is corrupted (data length is -1)");

                            if (cmdInfo.dataLength > 1024)
                            {
                                result = "Command data is more than 1KB\n Got:" + cmdInfo.dataLength;
                            }
                            else
                            {
                                TaskInfo dataTask = await RecieveCommandData(client, cmdInfo);
                                if (dataTask)
                                {
                                    result = cmdInfo.data;
                                }
                                else
                                {
                                    result = "Can't read command data\n" + dataTask.eventReason;
                                }
                            }
                        }
                        else
                        {
                            result = "Header could not recieved\n" + headerTask.eventReason;
                        }
                    }
                    else
                    {
                        result = "Task not sent\n" + task.eventReason;
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

        /*
         * public enum CommandType
        {
            ERROR,

            // Events with no data:
            PROXY_START, PROXY_END,
            BLOCKLOG_SHOW, BLOCKLOG_DELETE,
            LOCKED_CHECK,

            // Events with extra data:
            ECHO,
            ADD_URL,
            CHANGE_PASSWORD,
            LOCK,
        }
        */

       

        public delegate string endCommandMethod(CommandInfo cmd);
        public static Dictionary<CommandType, endCommandMethod> serverHelpers = 
            new Dictionary<CommandType, endCommandMethod>()
        {
            { CommandType.ECHO, Echo_Server },
            { CommandType.PROXY_START, ProxyStart_Server},
            { CommandType.PROXY_END, ProxyEnd_Server},
            { CommandType.CHANGE_PASSWORD, ChangePass_Server},
        };

        public static endCommandMethod HandleCommand(CommandType type)
        {
            if (serverHelpers.ContainsKey(type))
                return serverHelpers[type];
            else
                return Default_Server;
        }

        public static string chopString(string source, int maxLen = 1024)
        {
            if (source.Length > maxLen)
            {
                return source.Substring(0, 1024 - 3) + "...";
            }
            else
            {
                return source;
            }
        }

        public static string Default_Server(CommandInfo cmdInfo)
        {
            return "Server Handler for command not found! Define in Scenarios.cs";
        }

        // === === === === === ECHO === === === === === === 

        public async static Task<string> Echo_Client()
        {
            return await runCommand(CommandType.ECHO, "~Echo~");
        }

        public static string Echo_Server(CommandInfo cmdInfo)
        {
            return cmdInfo.data + " " + DateTime.Now;
        }

        // === === === === === PROXY_START === === === === === === 

        public async static Task<string> ProxyStart_Client()
        {
            return await runCommand(CommandType.PROXY_START, "");
        }

        public static string ProxyStart_Server(CommandInfo cmdInfo)
        {
            TaskInfo result = SystemUtils.StartService(Config.PROXY_SERVICE_NAME);
            return chopString("Sucess? " + result.success.ToString() + ", " + result.eventReason);
        }

        // === === === === === PROXY_END === === === === === === 

        public async static Task<string> ProxyEnd_Client()
        {
            return await runCommand(CommandType.PROXY_END, "");
        }

        public static string ProxyEnd_Server(CommandInfo cmdInfo)
        {
            TaskInfo result = SystemUtils.StopService(Config.PROXY_SERVICE_NAME);
            return chopString("Sucess? " + result.success.ToString() + ", " + result.eventReason);
        }

        // === === === === === CHANGE_PASSWORD === === === === === === 

        public async static Task<string> ChangePass_Client(string password)
        {
            return await runCommand(CommandType.CHANGE_PASSWORD, password);
        }

        public static string ChangePass_Server(CommandInfo cmdInfo)
        {
            TaskInfo result = TaskInfo.Fail("Can't change to empty password");
            if (!string.IsNullOrEmpty(cmdInfo.data))
                result = SystemUtils.ChangeUserPassword(Config.ADMIN_USERNAME, cmdInfo.data);

            return chopString("Password changed? " + result.success.ToString() + ", " + result.eventReason);
        }

    }

}
