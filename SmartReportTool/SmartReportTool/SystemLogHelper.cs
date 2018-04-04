using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartReportTool
{
    public class SystemLogHelper
    {
        static SystemLogHelper()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        public static Logger Logger { get; set; }
    }
}
