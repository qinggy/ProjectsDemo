using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace SimpleWCF.Contract
{
    [ServiceContract]
    public interface ISquareService
    {
        #region 获取某数的N次方值
        /// <summary>
        /// 说明：获取某数的N次方值
        /// </summary>
        /// <param name="dbValue">要进行求方计算的数</param>
        /// <param name="intNum">几次方</param>
        /// <returns>String类型的计算结果</returns>
        [OperationContract]
        String GetSquareValue(double dbValue, int intNum);
        #endregion
    }
}
