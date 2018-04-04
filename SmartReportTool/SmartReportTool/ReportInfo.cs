using System.Collections.Generic;

namespace SmartReportTool
{
    public class SmsContent
    {
        public string StartTimePosition { get; set; }
        public string EndTimePosition { get; set; }
        public string WriteTime { get; set; }
        public string GetDataTime { get; set; }
        public string ServerPort { get; set; }
        public string ServerHost { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public List<Mapping> CellMappings { get; set; }
    }
}
