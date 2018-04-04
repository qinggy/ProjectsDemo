using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Angus.qing.MultiDatabase
{
    #region Enum Declaration

    public enum DataProvider
    {
        Oracle, SqlServer, OleDb, Odbc, MySql
    }

    #endregion

    public interface IDBInterface
    {

        #region Public Properties Declaration
        DataProvider ProviderType
        {
            get;
            set;
        }

        string ConnectionString
        {
            get;
            set;
        }

        IDbConnection Connection
        {
            get;
        }

        IDbTransaction Transaction
        {
            get;
        }

        IDataReader DataReader
        {
            get;
        }

        IDbCommand Command
        {
            get;
        }

        IDbDataParameter[] Parameters
        {
            get;
        }

        #endregion

        #region Interface Method Declaration
        void Open();
        void BeginTransaction();
        void CommitTransaction();
        void CreateParameters(int paramsCount);
        void AddParameters(int index, string paramName, object objValue);
        IDataReader ExecuteReader(CommandType commandType, string commandText);
        DataSet ExecuteDataSet(CommandType commandType, string commandText);
        object ExecuteScalar(CommandType commandType, string commandText);
        int ExecuteNonQuery(CommandType commandType, string commandText);
        void CloseReader();
        void Close();
        void Dispose();
        #endregion

    }
}
