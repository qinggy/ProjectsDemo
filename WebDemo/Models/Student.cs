using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    [Serializable]
    public class Student
    {
        [StringLength(32)]
        public virtual string SName { get; set; }

        [StringLength(32)]
        public virtual string Address { get; set; }

        [Key]
        public virtual int Id { get; set; }
    }
}