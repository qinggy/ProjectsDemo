using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Dapper.Demo
{
    /*
     * Dapper.net api使用
     * */
    public class b_base
    {
        public IDbConnection Connection = null;
        public b_base()
        {
            Connection = new SqlConnection(ConfigurationManager.AppSettings["ConnStr"]);
        }
    }
}
