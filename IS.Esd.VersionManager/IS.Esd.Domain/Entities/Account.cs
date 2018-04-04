using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Entities
{
    public class Account : Entity
    {
        public string Name { get; set; }

        public string Avatar { get; set; }

        public Gender Gender { get; set; }

        public virtual Role Role { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
