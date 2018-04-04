using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Entities
{
    public class Module : Entity
    {
        public string Name { get; set; }

        public string DllName { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Remark { get; set; }

        public Guid BelongVersion { get; set; }

        public string VersionCode { get; set; }

    }
}
