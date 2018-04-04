using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Esd.EnergyPec.CommonImp
{
    [ServiceContract]
    public interface ICheckUpdateService
    {
        /// <summary>
        /// 判断当前程序在服务器上是否有最新版本存在
        /// </summary>
        /// <param name="ProductCode">产品编号</param>
        /// <returns></returns>
        [OperationContract]
        string IsExistsNewest(string ProductCode, string UpdaterCode);

        /// <summary>
        /// 下载程序更新包
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        [OperationContract]
        Stream DownLoadFile(string FilePath);
    }
}
