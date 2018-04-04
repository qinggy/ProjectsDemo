using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class BaseMeter
    {

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Id")]
        public string Id { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "BaseMeterParentAreaId")]
        public string BaseMeterParentAreaId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Name")]
        public string Name { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "BaseMeterEnergyId")]
        public string BaseMeterEnergyId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "BaseMeterModelId")]
        public string BaseMeterModelId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Manufacturer")]
        public string Manufacturer { get; set; }

        [DataMember]
        [XmlElement(DataType = "double", ElementName = "InitialValue")]
        public double InitialValue { get; set; }


        [DataMember]
        [XmlElement(DataType = "string", ElementName = "MeterNumber")]
        public string MeterNumber { get; set; }


        [DataMember]
        [XmlElement(DataType = "int", ElementName = "DisplayedOrder")]
        public int DisplayedOrder { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Remark")]
        public string Remark { get; set; }


        [DataMember]
        [XmlElement(DataType = "bool", ElementName = "IsEnabled")]
        public bool IsEnabled { get; set; }

        [DataMember]
        [XmlElement(DataType = "int", ElementName = "BaseMeterType")]
        public int BaseMeterType { get; set; }

        [DataMember]
        [XmlElement(DataType = "bool", ElementName = "IsVirtualMeter")]
        public bool IsVirtualMeter { get; set; }

        public bool IsRobotizationCollect { get { return false; } }

        public string SyncId { get; set; }
        public string ParentSyncId { get; set; }
        public string EnergySyncId { get; set; }
        public string MeterModelSyncId { get; set; }
        public int ParentAreaId { get; set; }
        public int EnergyId { get; set; }
        public int MeterModelId { get; set; }
        public bool RecordDeleted { get { return false; } }
        public int UserId { get; set; }
    }
}
