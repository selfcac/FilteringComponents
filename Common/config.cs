using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class Config
    {
        static Config _instance = null;

        static FileInfo configFile = new FileInfo("config.json");
        static FileInfo logFile = new FileInfo("log.json");
        static FileInfo unlockFile = new FileInfo("unlock.json");



        public static Config Instance {
            get
            {
                if (_instance == null)
                {
                    // Create or load config from file.
                    if (configFile.Exists)
                    {
                        ConnectionHelpers.TaskInfo task = FromFile(configFile.FullName);
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

        public ConnectionHelpers.TaskInfo ToFile(string filename)
        {
            ConnectionHelpers.TaskInfo result = ConnectionHelpers.TaskInfo.Fail("init");
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                if (File.Exists(filename))
                    File.Delete(filename); // O.W might write only in the beginning of the file.
                File.WriteAllText(filename, json);
                result = ConnectionHelpers.TaskInfo.Success("Json was saved");
            }
            catch (Exception ex)
            {
                result = ConnectionHelpers.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        public static ConnectionHelpers.TaskInfo FromFile(string filename)
        {
            ConnectionHelpers.TaskInfo result = ConnectionHelpers.TaskInfo.Fail("init");
            try
            {
                if (File.Exists(filename))
                {
                    string json = File.ReadAllText(filename);
                    Config obj = JsonConvert.DeserializeObject<Config>(json);
                    result = ConnectionHelpers.TaskInfoResult<Config>.Result(obj);
                }
                else
                {
                    result = ConnectionHelpers.TaskInfo.Fail("Json file not found");
                }
            }
            catch (Exception ex)
            {
                result = ConnectionHelpers.TaskInfo.Fail(ex.Message);
            }

            return result;
        }
    }
}
