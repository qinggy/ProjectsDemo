using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DinoTechDataSyncService.Business
{
    [DataContract]
    public class ResponseStatus
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}