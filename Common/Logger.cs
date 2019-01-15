using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Logger
    {
        private static object _WriteSyncLock = new object();

        public enum LogLevel
        {
            INFO = ConsoleColor.White,
            WARNING = ConsoleColor.Yellow,
            ERROR = ConsoleColor.Red,
            CRITICAL = ConsoleColor.Blue
        }

        string lvlString(LogLevel lvl)
        {
            string result = "???";
            switch (lvl)
            {
                case LogLevel.INFO:
                    result = "info";
                    break;
                case LogLevel.WARNING:
                    result = "warn";
                    break;
                case LogLevel.ERROR:
                    result = "err";
                    break;
                case LogLevel.CRITICAL:
                    result = "crit";
                    break;
            }
            return result;
        }

        string _myTag;
        bool _writeToConsole = false;

        public Logger(string tag, bool writeToConsole = true)
        {
            _myTag = tag;
            _writeToConsole = writeToConsole;
        }

        public void log(string message, LogLevel level)
        {

            if (_writeToConsole)
            {
                lock (_WriteSyncLock)
                {
                    Console.Write("[{0}]\t{1}\t", DateTime.Now.ToString("HH:mm:ss"), _myTag);
                    Console.ForegroundColor = (ConsoleColor)level;
                    Console.Write(lvlString(level));
                    Console.ResetColor();
                    Console.WriteLine("\t{0}", message);
                    Console.ResetColor();
                }
            }
        }

        public void log(string message, Exception ex, LogLevel level)
        {
            string ErrorMsg = "";
            string Stack = ex.StackTrace;
            while (ex != null)
            {
                ErrorMsg += "(*) " + ex.Message + "\r\n";
                ex = ex.InnerException;
            }

            log(message + "\r\n" + ErrorMsg + Stack, level);
        }

        public void i(string msg) { log(msg, LogLevel.INFO); }
        public void w(string msg) { log(msg, LogLevel.WARNING); }
        public void e(string msg) { log(msg, LogLevel.ERROR); }
        public void e(string msg, Exception ex) { log(msg, ex, LogLevel.ERROR); }
        public void c(string msg) { log(msg, LogLevel.CRITICAL); }
        public void c(string msg, Exception ex) { log(msg, ex, LogLevel.CRITICAL); }
    }

}
