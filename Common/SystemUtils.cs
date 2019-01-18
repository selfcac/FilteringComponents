using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using C=Common.ConnectionHelpers;

namespace Common
{
    public class SystemUtils
    {
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
    }
}
