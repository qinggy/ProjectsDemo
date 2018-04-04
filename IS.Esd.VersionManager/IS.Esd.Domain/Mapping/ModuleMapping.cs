using IS.Esd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Mapping
{
    public class ModuleMapping : EntityMapping<Module>
    {
        public ModuleMapping()
        {
            ToTable("Module");
        }
    }
}
