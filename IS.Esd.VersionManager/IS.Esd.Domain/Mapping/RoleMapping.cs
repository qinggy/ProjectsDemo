using IS.Esd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Mapping
{
    public class RoleMapping : EntityMapping<Role>
    {
        public RoleMapping()
        {
            ToTable("Role");
        }
    }
}
