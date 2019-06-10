//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class Config : JSONBaseClass
    {
        static Config _instance = null;
        public string unlockFile = new FileInfo("lock.txt").FullName;
        public string auditFile = new FileInfo("log_audit.txt").FullName;

        public static Config Instance {
            get
            {
                FileInfo configFile = new FileInfo("config.json");
                if (_instance == null)
                {
                    // Create or load config from file.
                    if (configFile.Exists)
                    {
                        ConnectionHelpers.TaskInfo task = FromFile<Config>(configFile.FullName);
                        if (task)
                        {
                            _instance = (task as ConnectionHelpers.TaskInfoResult<Config>).result;
                        }
                        else
                        {
                            _instance = new Config();
                        }
                    }
                    else
                        (_instance = new Config()).ToFile(configFile.FullName);

                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public ConfigCommand[] ALLOWED_COMMANDS = new ConfigCommand[] {
            new ConfigCommand() {name = "Start firewall" , path = "netsh.exe", arg ="advfirewall set allprofiles state on" },
            new ConfigCommand() {name = "Stop firewall" , path = "netsh.exe", arg ="advfirewall set allprofiles state off" },
            new ConfigCommand() {name = "Enable protection" , path = "enable.bat", arg ="" },
            new ConfigCommand() {name = "Restart Divert Services", path = "powershell.exe",arg = "-command \"Restart-Service -Name Yoni_D*\""}
        };
        public ConfigCommand[] ADMIN_COMMANDS = new ConfigCommand[] {
            new ConfigCommand() {name = "Stop protection" , path = "disable.bat", arg ="" },
        };

        public string ECHO_SALT = "Salt"; // To see config is being used
        public string ADMIN_USB_USERNAME = "YoniH";
        public string ADMIN_PASS_RESET_FILE = "userkey.psw";

        public int ControlPanelPort = 9012;
        public bool proxyMappingMode = false;
    }

    public class ConfigCommand
    {
        public string name;
        public string path;
        public string arg;
    }

}
