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
                        result = C.TaskInfo.Success("Service is running!");
                    }
                    catch (System.ServiceProcess.TimeoutException ex)
                    {
                        result = C.TaskInfo.Fail("Timout while waiting for service.");
                    }
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
