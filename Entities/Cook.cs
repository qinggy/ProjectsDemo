using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Entities
{
    [DataContract]
    public class Cook
    {
        [DataMember]
        public string CookName { get; set; }
        [DataMember]
        public string Hotel { get; set; }
    }
}
