using IS.Esd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Mapping
{
    public class AccountMapping : EntityMapping<Account>
    {
        public AccountMapping()
        {
            ToTable("Account");
            HasRequired(a => a.Role).WithMany().Map(m => m.MapKey("RoleId"));
        }
    }
}
