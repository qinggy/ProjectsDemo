using Esd.EnergyPec.CommonImp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Esd.EnergyPec.Services
{
    public class CheckUpdateService : ICheckUpdateService
    {

        public string IsExistsNewest(string ProductCode, string UpdaterCode)
        {
            string result = DBHelper.ExcuteProcdure(ProductCode, UpdaterCode);

            return result;
        }


        public Stream DownLoadFile(string FilePath)
        {
            if (!File.Exists(FilePath))//判断文件是否存在
            {
                return null;
            }

            try
            {
                Stream myStream = File.OpenRead(FilePath);
                return myStream;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
