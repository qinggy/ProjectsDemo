using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class BaseRecord
    {
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Id")]
        public string Id { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "TotalData")]
        public double TotalData { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "SameCompareTotalData")]
        public double SameCompareTotalData { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "LinkCompareTotalData")]
        public double LinkCompareTotalData { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "TotalMoney")]
        public double TotalMoney { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "SameCompareTotalMoney")]
        public double SameCompareTotalMoney { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "LinkCompareTotalMoney")]
        public double LinkCompareTotalMoney { get; set; }

        [DataMember]
        [XmlElement(DataType = "DateTime", ElementName = "HTime")]
        public DateTime HTime { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "DateTime")]
        public string DateTime { get; set; }

        [DataMember]
        [XmlElement(DataType = "int", ElementName = "RecordTypeId")]
        public int RecordTypeId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "BaseMeterAcquisitionParameterId")]
        public string BaseMeterAcquisitionParameterId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "BaseMeterId")]
        public string BaseMeterId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "AcuisitionParameterId")]
        public string AcuisitionParameterId { get; set; }

        public int BaseMeterFieldTypeId { get; set; }

        public int UserId { get; set; }
    }
}
