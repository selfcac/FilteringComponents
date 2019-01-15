using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ConnectionHelpers
    {
        public enum CommandType
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

        public const byte CMD_SEPERATOR = 0xAB;

        public struct CommandInfo
        {
            public CommandType cmd;
            public int dataLength;
            public string data;
        }

        // ========= ========= ========= Task helpers

        public class TaskInfo
        {         
            public bool success;
            public string error;

            public static TaskInfo Fail(string reason)
            {
                return new TaskInfo()
                {
                    success = false, error = reason
                };
            }

            public static TaskInfo Success()
            {
                return new TaskInfo()
                {
                    success = true,
                };
            }

            public static implicit operator bool(TaskInfo e)
            {
                return e.success;
            }         
        }

        public class TaskInfoResult<T> : TaskInfo
        {
            public T result;

            public static TaskInfoResult<T> Result(T resultData)
            {
                return new TaskInfoResult<T>()
                {
                    success = true,
                    result = resultData
                };
            }

            public static implicit operator T(TaskInfoResult<T> o)
            {
                return o.result;
            }
        }

        // ========= ========= ========= Parsers

        static TaskInfo ParseCommandHeader(byte[] headerBuffer, int startIndex =0)
        {
            TaskInfo result = null;

            if (headerBuffer.Length < (2 + 5) ||
                headerBuffer[startIndex] != CMD_SEPERATOR ||
                headerBuffer[startIndex + 6] != CMD_SEPERATOR)
            {
                result =  TaskInfo.Fail("Invalid header structure");
            }
            else
            {
                CommandInfo o = new CommandInfo();

                o.cmd = (CommandType)headerBuffer[startIndex + 1 ];
                o.dataLength = BitConverter.ToInt32(headerBuffer, startIndex + 2);

                result = TaskInfoResult<CommandInfo>.Result(o);
            }

            return result;
        }

        static TaskInfo ParseCommandData(int dataLength, byte[] dataBuffer, int startDataBuffer = 0)
        {
            TaskInfo result = null;

            try
            {
                if (dataBuffer.Length < (2 + dataLength) ||
                            dataBuffer[startDataBuffer] != CMD_SEPERATOR ||
                            dataBuffer[startDataBuffer + dataLength + 1] != CMD_SEPERATOR)
                {
                    result = TaskInfo.Fail("Unrecognized data format. Expected length: " + dataLength);
                }
                else
                {
                    result = TaskInfoResult<string>.Result(
                        Encoding.UTF8.GetString(dataBuffer, startDataBuffer + 1, dataLength)
                        );
                }
            }
            catch (Exception ex)
            {
                result = TaskInfo.Fail(ex.ToString());
            }

            return result;
        }

        static void CommandSerialize(CommandType cmd, string Data, out byte[] headerBuffer, out byte[] dataBuffer)
        {
            headerBuffer = new byte[2+5];

            byte[] dataBytes = Encoding.UTF8.GetBytes(Data);
            dataBuffer = new byte[2 + dataBytes.Length];

            // Seperators:
            // ============================================
            headerBuffer[0] = headerBuffer[6] = CMD_SEPERATOR;
            dataBytes[0] = dataBytes[1 + dataBytes.Length] = CMD_SEPERATOR;

            // Dataheader:
            // ============================================

            headerBuffer[1] = (byte)cmd;
            byte[] datalengthBytes = BitConverter.GetBytes(dataBytes.Length);
            for (int i = 0; i < 4; i++) headerBuffer[1 + i] = datalengthBytes[i];

            // Data:
            // ============================================
            for (int i = 0; i < dataBytes.Length; i++) dataBuffer[1 + i] = dataBytes[i];
        }

        // ========= ========= ========= High level 

        public static async Task<TaskInfo> SendCommand(CommandType cmd, string Data, TcpClient client)
        {
            TaskInfo result;

            try
            {
                byte[] headerBytes, commandBytes;
                CommandSerialize(cmd, Data, out headerBytes, out commandBytes);

                await client.GetStream().WriteAsync(headerBytes, 0, headerBytes.Length);

                await client.GetStream().WriteAsync(commandBytes, 0, commandBytes.Length);

                result = TaskInfo.Success();
            }
            catch (Exception ex)
            {
                result = TaskInfo.Fail(ex.ToString());
            }

            return result;
        }

        // if data length > 1024 or header not valid, just close the client (not here, in the algo!).
        public static async Task<TaskInfo> RecieveCommandHeader(TcpClient client)
        {
            TaskInfo result = null;

            try
            {
                byte[] headerBytes = new byte[2 + 5];

                await client.GetStream().ReadAsync(headerBytes, 0, headerBytes.Length);

                result = ParseCommandHeader(headerBytes);

            }
            catch (Exception ex)
            {
                result = TaskInfo.Fail(ex.ToString());
            }

            return result;
        }

        public static async Task<TaskInfo> RecieveCommandData(TcpClient client, CommandInfo cmd)
        {
            TaskInfo result = null;

            try
            {
                byte[] dataBytes = new byte[2 + cmd.dataLength];

                await client.GetStream().ReadAsync(dataBytes, 0, dataBytes.Length);

                result = ParseCommandData(cmd.dataLength, dataBytes);

                if (result)
                    cmd.data = (result as TaskInfoResult<string>).result;

            }
            catch (Exception ex)
            {
                result = TaskInfo.Fail(ex.ToString());
            }

            return result;
        }
    }
}
