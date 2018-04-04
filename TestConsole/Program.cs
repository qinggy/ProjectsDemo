using EF_CF_Data;
using EF_CF_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                EntityDbContext DbContext = new EntityDbContext();
                DbContext.Set<User>().Add(new User() { Id = Guid.NewGuid(), Name = "卿光扬", CardId = "458768598725014562", Role = new Role() { Id = Guid.NewGuid(), Name = "管理员" } });
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                //LogHelper.ErrorLog(e);
            }
        }
    }
}
