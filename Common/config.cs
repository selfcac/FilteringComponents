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
    public class Config : JSONBaseClass
    {
        [ScriptIgnore(), XmlIgnore()]
        private static Config _instance = null;

        public static FileInfo configFile = new FileInfo("config.json");
        public FileInfo unlockFile = new FileInfo("lock.txt");
        public FileInfo auditFile = new FileInfo("log_audit.txt");

        public FileInfo whitelistFile = new FileInfo("whitelist.txt");
        public FileInfo blocklogFile = new FileInfo("log_block.txt");

        
        public static Config Instance () { 
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
                    {
                        _instance = new Config();
                        var result = _instance.ToFile(configFile.FullName);
                        Console.WriteLine("Saved new config to " + configFile.FullName + ", result: " +
                            result.success + ", reason: " + result.eventReason);
                    }

                }
                return _instance;
        }
        

        public string PROXY_SERVICE_NAME = "w3logsvc";
        public string ADMIN_USERNAME = "YoniH";

               
        public int ProxyPort = 9011;
        public int ControlPanelPort = 9012;

        public bool proxyMappingMode = false;

        public TimeBlock[] blockedTimes = new TimeBlock[] { new TimeBlock( 23, 00,8 * 60) };
       
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
            
            if ( endTotalMinutes < startTotalMinutes ) // from one day to other
            {
                if (time  <= endTotalMinutes  ^ time >= startTotalMinutes)
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
