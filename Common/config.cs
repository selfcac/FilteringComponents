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

        public static FileInfo configFile = new FileInfo("config.json");
        public FileInfo unlockFile = new FileInfo("lock.txt");
        public FileInfo auditFile = new FileInfo("log_audit.txt");

        public static Config Instance {
            get
            {
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

        public ConfigCOmmand[] ALLOWED_COMMANDS = new ConfigCOmmand[] {
            new ConfigCOmmand() {name = "Start firewall" , path = "netsh.exe", arg ="advfirewall set allprofiles state on" },
            new ConfigCOmmand() {name = "Stop firewall" , path = "netsh.exe", arg ="advfirewall set allprofiles state off" },
            new ConfigCOmmand() {name = "Enable protection" , path = "enable.bat", arg ="" },
        };
        public ConfigCOmmand[] ADMIN_COMMANDS = new ConfigCOmmand[] {
            new ConfigCOmmand() {name = "Stop protection" , path = "enable.bat", arg ="" },
        };

        public string ADMIN_USB_USERNAME = "YoniH";
        public string ADMIN_PASS_RESET_FILE = "userkey.psw";

        public int ControlPanelPort = 9012;
        public bool proxyMappingMode = false;
    }

    public class ConfigCOmmand
    {
        public string name;
        public string path;
        public string arg;
    }

}
