using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConnectionHelpers
    {
        enum CommandType
        {
            ERROR,

            // No data:
            PROXY_START, PROXY_END,
            BLOCKLOG_SHOW, BLOCKLOG_DELETE,
            LOCKED_CHECK,

            // Has data:
            ECHO,
            ADD_URL,
            CHANGE_PASSWORD,
            LOCK,
        }

        struct CommandInfo
        {
            public CommandType cmd;
            public string data;
        }

        public const byte CMD_SEPERATOR = 0xAB;

        CommandInfo errorInfo(string Error)
        {
            return new CommandInfo()
            {
                cmd = CommandType.ERROR,
                data = Error
            };
        }

        CommandInfo ReadCommandBytes(
            byte[] headerBuffer, byte[] dataBuffer,
            int startHeaderIndex = 0, int startDataBuffer = 0)
        {
            CommandInfo result = errorInfo("Init value");

            if (headerBuffer.Length < (2+5) ||
                headerBuffer[startHeaderIndex] != CMD_SEPERATOR ||
                headerBuffer[startHeaderIndex + 6] != CMD_SEPERATOR)
            {
                result = errorInfo("Unrecognized header format.");
            }
            else
            {
                CommandType cmd = (CommandType)headerBuffer[startHeaderIndex];
                int dataLength = BitConverter.ToInt32(headerBuffer, startHeaderIndex + 1);

                if (dataBuffer.Length < (2+ dataLength) ||
                    dataBuffer[startDataBuffer] != CMD_SEPERATOR ||
                    dataBuffer[startDataBuffer + dataLength + 1] != CMD_SEPERATOR)
                {
                    result = errorInfo("Unrecognized data format. Expected length: " + dataLength);
                }
                else
                {
                    if (dataLength > 1024)
                    {
                        result = errorInfo("Data length more than 1024, got:" + dataLength);
                    }
                    else
                    {
                        result = new CommandInfo()
                        {
                            cmd = cmd,
                            data = Encoding.UTF8.GetString(dataBuffer, startDataBuffer + 1, dataLength)
                        };
                    }
                }
            }
            return result;
        }

        void BytesOfCommand(CommandInfo cmdInfo, out byte[] headerBuffer, out byte[] dataBuffer)
        {
            headerBuffer = new byte[2+5];

            byte[] dataBytes = Encoding.UTF8.GetBytes(cmdInfo.data);
            dataBuffer = new byte[2 + dataBytes.Length];

            // Seperators:
            // ============================================
            headerBuffer[0] = headerBuffer[6] = CMD_SEPERATOR;
            dataBytes[0] = dataBytes[1 + dataBytes.Length] = CMD_SEPERATOR;

            // Dataheader:
            // ============================================

            headerBuffer[1] = (byte)cmdInfo.cmd;
            byte[] datalengthBytes = BitConverter.GetBytes(cmdInfo.data.Length);
            for (int i = 0; i < 4; i++) headerBuffer[1 + i] = datalengthBytes[i];

            // Data:
            // ============================================



        }
    }
}
