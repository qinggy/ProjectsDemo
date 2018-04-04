using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DinoTechDataSyncService.Domain
{

    [DataContract]
    public class ResponseUserTokenInfo
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string LogId { get; set; }
    }
}