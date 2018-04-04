using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DinoTechDataSyncService.WCF.FileUtilities
{
    [DataContract]
    public class RequestBTMData
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public List<DataBase> Data { get; set; }
    }

    [DataContract]
    public class DataBase
    {
        [DataMember]
        public int BF { get; set; }
        [DataMember]
        public double D { get; set; }
        [DataMember]
        public string T { get; set; }
        [DataMember]
        public int DT { get; set; }
    }
}