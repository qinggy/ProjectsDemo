using IS.Esd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Mapping
{
    public class ClientMapping : EntityMapping<Client>
    {
        public ClientMapping()
        {
            ToTable("Client");
        }
    }
}
