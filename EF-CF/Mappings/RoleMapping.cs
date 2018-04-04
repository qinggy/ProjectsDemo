using EF_CF_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF_CF_Data.Mappings
{
    public class RoleMapping : EntityMapping<Role>
    {
        public RoleMapping()
        {
            ToTable("Role");

            this.Property(m => m.Name).IsRequired().HasMaxLength(50);
        }
    }
}
