//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Common
{
    public static class ConfigLoaded
    {
        public static Config _instance = null;
    }

    public class Config : JSONBaseClass
    {

        public string unlockFile = new FileInfo("lock.txt").FullName;
        public string auditFile = new FileInfo("log_audit.txt").FullName;

        public string whitelistFile = new FileInfo("whitelist.txt").FullName;
        public string blocklogFile = new FileInfo("log_block.txt").FullName;


        public static Config Instance()
        {
            FileInfo configFile = new FileInfo("config.json");
            if (ConfigLoaded._instance == null)
            {
                // Create or load config from file.
                if (configFile.Exists)
                {
                    ConnectionHelpers.TaskInfo task = FromFile<Config>(configFile.FullName);
                    if (task)
                    {
                        ConfigLoaded._instance = (task as ConnectionHelpers.TaskInfoResult<Config>).result;
                    }
                    else
                    {
                        ConfigLoaded._instance = new Config();
                    }
                }
                else
                {
                    ConfigLoaded._instance = new Config();
                    var result = ConfigLoaded._instance.ToFile(configFile.FullName);
                    Console.WriteLine("Saved new config to " + configFile.FullName + ", result: " +
                        result.success + ", reason: " + result.eventReason);
                }

            }
            return ConfigLoaded._instance;
        }


        public string PROXY_SERVICE_NAME = "w3logsvc";
        public string ADMIN_USERNAME = "YoniH";


        public int ProxyPort = 9011;
        public int ControlPanelPort = 9012;

        public bool proxyMappingMode = false;

        //public TimeBlock[] blockedTimes = new TimeBlock[] { new TimeBlock(23, 00, 8 * 60) };

    }

    public class TimeBlock
    {
        public int HourStart;
        public int MinuteStart;
        public int LengthMinutes;

        public TimeBlock(int hStart, int mStart, int length)
        {
            HourStart = hStart;
            MinuteStart = mStart;
            LengthMinutes = length;
        }

        public bool ContainTime(int hour, int minute)
        {
            int startTotalMinutes = ((HourStart % 24) * 60 + (MinuteStart % 60));
            int endTotalMinutes = (startTotalMinutes + LengthMinutes) % (24 * 60);

            int time = ((hour % 24) * 60 + minute);

            if (endTotalMinutes < startTotalMinutes) // from one day to other
            {
                if (time <= endTotalMinutes ^ time >= startTotalMinutes)
                {
                    return true;
                }
            }
            else
            {
                if (time >= startTotalMinutes && time <= endTotalMinutes)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
