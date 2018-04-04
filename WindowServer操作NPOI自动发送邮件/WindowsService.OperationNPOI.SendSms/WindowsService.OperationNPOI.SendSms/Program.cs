using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsService.OperationNPOI.SendSms
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[] 
            { 
                new NPOISmsService() 
            };
            ServiceBase.Run(ServicesToRun);

            //            NPOISmsService service = new NPOISmsService();
            //#if DEBUG
            //            service.DebugRun();
            //#else
            //                        ServiceBase.Run(service);
            //#endif
        }
    }
}
