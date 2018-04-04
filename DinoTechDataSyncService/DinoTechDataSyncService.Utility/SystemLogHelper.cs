using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinoTechDataSyncService.Utility
{
    public class SystemLogHelper
    {
        public static NLog.Logger Logger { get; set; }

        static SystemLogHelper()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }
    }
}
