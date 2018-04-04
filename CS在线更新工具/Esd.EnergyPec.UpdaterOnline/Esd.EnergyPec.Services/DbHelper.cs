using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Esd.EnergyPec.Services
{

    public static class DBHelper
    {
        static SqlConnection connection;
        static SqlConnection Connection
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString.ToString();
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }

        public static string ExcuteProcdure(string ProductCode, string UpdaterCode)
        {
            string result = "";
            try
            {
                using (SqlCommand cmd = new SqlCommand(string.Empty, Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "IsExistsNewest";
                    cmd.Parameters.Add("@ProductCode", SqlDbType.UniqueIdentifier).Value = new Guid(ProductCode);
                    cmd.Parameters.Add("@UpdaterCode", SqlDbType.VarChar).Value = UpdaterCode;

                    SqlParameter outputparam = cmd.Parameters.Add("@strRtn", SqlDbType.NVarChar, 120);
                    outputparam.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    result = outputparam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }

}