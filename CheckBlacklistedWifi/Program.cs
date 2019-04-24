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

        static void Main(string[] args)
        {
            try
            {
                string servicename = 
                    ((args != null && args.Length > 0) ? args : new[] { "no-service" })
                    .Last();

                string nearByWifis = getCMDOutput("netsh", "wlan show networks mode=bssid");



                /* TODO: Add names (In some comment mode so renaming wont affect... only bssid)

                */
                string[] currentBSSID =
                    nearByWifis
                    .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Where((line) => line.StartsWith("    BSSID"))
                    .ToArray();

                for (int i = 0; i < currentBSSID.Length; i++)
                {
                    currentBSSID[i] = currentBSSID[i].Substring(currentBSSID[i].IndexOf(": ") + 2);
                }

                // Logic:
                //      if in block zone (by finding 1 result of blocked bssid) save all new bssid.
                //      if no wifi found, consider in blocked mode (to avoid just disable wifi)

                if (blockFile.Exists)
                {
                    bool foundBlockedBSSID = false;

                    if (currentBSSID.Length > 0)
                    {
                        HashSet<string> blockHashes = new HashSet<string>(File.ReadAllLines(blockFile.FullName));
                        List<string> newBSSID = new List<string>();

                        for (int i = 0; i < currentBSSID.Length; i++)
                        {
                            if (blockHashes.Contains(currentBSSID[i]))
                            {
                                foundBlockedBSSID = true;
                            }
                            else
                            {
                                newBSSID.Add(currentBSSID[i]);
                            }
                        }

                        if (foundBlockedBSSID)
                        {
                            File.AppendAllLines(blockFile.FullName, newBSSID);
                            log("In block zone, found " + newBSSID.Count + " new bssid");
                            setService(true, servicename);
                        }
                        else
                        {
                            // found wifis + didnt find block, so we not in blocked zone!
                            log("Found " + currentBSSID.Length + " wifis but none blocked, ok zone!");
                            setService(false, servicename);
                        }
                    }
                    else
                    {
                        log("Found 0 wifis, assume blockzone");
                        setService(true, servicename);
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
