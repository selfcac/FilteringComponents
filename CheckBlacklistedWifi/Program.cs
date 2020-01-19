using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckBlacklistedWifiStandard;

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
            Process.Start(path);
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(
                     string.Join("\n", GitInfo.AllGitInfo()
                    )
                );

                log("Git Version: " + CheckBlacklistedWifiStandard.GitInfo.GetInfo());

                string lastArgument = 
                    ((args != null && args.Length > 0) ? args : new[] { "no-service" })
                    .Last();

                bool isExe = lastArgument.Contains(".exe"); // not endsWith because might contain \" after exe

                string nearByWifisCMDResult = getCMDOutput("netsh", "wlan show networks mode=bssid");

                List<string> current_near_wifis = Utils.getWifiWindowsCMDParsed(nearByWifisCMDResult);

                // Logic:
                //      if in block zone (by finding 1 result of blocked bssid) save all new bssid.
                //      if no wifi found, consider in blocked mode (to avoid just disable wifi)

                if (blockFile.Exists)
                {
                    List<string> latest_ruleset = File.ReadAllLines(blockFile.FullName).ToList();
                    WifiZoneFlow(current_near_wifis, latest_ruleset,
                        () => {
                            if (isExe)
                                startProcess(lastArgument);
                            else
                                setService(true, lastArgument);
                        },
                        () => {
                            if (!isExe) setService(false, lastArgument);
                        },
                        (new_rules) =>
                        {
                            File.WriteAllLines(blockFile.FullName, new_rules);
                        }
                        );

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

        private static void WifiZoneFlow(
            List<string> current_near_wifis,
            List<string> latest_ruleset,
            Action insideBlockZone, Action outsideBlockZone, Action<List<string>> updateRules
            )
        {
            if (current_near_wifis.Count > 0)
            {
                List<string> newRuleSet = new List<string>();
                string reason = "init";
                if (WifiHelper.fastBlockZoneCheck(current_near_wifis, latest_ruleset, out newRuleSet, (text) => log(text), out reason ))
                {
                    // Update all rules with bad ones:
                    
                    // TODO: get new rules by call not changing them

                    updateRules?.Invoke(latest_ruleset);
                    log("Reason blocked: " + reason);
                    insideBlockZone?.Invoke();
                }
                else
                {
                    outsideBlockZone?.Invoke();
                }
            }
            else
            {
                log("Found 0 wifis, assume blockzone");
                insideBlockZone?.Invoke();
            }
        }
    }
}
