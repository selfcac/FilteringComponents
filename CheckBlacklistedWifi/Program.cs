using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBlacklistedWifi
{
    class Program
    {
        static string getCMDOutput(string exe, string args)
        {
            // Start the child process.
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = args;
            p.Start();
            string output = p.StandardOutput.ReadToEnd(); // before exit
            p.WaitForExit();

            return output;
        }

        static FileInfo blockFile = new FileInfo("bssid_blacklist.txt");
        static FileInfo logFile = new FileInfo("wifiblock_log.txt");

        static void log(string msg)
        {
            File.AppendAllText(logFile.FullName, string.Format("[{0}] {1}", DateTime.Now, msg) + Environment.NewLine);
        }

        static void setService(bool start, string servicename) // start = true if blocked zone => start filter service
        {
            try
            {
                log("Start service ? " + start + " ,Name: '" + servicename + "'...");
                bool isServiceRunning = false;
                var runnigResult = Common.SystemUtils.isServiceRunning(servicename, out isServiceRunning);

                if (runnigResult)
                {
                    if (start)
                    {
                        if (!isServiceRunning)
                            Common.SystemUtils.StartService(servicename);
                    }
                    else
                    {
                        if (isServiceRunning)
                            Common.SystemUtils.StopService(servicename);
                    }
                }
                else
                {
                    log("Can't get state of service '" + servicename + "' because: " + runnigResult.eventReason);
                }
            }
            catch (Exception ex)
            {
                log(ex.ToString());
            }
        }

        static void startProcess(string path)
        {

        }

        static void Main(string[] args)
        {
            try
            {
                string lastArgument = 
                    ((args != null && args.Length > 0) ? args : new[] { "no-service" })
                    .Last();

                bool isExe = lastArgument.Contains(".exe"); // not endsWith because might contain \" after exe

                string nearByWifisCMDResult = getCMDOutput("netsh", "wlan show networks mode=bssid");

                List<string> wifi_data = Utils.getWifiParsed(nearByWifisCMDResult);

                // Logic:
                //      if in block zone (by finding 1 result of blocked bssid) save all new bssid.
                //      if no wifi found, consider in blocked mode (to avoid just disable wifi)

                if (blockFile.Exists)
                {

                    if (wifi_data.Count > 0)
                    {
                        List<string> ruleset = File.ReadAllLines(blockFile.FullName).ToList();

                        if (WifiHelper.fastBlockZoneCheck(wifi_data, ruleset, (text) => log(text))) 
                        {
                            // Update all rules with bad ones:
                            File.WriteAllLines(blockFile.FullName, ruleset);

                            if (isExe)
                                startProcess(lastArgument);
                            else
                                setService(true, lastArgument);
                        }
                        else
                        {
                            if (!isExe) setService(false, lastArgument);
                        }
                    }
                    else
                    {
                        log("Found 0 wifis, assume blockzone");

                        if (isExe)
                            startProcess(lastArgument);
                        else
                            setService(true, lastArgument);
                    }

                }
                else
                {
                    log("Can't find blacklist in '" + blockFile.FullName + "'");
                }
            }
            catch (Exception ex)
            {
                log(ex.ToString());
            }
        }
    }
}
