using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Dapper.Demo.model
{
    public class t_derelation
    {
        [Key]
        public int relationid { get; set; }
        public int employeeid { get; set; }
        public int departmentid { get; set; }
    }
}
