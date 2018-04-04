using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoTechDataSyncService.WCF.FileUtilities
{
    public class AutoData
    {
        /// <summary>
        /// 仪表值
        /// </summary>
        public double MeterValue { get; set; }

        /// <summary>
        /// 采集参数
        /// </summary>
        public int MeterFieldTypeId { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime CollectionTime { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public int UserId { get; set; }
    }
}
