using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Mapping
{
    public class VersionMapping : EntityMapping<IS.Esd.Domain.Entities.Version>
    {
        public VersionMapping()
        {
            ToTable("Version");
        }
    }
}
