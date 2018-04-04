using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class VirtualMeterParameterInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CalculateFormula { get; set; }

        [DataMember]
        public int MeterDataType { get; set; }

        [DataMember]
        public int RecordDeleted { get; set; }

        [DataMember]
        public string ShowCalculateFormula { get; set; }

        [DataMember]
        public string AssociatedParameterIds { get; set; }

        [DataMember]
        public string MeterSyncId { get; set; }

        [DataMember]
        public string FieldTypeSyncId { get; set; }

        [DataMember]
        public string Unit { get; set; }

        [DataMember]
        public string PNumber { get; set; }

        [DataMember]
        public int Factor { get; set; }

        [DataMember]
        public int Interval { get; set; }
    }
}
