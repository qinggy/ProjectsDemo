using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class MeterModelFieldTypes
    {
        [DataMember, XmlElement(DataType = "string", ElementName = "MeterModelId")]
        public string MeterModelId
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "string", ElementName = "FieldTypeId")]
        public string FieldTypeId
        {
            get;
            set;
        }
    }
}
