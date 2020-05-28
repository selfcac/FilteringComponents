using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using static Common.ConnectionHelpers;

namespace Common
{
    public static class Scenarios
    {
        static string defaultNewAdminPass = "1234";

        public enum CommandType
        {
            ERROR,

            // User actions:
            ECHO,           
            ALLOWED_COMMAND,
            REFRESH_CONFIG,
            
            // Actions given usb (reset file *.psw)
            RESET_PASS_USB,
            RESET_UNLOCK_USB,

            // Admin actions:
            CHANGE_PASSWORD,            
            LOCK,                       
            ADMIN_COMMAND,

        }

        public enum LOCK_ACTION
        {
            START_LOCK,
            RESET_PASS,
            RESET_LOCK,
        }

        public static void logLockAction(LOCK_ACTION action)
        {
            try
            {
                File.AppendAllLines(Config.Instance.lockingHistoryFile, new[] { DateTime.Now.ToString() + "|" + action.ToString() });
            }
            catch (Exception ex)
            {
                // fail silently
            }
        }

        public enum CommandActions
        {
            START, STOP, SHOW, DELETE, CHECK
        }

        public static TcpClient getTcpClient()
        {
            TcpClient client = new TcpClient("127.0.0.1", Config.Instance.ControlPanelPort);
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

        public delegate string endCommandMethod(CommandInfo cmd);
        public static Dictionary<CommandType, endCommandMethod> serverHelpers =
            new Dictionary<CommandType, endCommandMethod>()
        {
            { CommandType.ECHO, Echo_Server },
            { CommandType.CHANGE_PASSWORD, ChangePass_Server},
            { CommandType.REFRESH_CONFIG, RefreshConfig_Server},

            { CommandType.LOCK, Lock_Server},
            { CommandType.ALLOWED_COMMAND, Allowed_command_server},
            { CommandType.ADMIN_COMMAND, Admin_command_server},

            { CommandType.RESET_PASS_USB, ResetPass_Server},
            {CommandType.RESET_UNLOCK_USB, RESET_UNLOCK_Server },
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
            return cmdInfo.data + " " + Config.Instance.ECHO_SALT  + ", " + DateTime.Now;
        }

        // === === === === === CHANGE_PASSWORD === === === === === === 

        public async static Task<string> ChangePass_Client(string password)
        {
            return await runCommand(CommandType.CHANGE_PASSWORD, password);
        }

        public static string ChangePass_Server(CommandInfo cmdInfo)
        {
            TaskInfo unlockedStatus = isLocked();
            if (unlockedStatus)
            {
                return LockedFormat(unlockedStatus);
            }
            else
            {
                TaskInfo result = TaskInfo.Fail("Can't change to empty password");
                result = _changePassword(cmdInfo.data, result);

                return chopString("Password changed? " + result.success.ToString() + ", " + result.eventReason);
            }

        }

        private static TaskInfo _changePassword(string password, TaskInfo result)
        {
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    File.AppendAllLines(Config.Instance.auditFile, new[] {
                        string.Format("[{0}] Start changing to pass: {1}",DateTime.Now, password)
                        });
                    result = SystemUtils.ChangeUserPassword(Config.Instance.ADMIN_USB_USERNAME, password);
                    if (result)
                    {
                        File.AppendAllLines(Config.Instance.auditFile, new[] {
                            string.Format("[{0}] Successfuly changed to pass: {1}",DateTime.Now, password)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                result = TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        // === === === === === LOCK            === === === === === === 

        static TaskInfo isLocked()
        {
            TaskInfo isLocked = TaskInfo.Fail("No issue (init)"); // unlocked on error by default
            try
            {
                if (File.Exists(Config.Instance.unlockFile))
                {
                    DateTime unlock = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));
                    if (DateTime.TryParse(File.ReadAllText(Config.Instance.unlockFile), out unlock))
                    {
                        if (unlock > DateTime.Now)
                        {
                            isLocked = TaskInfoResult<DateTime>.Result(unlock);
                        }
                        else
                        {
                            isLocked = TaskInfo.Fail("Lock expired");
                        }
                    }
                }
                else
                {
                    isLocked = TaskInfo.Fail("No file exist");
                }
            }
            catch (Exception ex)
            {
                isLocked = TaskInfo.Fail(ex.ToString());
            }
            return isLocked;
        }

        static string LockedFormat(TaskInfo unlockTask)
        {
            DateTime locked = (unlockTask as TaskInfoResult<DateTime>).result;
            return "Locked until: " + locked.ToString() + ", Left: " +
                string.Format("{0:%d}days {0:%h}h {0:%m}m {0:%s}sec", (locked - DateTime.Now));
        }

        public async static Task<string> Lock_Client(bool check, DateTime lockTime)
        {
            return await runCommand(CommandType.LOCK, check ? CommandActions.CHECK.ToString() : lockTime.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo));
        }

        public static string Lock_Server(CommandInfo cmdInfo)
        {
            string result = "Unkown lock result";

            TaskInfo unlockedStatus = isLocked();
            if (cmdInfo.data == CommandActions.CHECK.ToString())
            {
                if (unlockedStatus)
                {
                    result = LockedFormat(unlockedStatus);
                }
                else
                {
                    result = "Unlocked!, " + unlockedStatus.eventReason;
                }
            }
            else
            {
                // Lock it!
                try
                {
                    DateTime date = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));
                    if (DateTime.TryParse(cmdInfo.data,
                        System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.AssumeLocal,
                        out date))
                    {
                        if (!unlockedStatus) // Only if not already locked!
                        {
                            if (date > DateTime.Now)
                            {
                                result = _LockDate(date);
                                logLockAction(LOCK_ACTION.START_LOCK);
                            }
                            else
                            {
                                result = "Please choose *future* time";
                            }
                        }
                        else
                        {
                            result = LockedFormat(unlockedStatus);
                        }
                    }
                    else
                    {
                        result = "bad_date_str. got: " + cmdInfo.data;
                    }
                }
                catch (Exception ex)
                {
                    result = "Failed lock: " + ex.Message;
                }

            }

            return chopString(result);
        }

        private static string _LockDate(DateTime date)
        {
            string result;
            string unlockPath = Config.Instance.unlockFile;
            File.AppendAllText(Config.Instance.auditFile, "(*) Locking until '" + date.ToString() + "'" + Environment.NewLine);
            if (File.Exists(unlockPath))
                File.Delete(unlockPath);
            File.WriteAllText(unlockPath, date.ToString());
            result = "Sucess! Locked to " + date.ToString();
            return result;
        }

        // === === === === === RESET_PASS            === === === === === === 

        public async static Task<string> ResetPass_Client(string filename)
        {
            return await runCommand(CommandType.RESET_PASS_USB, filename);
        }

        public static string ResetPass_Server(CommandInfo cmdInfo)
        {
            TaskInfo result = TaskInfo.Fail("Init");
            try
            {
                string userFilePath = cmdInfo.data;
                Func<TaskInfo> onUsbValidates = new Func<TaskInfo>(
                    () => { return _changePassword(defaultNewAdminPass, result); }
                );

                result = ValidateUsbResetFile(userFilePath, onUsbValidates);
                if (result)
                    logLockAction(LOCK_ACTION.RESET_PASS);
            }
            catch (Exception ex)
            {
                result = TaskInfo.Fail(ex.Message);
            }

            return chopString("Reset to " + defaultNewAdminPass + "? " + result.success.ToString() + ", " + result.eventReason);
        }

        private static TaskInfo ValidateUsbResetFile(string userFilePath, Func<TaskInfo> onUsbValidates)
        {
            TaskInfo result;
            FileInfo passFile = new FileInfo(Config.Instance.ADMIN_PASS_RESET_FILE);
            FileInfo userFile = new FileInfo(userFilePath);

            if (!passFile.Exists)
            {
                result = TaskInfo.Fail("Can't load password file from config");
            }
            else if (!userFile.Exists)
            {
                result = TaskInfo.Fail("Can't load file from user path");
            }
            else if (passFile.Length != userFile.Length)
            {
                result = TaskInfo.Fail("File length mismatch. Expecting: " + passFile.Length + "B");
            }
            else if (!File.ReadAllBytes(passFile.FullName).SequenceEqual(File.ReadAllBytes(userFile.FullName)))
            {
                result = TaskInfo.Fail("Files are different!");
            }
            else
            {
                result = onUsbValidates();
            }

            return result;
        }

        // === === === === === Allowed_Command            === === === === === === 

        static Dictionary<int, DateTime> CommandCooldown = new Dictionary<int, DateTime>();
        const int COOLDOWN_SEC = 45;

        public async static Task<string> Allowed_command_client(int index)
        {
           return await runCommand(CommandType.ALLOWED_COMMAND, index.ToString());
        }

        public static string Allowed_command_server(CommandInfo cmdInfo)
        {
            TaskInfo result = TaskInfo.Fail("Index not int: " + cmdInfo.data);

            int commandIndex = -1;
            if (
                int.TryParse(cmdInfo.data,out commandIndex) 
                && commandIndex > -1)
            {
                result = TaskInfo.Fail("Index out of range : " + commandIndex);
                if (commandIndex < Config.Instance.ALLOWED_COMMANDS.Length)
                {
                    TimeSpan cooldownLeft = TimeSpan.FromSeconds(0);
                    if (!CommandCooldown.ContainsKey(commandIndex) 
                        || 
                       Math.Abs((cooldownLeft = CommandCooldown[commandIndex] - DateTime.Now ).TotalSeconds) > COOLDOWN_SEC )
                    {
                        CommandCooldown[commandIndex] = DateTime.Now;

                        ConfigCommand command = Config.Instance.ALLOWED_COMMANDS[commandIndex];
                        result = SystemUtils.RunProcessInfo(command.name, command.path, command.arg);
                    }
                    else
                    {
                        result = TaskInfo.Fail("Cooldown, Wait: " + cooldownLeft.TotalSeconds + "sec");
                    }
                }
            }

            return chopString("Run allowed_cmd? " + result.success.ToString() + ", " + result.eventReason);
        }

        // === === === === === Admin_Command            === === === === === === 

        public async static Task<string> Admin_command_client(int index)
        {
            return await runCommand(CommandType.ADMIN_COMMAND, index.ToString());
        }

        public static string Admin_command_server(CommandInfo cmdInfo)
        {
            TaskInfo result = TaskInfo.Fail("Index not int: " + cmdInfo.data);

            TaskInfo unlockedStatus = isLocked();
            if (unlockedStatus)
            {
                return LockedFormat(unlockedStatus);
            }
            else
            {
                int commandIndex = -1;
                if (
                    int.TryParse(cmdInfo.data, out commandIndex)
                    && commandIndex > -1)
                {
                    result = TaskInfo.Fail("Index out of range : " + commandIndex);
                    if (commandIndex < Config.Instance.ADMIN_COMMANDS.Length)
                    {
                        ConfigCommand command = Config.Instance.ADMIN_COMMANDS[commandIndex];
                        result = SystemUtils.RunProcessInfo(command.name, command.path, command.arg);
                    }
                }
            }

            return chopString("Run admin_cmd? " + result.success.ToString() + ", " + result.eventReason);
        }

        // === === === === === RESET_UNLOCK            === === === === === === 

        public async static Task<string> RESET_UNLOCK_Client(string filename)
        {
            return await runCommand(CommandType.RESET_UNLOCK_USB, filename);
        }

        public static string RESET_UNLOCK_Server(CommandInfo cmdInfo)
        {
            TaskInfo result = TaskInfo.Fail("Init");
            try
            {
                string userFilePath = cmdInfo.data;
                Func<TaskInfo> onUsbValidates = new Func<TaskInfo>(
                    () => {
                        string _date_result = _LockDate(DateTime.Now.Subtract(TimeSpan.FromDays(2)));
                        return new TaskInfo()
                        {
                            success =true, eventReason = _date_result
                        };
                    }
                );

                result = ValidateUsbResetFile(userFilePath, onUsbValidates);

                if (result)
                    logLockAction(LOCK_ACTION.RESET_LOCK);
            }
            catch (Exception ex)
            {
                result = TaskInfo.Fail(ex.Message);
            }

            return chopString("Unlock? " + result.success.ToString() + ", " + result.eventReason);
        }

        // === === === === === Refresh Config === === === === === === 

        public async static Task<string> RefreshConfig_Client()
        {
            return await runCommand(CommandType.REFRESH_CONFIG, "");
        }

        public static string RefreshConfig_Server(CommandInfo cmdInfo)
        {
            return Config.RefreshOrInitPolicy();
        }

    }
}
