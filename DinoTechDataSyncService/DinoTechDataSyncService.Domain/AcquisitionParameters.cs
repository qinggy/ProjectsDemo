using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class AcquisitionParameters
    {

        [DataMember]
        [XmlElement(DataType = "int", ElementName = "Id")]
        public string Id { get; set; }
       
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Name")]
        public string Name { get; set; }
      
        [DataMember]
        [XmlElement(DataType = "int", ElementName = "MeterDataType")]
        public int MeterDataType { get; set; }
      
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "DefaultUnit")]
        public string DefaultUnit { get; set; }
     
        [DataMember]
        [XmlElement(DataType = "int", ElementName = "InventedParameter")]
        public int InventedParameter { get; set; }
        
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Remark")]
        public string Remark { get; set; }

        [DataMember]
        public int ParameterType { get; set; }

        public int UserId { get; set; }

        public string SyncId { get; set; }

    }
}
