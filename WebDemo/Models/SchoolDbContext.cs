using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class SchoolDbContext :DbContext
    {
        public SchoolDbContext()
            : base("name=MySqlDemo")
        {
            this.Database.CreateIfNotExists();
        }

        public virtual DbSet<Student> Student { get; set; }

        public virtual DbSet<UserInfo> UserInfo { get; set; }
    }
}