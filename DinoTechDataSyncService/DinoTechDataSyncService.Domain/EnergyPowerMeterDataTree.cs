using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class EnergyPowerMeterDataTree
    {
        [DataMember, XmlElement(DataType = "int", ElementName = "Id")]
        public int Id
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "string", ElementName = "Name")]
        public string Name
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "int", ElementName = "ParentId")]
        public int ParentId
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "int", ElementName = "NodeType")]
        public int NodeType
        {
            get;
            set;
        }
    }
}
