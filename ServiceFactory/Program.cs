using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ServiceFactory
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            CookServer server = new CookServer();
#if DEBUG
            server.DebugRun();
#else
            ServiceBase.Run(server);
#endif
        }
    }
}
