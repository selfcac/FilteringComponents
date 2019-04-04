using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using C=Common.ConnectionHelpers;

namespace Common
{
    public class SystemUtils
    {
        public static C.TaskInfo isServiceRunning(string name, out bool isRunning)
        {
            C.TaskInfo result = C.TaskInfo.Fail("Init");
            isRunning = false;

            try
            {
                ServiceController service = new ServiceController(name);
                isRunning = 
                    (service.Status == ServiceControllerStatus.Running) ||
                    (service.Status == ServiceControllerStatus.StartPending) ||
                    (service.Status == ServiceControllerStatus.ContinuePending) 
                    ;
                result = C.TaskInfo.Success("got status");
            }
            catch (Exception ex)
            {
                result = C.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        public static C.TaskInfo StartService(string name)
        {
            C.TaskInfo result = C.TaskInfo.Fail("Init");
            try
            {
                ServiceController service = new ServiceController(name);

                //Start the service
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    try
                    {
                        service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10.0));
                        result = C.TaskInfo.Success("Service started!");
                    }
                    catch (System.ServiceProcess.TimeoutException ex)
                    {
                        result = C.TaskInfo.Fail("Timout while waiting for service.");
                    }
                }
                else
                {
                    result = C.TaskInfo.Fail("Service not in stopped mode! Mode: " + service.Status.ToString());
                }
            }
            catch (Exception ex)
            {
                result = C.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        public static C.TaskInfo StopService(string name)
        {
            C.TaskInfo result = C.TaskInfo.Fail("Init");
            try
            {
                ServiceController service = new ServiceController(name);

                //Start the service
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    try
                    {
                        service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10.0));
                        result = C.TaskInfo.Success("Service stopped!");
                    }
                    catch (System.ServiceProcess.TimeoutException ex)
                    {
                        result = C.TaskInfo.Fail("Timout while waiting for service.");
                    }
                }
                else
                {
                    result = C.TaskInfo.Fail("Service not in running mode! Mode: " + service.Status.ToString());
                }
            }
            catch (Exception ex)
            {
                result = C.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        public static C.TaskInfo ChangeUserPassword(string username, string newPassword)
        {
            C.TaskInfo result = C.TaskInfo.Fail("Init");

            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntry grp;
                grp = AD.Children.Find(username, schemaClassName: "user");
                if (grp != null)
                {
                    grp.Invoke("SetPassword", new object[] { newPassword });
                    grp.CommitChanges();
                    result = C.TaskInfo.Success("User password changed!");
                }
                else
                {
                    result = C.TaskInfo.Fail("Can't find username.");
                }
            }
            catch (Exception ex)
            {
                result = C.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        static void runCmdProcess(string process, string arguments) 
        {
            Process proc = new Process();
            proc.StartInfo.FileName = process;
            proc.StartInfo.Arguments = arguments;
            //proc.StartInfo.UseShellExecute = false;
            //proc.StartInfo.RedirectStandardOutput = true;
            //proc.StartInfo.CreateNoWindow = true;
            proc.Start();
        }

        public static C.TaskInfo StartFirewall()
        {
            C.TaskInfo result = C.TaskInfo.Fail("Init");
            try
            {
                runCmdProcess("netsh.exe", "advfirewall set allprofiles state on");
                result = C.TaskInfo.Success("Firewall started...");
            }
            catch (Exception ex)
            {
                result = C.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

        public static C.TaskInfo StopFirewall()
        {
            C.TaskInfo result = C.TaskInfo.Fail("Init");
            try
            {
                runCmdProcess("netsh.exe", "advfirewall set allprofiles state off");
                result = C.TaskInfo.Success("Firewall started...");
            }
            catch (Exception ex)
            {
                result = C.TaskInfo.Fail(ex.Message);
            }

            return result;
        }

    }
}
