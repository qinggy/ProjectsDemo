using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DinoTechDataSyncService.Domain
{

    [DataContract]

    public class Energy
    {
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Id")]
        public string Id { get; set; }
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Name")]
        public string Name { get; set; }
        [DataMember]
        [XmlElement(DataType = "bool", ElementName = "IsEnabled")]
        public bool IsEnabled { get; set; }
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Remark")]
        public string Remark { get; set; }
        [DataMember]
        [XmlElement(DataType = "int", ElementName = "EnergyType")]
        public int EnergyType { get; set; }

        public int UserId { get; set; }

        public string SyncId { get; set; }


    }
}