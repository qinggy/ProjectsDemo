using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace DinoTechDataSyncService.Domain
{
    [DataContract]
    public class EnergyMeterHistoryDataRecord
    {
        [DataMember, XmlElement(DataType = "int", ElementName = "CloudBaseMeterFieldTypeId")]
        public int CloudBaseMeterFieldTypeId
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "double", ElementName = "TotalData")]
        public double TotalData
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "string", ElementName = "DurationType")]
        public string DurationType
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "double", ElementName = "SameCompareTotalData")]
        public double SameCompareTotalData
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "double", ElementName = "LinkCompareTotalData")]
        public double LinkCompareTotalData
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "double", ElementName = "TotalMoney")]
        public double TotalMoney
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "double", ElementName = "SameCompareTotalMoney")]
        public double SameCompareTotalMoney
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "double", ElementName = "LinkCompareTotalMoney")]
        public double LinkCompareTotalMoney
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "DateTime", ElementName = "HTime")]
        public DateTime HTime
        {
            get;
            set;
        }
        [DataMember, XmlElement(DataType = "int", ElementName = "RecordTypeId")]
        public int RecordTypeId
        {
            get;
            set;
        }
    }
}
