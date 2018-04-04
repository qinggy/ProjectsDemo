using SianalR.MSSQL.Monitor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SianalR.MSSQL.Monitor
{
    public class DataRepository
    {
        public IEnumerable<Test> GetData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id],[UserId],[UserName] FROM [dbo].[SignalR]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new Test()
                            {
                                Id = x.GetInt32(0),
                                UserId = x.GetInt32(1),
                                UserName = x.GetString(2)
                            }).ToList();
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            DataHub.ShowChangedData();
        }
    }
}