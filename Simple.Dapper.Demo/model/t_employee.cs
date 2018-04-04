using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Dapper.Demo.model
{
    public class t_employee
    {
        public t_employee()
        {

        }
        [Key]
        public int employeeid { get; set; }
        public string displayname { get; set; }
        public string loginname { get; set; }
        public string password { get; set; }
        /// 1:男,0:女
        public bool sex { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        /// 1:启用,0:禁用
        public bool enable { get; set; }
        [Editable(false)]
        public string address { get; set; }
    }
}
