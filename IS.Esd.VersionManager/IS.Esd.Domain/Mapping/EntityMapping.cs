using IS.Esd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Mapping
{
    public class EntityMapping<T> : EntityTypeConfiguration<T> where T : Entity
    {
        protected EntityMapping()
        {
            HasKey(a => a.Id);
        }
    }
}
