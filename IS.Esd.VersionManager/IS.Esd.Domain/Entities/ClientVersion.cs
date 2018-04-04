using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Entities
{
    public class ClientVersion : Entity
    {
        public Guid ClientId { get; set; }

        public Guid VersionId { get; set; }
    }
}
