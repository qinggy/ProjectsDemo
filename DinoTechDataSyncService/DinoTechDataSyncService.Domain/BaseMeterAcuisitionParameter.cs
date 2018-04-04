using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class BaseMeterAcuisitionParameter
    {
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Id")]
        public string Id { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Unit")]
        public string Unit { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "BaseMeterId")]
        public string BaseMeterId { get; set; }
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "AcuisitionParameterId")]
        public string AcuisitionParameterId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "PNumber")]
        public string PNumber { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "Factor")]
        public double Factor { get; set; }

        [DataMember]
        [XmlElement(DataType = "int", ElementName = "Interval")]
        public int Interval { get; set; }

        public string SyncId { get; set; }
        public string MeterSyncId { get; set; }
        public string FieldTypeSyncId { get; set; }
        public int MeterId { get; set; }
        public int FieldTypeId { get; set; }
      
    }
}
