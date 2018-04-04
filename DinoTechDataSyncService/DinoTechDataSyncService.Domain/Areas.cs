using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class Areas
    {

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Id")]
        public string Id { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "ParentAreaId")]
        public string ParentAreaId { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Name")]
        public string Name { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Address")]
        public string Address { get; set; }

        [DataMember]
        [XmlElement(DataType = "int", ElementName = "AreaType")]
        public int AreaType { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Acreage")]
        public string Acreage { get; set; }
        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Contact")]
        public string Contact { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Phone")]
        public string Phone { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Fax")]
        public string Fax { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "ZipCode")]
        public string ZipCode { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Email")]
        public string Email { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "Remark")]
        public string Remark { get; set; }
        [DataMember]
        [XmlElement(DataType = "int", ElementName = "DisplayedOrder")]
        public int DisplayedOrder { get; set; }

        [DataMember]
        [XmlElement(DataType = "string", ElementName = "MapCoordinate")]
        public string Coordinate { get; set; }

        public bool RecordDeleted { get { return false; } }
        public string SyncId { get; set; }
        public int ParentId { get; set; }
        public int UserId { get; set; }
    }
}
