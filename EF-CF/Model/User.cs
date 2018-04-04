using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF_CF_Data.Model
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string CardId { get; set; }

        public virtual Role Role { get; set; }
    }
}
