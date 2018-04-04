using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    [Serializable]
    public class UserInfo
    {
        public string UName { get; set; }

        [Required]
        [MaxLength(32)]
        public string UPwd { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
    }
}