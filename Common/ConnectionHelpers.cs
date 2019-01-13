using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConnectionHelpers
    {
        enum CommandType
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

        class TaskInfo
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

        class TaskInfoResult<T> : TaskInfo
        {
            public T result;

            public static TaskInfoResult<V> Result<V>(V resultData)
            {
                return new TaskInfoResult<V>()
                {
                    success = true,
                    result = resultData
                };
            }
        }

        public const byte CMD_SEPERATOR = 0xAB;

        int ParseCommandHeader(byte[] headerBuffer, out CommandType cmd , int startIndex =0)
        {
            int result;

            if (headerBuffer.Length < (2 + 5) ||
                headerBuffer[startIndex] != CMD_SEPERATOR ||
                headerBuffer[startIndex + 6] != CMD_SEPERATOR)
            {
                result = -1;
                cmd = CommandType.ERROR;
            }
            else
            {
                cmd = (CommandType)headerBuffer[startIndex + 1 ];
                result = BitConverter.ToInt32(headerBuffer, startIndex + 2);
            }

            return result;
        }

        bool ParseCommandData(int dataLength, byte[] dataBuffer, out string dataResult, int startDataBuffer = 0)
        {
            bool result = false;

            try
            {
                if (dataBuffer.Length < (2 + dataLength) ||
                            dataBuffer[startDataBuffer] != CMD_SEPERATOR ||
                            dataBuffer[startDataBuffer + dataLength + 1] != CMD_SEPERATOR)
                {
                    dataResult = "Unrecognized data format. Expected length: " + dataLength;
                }
                else
                {
                    dataResult = Encoding.UTF8.GetString(dataBuffer, startDataBuffer + 1, dataLength);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                dataResult = ex.ToString();
            }

            return result;
        }

        void CommandSerialize(CommandType cmd, string Data, out byte[] headerBuffer, out byte[] dataBuffer)
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

        async Task<TaskInfo> SendCommand(CommandType cmd, string Data, TcpClient client)
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

        async Task<TaskInfo> RecieveCommandHeader(TcpClient client)
        {
            // if data length > 1024 or header not valid, just close the client (not here, in the algo!).
            TaskInfo result = null;

            return result;
        }

        async Task<TaskInfo> RecieveCommandData(TcpClient client)
        {
            TaskInfo result = null;

            return result;
        }
    }
}
