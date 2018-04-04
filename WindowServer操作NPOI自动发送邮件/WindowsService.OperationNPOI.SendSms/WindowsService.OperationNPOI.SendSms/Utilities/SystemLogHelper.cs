using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsService.OperationNPOI.SendSms.Utilities
{
    public class SystemLogHelper
    {
        public static Logger Logger;

        static SystemLogHelper()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

    }
}
