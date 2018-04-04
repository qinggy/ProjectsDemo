using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoTechDataSyncService.Repository
{
    public sealed class DBManagerFactory
    {
        #region Constructor
        private DBManagerFactory() { }
        #endregion

        #region Public Method
        /// <summary>
        /// GetConnection Fns will initialize the DBConnection based on the Database we are using.
        /// Currently this will supports for following DB
        /// 1. Microsoft SQL Server
        /// 2. OLEDB Connections
        /// 3. ODBC Connections
        /// 4. Oracle Connections
        /// 5. MySql 
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        /// 


        public static IDbConnection GetConnection(DataProvider providerType)
        {
            IDbConnection iDbConnection = null;
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    iDbConnection = new SqlConnection();
                    break;
                case DataProvider.OleDb:
                    iDbConnection = new OleDbConnection();
                    break;
                case DataProvider.Odbc:
                    iDbConnection = new OdbcConnection();
                    break;
                //case DataProvider.Oracle:
                //    iDbConnection = new OracleConnection();
                //    break;
                //case DataProvider.MySql:
                //    iDbConnection = new MySqlConnection();
                //    break;
                default:
                    return null;
            }
            return iDbConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDbCommand GetCommand(DataProvider providerType)
        {
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    return new SqlCommand();
                case DataProvider.OleDb:
                    return new OleDbCommand();
                case DataProvider.Odbc:
                    return new OdbcCommand();
                //case DataProvider.Oracle:
                //    return new OracleCommand();
                //case DataProvider.MySql:
                //    return new MySqlCommand();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDbDataAdapter GetDataAdapter(DataProvider providerType)
        {
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    return new SqlDataAdapter();
                case DataProvider.OleDb:
                    return new OleDbDataAdapter();
                case DataProvider.Odbc:
                    return new OdbcDataAdapter();
                //case DataProvider.Oracle:
                //    return new OracleDataAdapter();
                //case DataProvider.MySql:
                //    return new MySqlDataAdapter();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDbTransaction GetTransaction(DataProvider providerType)
        {
            IDbConnection iDbConnection = GetConnection(providerType);
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
            return iDbTransaction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDataParameter GetParameter(DataProvider providerType)
        {
            IDataParameter iDataParameter = null;
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    iDataParameter = new SqlParameter();
                    break;
                case DataProvider.OleDb:
                    iDataParameter = new OleDbParameter();
                    break;
                case DataProvider.Odbc:
                    iDataParameter = new OdbcParameter();
                    break;
                    //case DataProvider.Oracle:
                    //    iDataParameter = new OracleParameter();
                    //    break;
                    //case DataProvider.MySql:
                    //    iDataParameter = new MySqlParameter();
                    break;

            }
            return iDataParameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerType"></param>
        /// <param name="paramsCount"></param>
        /// <returns></returns>
        public static IDbDataParameter[] GetParameters(DataProvider providerType, int paramsCount)
        {
            IDbDataParameter[] idbParams = new IDbDataParameter[paramsCount];

            switch (providerType)
            {
                case DataProvider.SqlServer:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new SqlParameter();
                    }
                    break;
                case DataProvider.OleDb:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OleDbParameter();
                    }
                    break;
                case DataProvider.Odbc:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OdbcParameter();
                    }
                    break;
                //case DataProvider.Oracle:
                //    for (int i = 0; i < paramsCount; ++i)
                //    {
                //        idbParams[i] = new OracleParameter();
                //    }
                //    break;
                //case DataProvider.MySql:
                //    for (int i = 0; i < paramsCount; ++i)
                //    {
                //        idbParams[i] = new MySqlParameter();
                //    }
                //    break;
                default:
                    idbParams = null;
                    break;
            }
            return idbParams;
        }
        #endregion

    }
}
