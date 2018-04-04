using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace DinoTechDataSyncService.WCF.FileUtilities
{
    public static class HelperSystem
    {
        public static readonly string SystemDataFilePath;

        static HelperSystem()
        {
            var fileDataPath = ConfigurationManager.AppSettings["FileDataPath"];
            if (string.IsNullOrEmpty(fileDataPath))
            {
                fileDataPath = @"C:\RealTimeFiles\";
            }

            if (!Directory.Exists(fileDataPath))
            {
                Directory.CreateDirectory(fileDataPath);
            }
            SystemDataFilePath = fileDataPath;
        }
    }
}