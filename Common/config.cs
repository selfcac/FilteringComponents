using Newtonsoft.Json;
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

        static FileInfo configFile = new FileInfo("config.json");
        static FileInfo unlockFile = new FileInfo("unlock.json");
        static FileInfo auditFile = new FileInfo("log_audit.txt");

        static FileInfo whitelistFile = new FileInfo("whitelist.txt");
        static FileInfo blocklogFile = new FileInfo("log_block.txt");

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

        public string PROXY_SERVICE_NAME = "w3logsvc";
        public string ADMIN_USERNAME = "YoniH";
               
        public int ProxyPort = 9011;
        public int ControlPanelPort = 9012;
       
    }
}
