using System;

namespace SmartReportTool
{
    public class Mapping
    {
        /// <summary>
        /// Cell Address to be mapped
        /// </summary>
        public string CellAddress { get; set; }
        /// <summary>
        /// Period
        /// </summary>
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Field Type to be mapped from database
        /// </summary>
        public string FieldTypeId { get; set; }
        /// <summary>
        /// Price Type(峰 谷 平)
        /// </summary>
        public string PriceType { get; set; }
        /// <summary>
        /// Each Cell DataSource
        /// </summary>
        public string SourceType { get; set; }
        /// <summary>
        /// Each Cell ServiceURL
        /// </summary>
        public string ServiceURL { get; set; }
    }
}
