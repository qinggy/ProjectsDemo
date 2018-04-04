using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Utility
{
    public class SystemLogHelper
    {
        public static NLog.Logger _Logger { get; set; }

        static SystemLogHelper()
        {
            _Logger = LogManager.GetCurrentClassLogger();
        }
    }
}
