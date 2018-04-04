using EF_CF_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF_CF_Data.Mappings
{
    public class UserMapping : EntityMapping<User>
    {
        public UserMapping()
        {
            ToTable("User");

            this.Property(m => m.Name).IsRequired().HasMaxLength(50);
            this.HasRequired(m => m.Role).WithMany().Map(m => m.MapKey("RoleId"));
        }
    }
}
