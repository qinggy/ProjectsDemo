using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DinoTechDataSyncService.Domain
{

    [DataContract]
    public class MeterModel
    {
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Id")]
        public string Id { get; set; }
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Name")]
        public string Name { get; set; }
        [DataMember]
        [XmlElement(DataType = "double", ElementName = "MinValue")]
        public double MinValue { get; set; }
        [DataMember]
        [XmlElement(DataType = "double", ElementName = "MaxValue")]
        public double MaxValue { get; set; }
        [DataMember]
        [XmlElement(DataType = "double", ElementName = "Accuracy")]
        public double Accuracy { get; set; }
        [DataMember]
        [XmlElement(DataType = "bool", ElementName = "CanReadBack")]
        public bool CanReadBack { get; set; }
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Remark")]
        public string Remark { get; set; }
        [DataMember]
        [XmlElement(DataType = "int", ElementName = "EnergyId")]
        public string EnergyId { get; set; }


        public int UserId { get; set; }
        public string SyncId { get; set; }


    }
}