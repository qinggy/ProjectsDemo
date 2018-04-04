using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;

namespace SimpleWCF.Implement
{
    [ServiceBehavior]
    class SquareService:SimpleWCF.Contract.ISquareService
    {
        #region 获取某数的N次方值
        /// <summary>
        /// 说明：获取某数的N次方值
        /// </summary>
        /// <param name="dbValue">要进行求方计算的数</param>
        /// <param name="intNum">几次方</param>
        /// <returns>String类型的计算结果</returns>
        public String GetSquareValue(double dbValue, int intNum)
        {
            if (intNum < 0)
            {
                return "求方计算的次方不能小于0！";
            }
            else if (intNum == 0)
            {
                return "1";
            }
            else if (intNum == 1)
            {
                return dbValue.ToString();
            }
            else
            {
                double dbResultValue = 1;

                for (int i = 0; i < intNum; i++)
                {
                    dbResultValue *= dbValue;
                }

                return dbResultValue.ToString();
            }
        }
        #endregion
    }
}
