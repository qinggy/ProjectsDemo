using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Configuration;
using System.Reflection;
using DinoTechDataSyncService.Domain;
using DinoTechDataSyncService.Repository.Utilities;
using System.Collections.Concurrent;
using DinoTechDataSyncService.Utility;

namespace DinoTechDataSyncService.Repository
{
    public class DinoTechDataSyncServiceHandler
    {
        #region Public Member Variable
        public IDBInterface dbInterface = null;
        public string _DBConnStr = string.Empty;
        public string _DBProv;
        public string _success = "SUCCESS";
        public string _error = "ERROR";
        public string _warning = "WARNING";
        #endregion

        #region Private Member Variable
        private DataProvider _dataproviderType;
        #endregion

        #region Constructor
        public DinoTechDataSyncServiceHandler()
        {

            _DBProv = ConfigurationSettings.AppSettings["DBProvider"].ToString();
            _dataproviderType = SetDBProvider(_DBProv);
        }
        #endregion

        #region Private Memeber Variable

        /// <summary>
        /// SetDBInfo Function will 
        /// </summary>
        private void SetDBInfo()
        {
            _DBProv = ConfigurationSettings.AppSettings["DBProvider"].ToString();

            _dataproviderType = SetDBProvider(_DBProv);
            dbInterface = new DataAccessLayer(_dataproviderType);
            _DBConnStr = Convert.ToString(ConfigurationManager.ConnectionStrings["DBConnString"]);
            dbInterface.ConnectionString = _DBConnStr;
        }

        /// <summary>
        /// Set the DB Access Provider Type
        /// </summary>
        /// <param name="_providerName"></param>
        /// <returns></returns>
        private DataProvider SetDBProvider(string _providerName)
        {
            switch (_providerName)
            {
                case "DataProvider.SqlServer":
                    return DataProvider.SqlServer;
                case "DataProvider.OleDb":
                    return DataProvider.OleDb;
                case "DataProvider.Odbc":
                    return DataProvider.Odbc;
                default:
                    return DataProvider.SqlServer;

            }
        }

        #endregion

        public User ValidateUser(string userId, string password, string timeZoneOffSet)
        {

            try
            {
                User objUser = null;
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    DataTable dtUser = new DataTable();
                    string sqlQuery = string.Empty;
                    connection.Open();

                    sqlQuery = "SELECT UserId, FirstName, LastName,EmailId, TokenId FROM Users WHERE EmailId='" + userId + "' AND Password='" + password + "'";

                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtUser);



                    if (dtUser != null && dtUser.Rows.Count == 1)
                    {
                        objUser = new User();
                        objUser.UserId = Convert.ToInt32(dtUser.Rows[0]["UserId"]);
                        objUser.Email = dtUser.Rows[0]["EmailId"].ToString();
                        objUser.FirstName = dtUser.Rows[0]["FirstName"].ToString();
                        objUser.LastName = dtUser.Rows[0]["LastName"].ToString();
                        objUser.Token = dtUser.Rows[0]["TokenId"].ToString();

                        //Update TimeZoneOffSet in User Setting
                        sqlQuery = "SELECT ParameterName FROM UserSettings WHERE UserId='" + objUser.UserId + "'  AND ParameterName='TimeZoneOffSet'";

                        //sqlQuery = "SELECT ParameterName FROM UserSettings WHERE UserId='" + objUser.UserId + "'  AND ParameterName='TimeZoneOffSet' AND ParameterValue='" + timeZoneOffSet + "' AND ParameterValue IS NOT NULL";
                        DataTable dtUserSettings = new DataTable();
                        cmd = new SqlCommand(sqlQuery, connection);

                        adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtUserSettings);

                        if (dtUserSettings.Rows.Count == 0)
                        {
                            sqlQuery = "INSERT INTO UserSettings(ParameterName,ParameterValue, UserId) VALUES('TimeZoneOffSet','" + timeZoneOffSet + "'," + objUser.UserId + ")";
                            cmd = new SqlCommand(sqlQuery, connection);
                            cmd.ExecuteNonQuery();
                        }

                        return objUser;

                    }
                    cmd.Dispose();
                    adapter.Dispose();

                    return objUser;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUserToken(int userId, string token)
        {
            try
            {
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    DataTable dtUser = new DataTable();
                    string sqlQuery = string.Empty;
                    connection.Open();
                    sqlQuery = "UPDATE Users SET TokenId='" + token + "' WHERE UserId='" + userId + "'";

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    int result = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (result > 0)
                        return true;


                    return false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ValidateUserToken(string token)
        {
            try
            {
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    DataTable dtUser = new DataTable();
                    string sqlQuery = string.Empty;
                    connection.Open();
                    sqlQuery = "SELECT Count(*) FROM Users WHERE  TokenId='" + token + "'";

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                    if (result == 1)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateFinalUpdationOnSyncTables(int companyId)
        {
            try
            {
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    string sqlQuery = string.Empty;
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SSP_Sync_UpdateFinalUpdationOnSyncTables";
                    cmd.Parameters.AddWithValue("@CompanyId", companyId);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Logging

        public int CreateMainLogEntry(int companyId)
        {

            try
            {
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    int logId = 0;
                    DataTable dtUser = new DataTable();
                    string sqlQuery = string.Empty;
                    connection.Open();


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SSP_Sync_CreateMainLogEntry";

                    cmd.Parameters.AddWithValue("@CompanyId", companyId);
                    cmd.Parameters.AddWithValue("@LogStartDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@LogStatus", 1);

                    SqlParameter sqlLogId = new SqlParameter("@LogId", SqlDbType.Int);

                    sqlLogId.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(sqlLogId);

                    cmd.ExecuteNonQuery();

                    int.TryParse(sqlLogId.Value.ToString(), out logId);

                    connection.Close();

                    return logId;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateMainLogEntryOnSyncComplete(int mainLogId, int status, string description)
        {
            try
            {
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    DataTable dtUser = new DataTable();
                    string sqlQuery = string.Empty;
                    connection.Open();

                    sqlQuery = "UPDATE LogSync SET LogStatus=" + status + ", Description='" + description + "', LogEndDate='" + DateTime.Now + "' WHERE LogId='" + mainLogId + "'";

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    int result = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (result > 0)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CreateDetailLogEntry(int mainLogId, string tableName)
        {
            try
            {
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    int detailLogId = 0;
                    DataTable dtUser = new DataTable();
                    string sqlQuery = string.Empty;
                    connection.Open();


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SSP_Sync_CreateDetailLogEntry";

                    cmd.Parameters.AddWithValue("@MainLogId", mainLogId);
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@LogStatus", 1);

                    SqlParameter sqlDetailLogId = new SqlParameter("@DetailLogId", SqlDbType.Int);

                    sqlDetailLogId.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(sqlDetailLogId);

                    cmd.ExecuteNonQuery();

                    int.TryParse(sqlDetailLogId.Value.ToString(), out detailLogId);

                    connection.Close();

                    return detailLogId;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateDetailLogEntry(int deatailLogId, int status, string description)
        {
            try
            {
                SetDBInfo();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    DataTable dtUser = new DataTable();
                    string sqlQuery = string.Empty;
                    connection.Open();
                    sqlQuery = "UPDATE LogSyncDetail SET LogStatus=" + status + ", Description='" + description + "' WHERE LogSyncDetailId='" + deatailLogId + "'";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    int result = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCompanyTableRecCountInfo(int companyId, string tableName)
        {
            try
            {


                SetDBInfo();

                using (var connection = new SqlConnection(_DBConnStr))
                {
                    connection.Open();
                    DataTable dtSyncRecordCount = new DataTable();

                    string sqlQuery = string.Empty;

                    sqlQuery = "SELECT  SyncRecordCount FROM CompanyTableSyncHistory WHERE UserId=" + companyId + " AND TableName='" + tableName + "'";


                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    cmd.Connection = connection;
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;



                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtSyncRecordCount);
                    if (dtSyncRecordCount.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtSyncRecordCount.Rows[0]["SyncRecordCount"]);
                    }
                    else
                    {
                        //Create history record for table for company

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@UserId", companyId);
                        cmd.Parameters.AddWithValue("@TableName", tableName);
                        cmd.Parameters.AddWithValue("@SyncRecordCount", 0);
                        cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);

                        sqlQuery = "INSERT INTO CompanyTableSyncHistory(UserId,TableName,SyncRecordCount,UpdatedOn) VALUES(@UserId,@TableName,@SyncRecordCount,@UpdatedOn)";
                        cmd.CommandText = sqlQuery;
                        cmd.ExecuteNonQuery();
                        return 0;
                    }




                    cmd.Dispose();


                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }




        }

        public string GetCompanyTableLastSyncDateTimeInfo(int companyId, string tableName, int bmftId)
        {
            try
            {
                SetDBInfo();

                using (var connection = new SqlConnection(_DBConnStr))
                {
                    connection.Open();
                    DataTable dtSyncRecordCount = new DataTable();
                    string sqlQuery = "SELECT SyncedUpToDateTime FROM CompanyTableSyncHistory WHERE UserId=" + companyId + " AND TableName='" + tableName + "' AND BmfId=" + bmftId;

                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    cmd.Connection = connection;
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtSyncRecordCount);
                    cmd.Dispose();
                    //SystemLogHelper.Logger.Info(string.Format("查询当前仪表参数'{0}'在指定表'{1}'中的最近一条记录上传时间，查询语句\n\r'{2}'"), bmftId, tableName, sqlQuery);
                    if (dtSyncRecordCount.Rows.Count > 0)
                    {
                        return Convert.ToString(dtSyncRecordCount.Rows[0]["SyncedUpToDateTime"]);
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception Ex)
            {
                SystemLogHelper.Logger.Error(Ex.InnerException ?? Ex, Ex.Message);
                throw Ex;
            }
        }

        public bool UpdateCompanyTableLastSyncDateTimeInfo(int companyId, string tableName, string currentTableLastSyncDateTime)
        {
            try
            {


                SetDBInfo();

                using (var connection = new SqlConnection(_DBConnStr))
                {
                    connection.Open();
                    DataTable dtSyncRecordCount = new DataTable();

                    string sqlQuery = string.Empty;



                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    cmd.Connection = connection;
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;


                    //Create history record for table for company

                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@UserId", companyId);
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@LastSyncDateTime", currentTableLastSyncDateTime);
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);

                    sqlQuery = "UPDATE CompanyTableSyncHistory SET SyncedUpToDateTime=@LastSyncDateTime,UpdatedOn=@UpdatedOn WHERE  UserId=@UserId  AND TableName=@TableName";
                    cmd.CommandText = sqlQuery;
                    cmd.ExecuteNonQuery();
                    return true;
                }




            }

            catch (Exception ex)
            {
                throw ex;

            }




        }

        public bool UpdateCompanyTableRecCountInfo(int companyId, string tableName, int currentTableSyncRecCountInfo)
        {
            try
            {


                SetDBInfo();

                using (var connection = new SqlConnection(_DBConnStr))
                {
                    connection.Open();
                    DataTable dtSyncRecordCount = new DataTable();

                    string sqlQuery = string.Empty;

                    sqlQuery = "SELECT  SyncRecordCount FROM CompanyTableSyncHistory WHERE UserId=" + companyId + " AND TableName='" + tableName + "'";


                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    cmd.Connection = connection;
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;


                    //Create history record for table for company

                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@UserId", companyId);
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@SyncRecordCount", currentTableSyncRecCountInfo);
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);

                    sqlQuery = "UPDATE CompanyTableSyncHistory SET SyncRecordCount=@SyncRecordCount,UpdatedOn=@UpdatedOn WHERE  UserId=@UserId  AND TableName=@TableName";
                    cmd.CommandText = sqlQuery;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Data Sync from 2nd Edtion to Cloud base on Mapping Configurations

        public int SyncEnergyMeterDataToCloud(EnergyMeterHistoryDataRecord energyMeterHistoryDataRecord)
        {

            try
            {
                SetDBInfo();

                int responseCode = 0;

                using (var connection = new SqlConnection(_DBConnStr))
                {
                    string sqlQuery = string.Empty;

                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = connection;
                    connection.Open();

                    DataTable dtEnergys = new DataTable();

                    if (energyMeterHistoryDataRecord.DurationType == "Minute")
                    {

                        sqlQuery = "SELECT Id FROM MinutesRecord WHERE BaseMeterFieldTypeId=" + energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId + " AND Htime='" + energyMeterHistoryDataRecord.HTime + "'";

                        dtEnergys = new DataTable();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtEnergys);

                        if (dtEnergys.Rows.Count > 0)
                        {
                            //Send Response Error for Row already exist on cloud
                            responseCode = 1024;

                        }
                        else
                        {
                            cmd.Parameters.Clear();
                            //insert records in DB 

                            cmd.Parameters.AddWithValue("@BaseMeterFieldTypeId", energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId);

                            cmd.Parameters.AddWithValue("@TotalData", energyMeterHistoryDataRecord.TotalData);
                            cmd.Parameters.AddWithValue("@HTime", energyMeterHistoryDataRecord.HTime);
                            cmd.Parameters.AddWithValue("@TotalMoney", energyMeterHistoryDataRecord.TotalMoney);

                            cmd.Parameters.AddWithValue("@SameCompareTotalData", energyMeterHistoryDataRecord.SameCompareTotalData);

                            cmd.Parameters.AddWithValue("@LinkCompareTotalData", energyMeterHistoryDataRecord.LinkCompareTotalData);

                            cmd.Parameters.AddWithValue("@SameCompareTotalMoney", energyMeterHistoryDataRecord.SameCompareTotalMoney);
                            cmd.Parameters.AddWithValue("@LinkCompareTotalMoney", energyMeterHistoryDataRecord.LinkCompareTotalMoney);






                            sqlQuery = "INSERT INTO MinutesRecord(TotalData,TotalMoney,SameCompareTotalData,LinkCompareTotalData,SameCompareTotalMoney, LinkCompareTotalMoney,HTime,BaseMeterFieldTypeId) VALUES(@TotalData,@TotalMoney,@SameCompareTotalData,@LinkCompareTotalData,@SameCompareTotalMoney, @LinkCompareTotalMoney,@HTime,@BaseMeterFieldTypeId)";


                            cmd.CommandText = sqlQuery;

                            cmd.ExecuteNonQuery();

                            responseCode = 1000;
                        }
                    }

                    else if (energyMeterHistoryDataRecord.DurationType == "Hour")
                    {
                        sqlQuery = "SELECT Id FROM HourRecord WHERE BaseMeterFieldTypeId=" + energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId + " AND Htime='" + energyMeterHistoryDataRecord.HTime + "'";
                        dtEnergys = new DataTable();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtEnergys);

                        if (dtEnergys.Rows.Count > 0)
                        {
                            //Send Response Error for Row already exist on cloud
                            responseCode = 1024;

                        }
                        else
                        {
                            cmd.Parameters.Clear();
                            //insert records in DB 

                            cmd.Parameters.AddWithValue("@BaseMeterFieldTypeId", energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId);

                            cmd.Parameters.AddWithValue("@TotalData", energyMeterHistoryDataRecord.TotalData);
                            cmd.Parameters.AddWithValue("@HTime", energyMeterHistoryDataRecord.HTime);
                            cmd.Parameters.AddWithValue("@TotalMoney", energyMeterHistoryDataRecord.TotalMoney);
                            cmd.Parameters.AddWithValue("@SameCompareTotalData", energyMeterHistoryDataRecord.SameCompareTotalData);

                            cmd.Parameters.AddWithValue("@LinkCompareTotalData", energyMeterHistoryDataRecord.LinkCompareTotalData);
                            cmd.Parameters.AddWithValue("@SameCompareTotalMoney", energyMeterHistoryDataRecord.SameCompareTotalMoney);
                            cmd.Parameters.AddWithValue("@LinkCompareTotalMoney", energyMeterHistoryDataRecord.LinkCompareTotalMoney);





                            sqlQuery = "INSERT INTO HourRecord(TotalData,TotalMoney,SameCompareTotalData,LinkCompareTotalData,SameCompareTotalMoney, LinkCompareTotalMoney,HTime,BaseMeterFieldTypeId) VALUES(@TotalData,@TotalMoney,@SameCompareTotalData,@LinkCompareTotalData,@SameCompareTotalMoney, @LinkCompareTotalMoney,@HTime,@BaseMeterFieldTypeId)";


                            cmd.CommandText = sqlQuery;

                            cmd.ExecuteNonQuery();
                            responseCode = 1000;
                        }
                    }
                    else if (energyMeterHistoryDataRecord.DurationType == "Day")
                    {

                        sqlQuery = "SELECT Id FROM DayRecord WHERE BaseMeterFieldTypeId=" + energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId + " AND Htime='" + energyMeterHistoryDataRecord.HTime + "'";
                        dtEnergys = new DataTable();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtEnergys);

                        if (dtEnergys.Rows.Count > 0)
                        {
                            //Send Response Error for Row already exist on cloud
                            responseCode = 1024;
                        }
                        else
                        {
                            cmd.Parameters.Clear();
                            //insert records in DB 

                            cmd.Parameters.AddWithValue("@BaseMeterFieldTypeId", energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId);

                            cmd.Parameters.AddWithValue("@TotalData", energyMeterHistoryDataRecord.TotalData);
                            cmd.Parameters.AddWithValue("@HTime", energyMeterHistoryDataRecord.HTime);
                            cmd.Parameters.AddWithValue("@TotalMoney", energyMeterHistoryDataRecord.TotalMoney); cmd.Parameters.AddWithValue("@SameCompareTotalData", energyMeterHistoryDataRecord.SameCompareTotalData);

                            cmd.Parameters.AddWithValue("@LinkCompareTotalData", energyMeterHistoryDataRecord.LinkCompareTotalData);
                            cmd.Parameters.AddWithValue("@SameCompareTotalMoney", energyMeterHistoryDataRecord.SameCompareTotalMoney);
                            cmd.Parameters.AddWithValue("@LinkCompareTotalMoney", energyMeterHistoryDataRecord.LinkCompareTotalMoney);





                            sqlQuery = "INSERT INTO DayRecord(TotalData,TotalMoney,SameCompareTotalData,LinkCompareTotalData,SameCompareTotalMoney, LinkCompareTotalMoney,HTime,BaseMeterFieldTypeId) VALUES(@TotalData,@TotalMoney,@SameCompareTotalData,@LinkCompareTotalData,@SameCompareTotalMoney, @LinkCompareTotalMoney,@HTime,@BaseMeterFieldTypeId)";


                            cmd.CommandText = sqlQuery;

                            cmd.ExecuteNonQuery();

                            responseCode = 1000;

                        }
                    }
                    else if (energyMeterHistoryDataRecord.DurationType == "Month")
                    {
                        sqlQuery = "SELECT Id FROM MonthRecord WHERE BaseMeterFieldTypeId=" + energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId + " AND Htime='" + energyMeterHistoryDataRecord.HTime + "'";
                        dtEnergys = new DataTable();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtEnergys);

                        if (dtEnergys.Rows.Count > 0)
                        {
                            //Send Response Error for Row already exist on cloud
                            responseCode = 1024;

                        }
                        else
                        {
                            cmd.Parameters.Clear();
                            //insert records in DB 

                            cmd.Parameters.AddWithValue("@BaseMeterFieldTypeId", energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId);

                            cmd.Parameters.AddWithValue("@TotalData", energyMeterHistoryDataRecord.TotalData);
                            cmd.Parameters.AddWithValue("@HTime", energyMeterHistoryDataRecord.HTime);
                            cmd.Parameters.AddWithValue("@TotalMoney", energyMeterHistoryDataRecord.TotalMoney); cmd.Parameters.AddWithValue("@SameCompareTotalData", energyMeterHistoryDataRecord.SameCompareTotalData);

                            cmd.Parameters.AddWithValue("@LinkCompareTotalData", energyMeterHistoryDataRecord.LinkCompareTotalData);
                            cmd.Parameters.AddWithValue("@SameCompareTotalMoney", energyMeterHistoryDataRecord.SameCompareTotalMoney);
                            cmd.Parameters.AddWithValue("@LinkCompareTotalMoney", energyMeterHistoryDataRecord.LinkCompareTotalMoney);




                            sqlQuery = "INSERT INTO MonthRecord(TotalData,TotalMoney,SameCompareTotalData,LinkCompareTotalData,SameCompareTotalMoney, LinkCompareTotalMoney,HTime,BaseMeterFieldTypeId) VALUES(@TotalData,@TotalMoney,@SameCompareTotalData,@LinkCompareTotalData,@SameCompareTotalMoney, @LinkCompareTotalMoney,@HTime,@BaseMeterFieldTypeId)";


                            cmd.CommandText = sqlQuery;

                            cmd.ExecuteNonQuery();
                            responseCode = 1000;

                        }
                    }
                    else if (energyMeterHistoryDataRecord.DurationType == "Year")
                    {
                        sqlQuery = "SELECT Id FROM YearRecord WHERE BaseMeterFieldTypeId=" + energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId + " AND Htime='" + energyMeterHistoryDataRecord.HTime + "'";
                        dtEnergys = new DataTable();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtEnergys);

                        if (dtEnergys.Rows.Count > 0)
                        {
                            //Send Response Error for Row already exist on cloud
                            responseCode = 1024;

                        }
                        else
                        {
                            cmd.Parameters.Clear();
                            //insert records in DB 

                            cmd.Parameters.AddWithValue("@BaseMeterFieldTypeId", energyMeterHistoryDataRecord.CloudBaseMeterFieldTypeId);

                            cmd.Parameters.AddWithValue("@TotalData", energyMeterHistoryDataRecord.TotalData);
                            cmd.Parameters.AddWithValue("@HTime", energyMeterHistoryDataRecord.HTime);
                            cmd.Parameters.AddWithValue("@TotalMoney", energyMeterHistoryDataRecord.TotalMoney); cmd.Parameters.AddWithValue("@SameCompareTotalData", energyMeterHistoryDataRecord.SameCompareTotalData);

                            cmd.Parameters.AddWithValue("@LinkCompareTotalData", energyMeterHistoryDataRecord.LinkCompareTotalData);
                            cmd.Parameters.AddWithValue("@SameCompareTotalMoney", energyMeterHistoryDataRecord.SameCompareTotalMoney);
                            cmd.Parameters.AddWithValue("@LinkCompareTotalMoney", energyMeterHistoryDataRecord.LinkCompareTotalMoney);





                            sqlQuery = "INSERT INTO YearRecord(TotalData,TotalMoney,SameCompareTotalData,LinkCompareTotalData,SameCompareTotalMoney, LinkCompareTotalMoney,HTime,BaseMeterFieldTypeId) VALUES(@TotalData,@TotalMoney,@SameCompareTotalData,@LinkCompareTotalData,@SameCompareTotalMoney, @LinkCompareTotalMoney,@HTime,@BaseMeterFieldTypeId)";


                            cmd.CommandText = sqlQuery;

                            cmd.ExecuteNonQuery();
                            responseCode = 1000;

                        }



                    }
                }
                return responseCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public List<EnergyPowerMeterDataTree> GetListOfAllAreaMeterAndParametersOnCloud(int companyId)
        {

            try
            {

                SetDBInfo();


                List<EnergyPowerMeterDataTree> listEnergyPowerMeterDataTree = new List<EnergyPowerMeterDataTree>();
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    string sqlQuery = string.Empty;

                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = connection;
                    connection.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SSP_DataSync_GetListOfAllAreaMeterAndParameters";

                    cmd.Parameters.AddWithValue("@CompanyId", companyId);


                    DataTable dtEnergys = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtEnergys);

                    if (dtEnergys.Rows.Count > 0)
                    {


                        EnergyPowerMeterDataTree objEnergyPowerMeterDataTree = new EnergyPowerMeterDataTree();

                        for (int i = 0; i < dtEnergys.Rows.Count; i++)
                        {
                            objEnergyPowerMeterDataTree = new EnergyPowerMeterDataTree();

                            objEnergyPowerMeterDataTree.Id = Convert.ToInt32(dtEnergys.Rows[i]["Id"]);
                            objEnergyPowerMeterDataTree.Name = Convert.ToString(dtEnergys.Rows[i]["Name"]);
                            if (dtEnergys.Rows[i]["ParentId"] != null && dtEnergys.Rows[i]["ParentId"].ToString() != "")
                                objEnergyPowerMeterDataTree.ParentId = Convert.ToInt32(dtEnergys.Rows[i]["ParentId"]);
                            else
                                objEnergyPowerMeterDataTree.ParentId = 0;

                            objEnergyPowerMeterDataTree.NodeType = Convert.ToInt32(dtEnergys.Rows[i]["NodeType"]);

                            listEnergyPowerMeterDataTree.Add(objEnergyPowerMeterDataTree);
                        }



                    }



                    cmd.Dispose();


                }
                return listEnergyPowerMeterDataTree;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Sync Base Information

        //Energies
        public int SyncEnergyItemsToCloudSystemInSequence(int companyId, List<Energy> energySyncItems)
        {
            SetDBInfo();

            int responseCode = 0;
            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                try
                {
                    string sqlQuery = string.Empty;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;

                    DataTable dtEnergys = new DataTable();

                    for (int i = 0; i < energySyncItems.Count; i++)
                    {
                        sqlQuery = "Select Id FROM Energys WHERE UserId='" + companyId + "' AND SyncId='" + energySyncItems[i].Id + "'";

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        dtEnergys = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtEnergys);
                        SystemLogHelper.Logger.Info("SQL:" + sqlQuery);

                        if (dtEnergys.Rows.Count == 0)
                        {
                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@SyncId", energySyncItems[i].Id);
                            cmd.Parameters.AddWithValue("@Name", energySyncItems[i].Name);
                            cmd.Parameters.AddWithValue("@IsEnabled", energySyncItems[i].IsEnabled);
                            cmd.Parameters.AddWithValue("@Remark", energySyncItems[i].Remark ?? "");
                            cmd.Parameters.AddWithValue("@EnergyType", energySyncItems[i].EnergyType);
                            cmd.Parameters.AddWithValue("@UserId", companyId);
                            cmd.Parameters.AddWithValue("@RecordDeleted", 0);

                            sqlQuery = "INSERT INTO Energys(SyncId,Name,IsEnabled,Remark,EnergyType,UserId, RecordDeleted) VALUES(@SyncId,@Name,@IsEnabled,@Remark,@EnergyType,@UserId,@RecordDeleted)";

                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();
                            responseCode = 1001;
                            SystemLogHelper.Logger.Info("SQL:" + sqlQuery);
                        }
                        else
                        { //update records

                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@SyncId", energySyncItems[i].Id);
                            cmd.Parameters.AddWithValue("@Name", energySyncItems[i].Name ?? "");
                            cmd.Parameters.AddWithValue("@IsEnabled", energySyncItems[i].IsEnabled);
                            cmd.Parameters.AddWithValue("@Remark", energySyncItems[i].Remark ?? "");
                            cmd.Parameters.AddWithValue("@EnergyType", energySyncItems[i].EnergyType);
                            cmd.Parameters.AddWithValue("@UserId", companyId);

                            int Id = Convert.ToInt32(dtEnergys.Rows[0]["Id"]);
                            sqlQuery = "Update Energys SET Name=@Name, IsEnabled=@IsEnabled ,Remark=@Remark,EnergyType=@EnergyType,UserId=@UserId WHERE Id=" + Id;

                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();
                            responseCode = 1001;
                            SystemLogHelper.Logger.Info("SQL:" + sqlQuery);
                        }
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.Message, ex.InnerException ?? ex);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return responseCode;
        }

        //MeterModel
        public int SyncMeterModelItemsToCloudSystemInSequence(int companyId, List<MeterModel> meterModelSyncItems)
        {
            SetDBInfo();

            int responseCode = 0;
            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                try
                {
                    string sqlQuery = string.Empty;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;

                    DataTable dtMeterModels = new DataTable();
                    for (int i = 0; i < meterModelSyncItems.Count; i++)
                    {
                        Boolean ProcessCurrentRec = true;
                        responseCode = 1001;
                        Nullable<int> SystemEnergyId = null;

                        sqlQuery = "Select Id FROM MeterModels WHERE UserId='" + companyId + "' AND  SyncId=N'" + meterModelSyncItems[i].Id + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;

                        dtMeterModels = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtMeterModels);
                        SystemLogHelper.Logger.Info("仪表型号 " + sqlQuery);

                        if (dtMeterModels.Rows.Count == 0)
                        {
                            //Get the associated EnergyId for metermodel
                            if (!string.IsNullOrEmpty(meterModelSyncItems[i].EnergyId))
                            {
                                DataTable dtEnergyInfo = new DataTable();

                                sqlQuery = "SELECT Id FROM Energys WHERE UserId='" + companyId + "' AND SyncId='" + meterModelSyncItems[i].EnergyId + "'";
                                cmd.CommandText = sqlQuery;

                                adapter = new SqlDataAdapter(cmd);
                                adapter.Fill(dtEnergyInfo);

                                if (dtEnergyInfo.Rows.Count == 0)
                                {
                                    ProcessCurrentRec = false;
                                    string errorMessage = "Record with SyncId: " + meterModelSyncItems[i].Id + " could not be processed because associated EnergyId in meter model item  does not exist in Energies list of the company";
                                    SystemLogHelper.Logger.Error(errorMessage);
                                }
                                else
                                {
                                    ProcessCurrentRec = true;
                                    SystemEnergyId = Convert.ToInt32(dtEnergyInfo.Rows[0]["Id"]);
                                }
                            }

                            if (ProcessCurrentRec)
                            {
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@SyncId", meterModelSyncItems[i].Id);
                                cmd.Parameters.AddWithValue("@Name", meterModelSyncItems[i].Name ?? "");
                                cmd.Parameters.AddWithValue("@MinValue", meterModelSyncItems[i].MinValue);
                                cmd.Parameters.AddWithValue("@MaxValue", meterModelSyncItems[i].MaxValue);
                                cmd.Parameters.AddWithValue("@Accuracy", meterModelSyncItems[i].Accuracy);
                                cmd.Parameters.AddWithValue("@CanReadBack", meterModelSyncItems[i].CanReadBack);
                                cmd.Parameters.AddWithValue("@RecordDeleted", 0);
                                cmd.Parameters.AddWithValue("@Remark", meterModelSyncItems[i].Remark ?? "");
                                if (meterModelSyncItems[i].EnergyId != null)
                                    cmd.Parameters.AddWithValue("@EnergySyncId", meterModelSyncItems[i].EnergyId);
                                else
                                    cmd.Parameters.AddWithValue("@EnergySyncId", DBNull.Value);

                                cmd.Parameters.AddWithValue("@UserId", companyId);

                                if (SystemEnergyId != null)
                                    cmd.Parameters.AddWithValue("@EnergyId", SystemEnergyId);
                                else
                                    cmd.Parameters.AddWithValue("@EnergyId", DBNull.Value);

                                sqlQuery = "INSERT INTO MeterModels(SyncId,Name,MinValue,MaxValue,Accuracy,CanReadBack,RecordDeleted,Remark,EnergySyncId,EnergyId,UserId) VALUES(@SyncId,@Name,@MinValue,@MaxValue,@Accuracy,@CanReadBack,@RecordDeleted,@Remark,@EnergySyncId,@EnergyId,@UserId)";

                                cmd.CommandText = sqlQuery;
                                cmd.ExecuteNonQuery();
                                responseCode = 1001;
                                SystemLogHelper.Logger.Info("插入仪表型号 " + sqlQuery);
                            }
                        }
                        else
                        { //update records

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@SyncId", meterModelSyncItems[i].Id);
                            cmd.Parameters.AddWithValue("@Name", meterModelSyncItems[i].Name ?? "");
                            cmd.Parameters.AddWithValue("@MinValue", meterModelSyncItems[i].MinValue);
                            cmd.Parameters.AddWithValue("@MaxValue", meterModelSyncItems[i].MaxValue);
                            cmd.Parameters.AddWithValue("@Accuracy", meterModelSyncItems[i].Accuracy);
                            cmd.Parameters.AddWithValue("@CanReadBack", meterModelSyncItems[i].CanReadBack);
                            cmd.Parameters.AddWithValue("@Remark", meterModelSyncItems[i].Remark ?? "");
                            if (meterModelSyncItems[i].EnergyId != null)
                                cmd.Parameters.AddWithValue("@EnergySyncId", meterModelSyncItems[i].EnergyId);
                            else
                                cmd.Parameters.AddWithValue("@EnergySyncId", DBNull.Value);

                            if (SystemEnergyId != null)
                                cmd.Parameters.AddWithValue("@EnergyId", SystemEnergyId);
                            else
                                cmd.Parameters.AddWithValue("@EnergyId", DBNull.Value);

                            cmd.Parameters.AddWithValue("@UserId", companyId);

                            int Id = Convert.ToInt32(dtMeterModels.Rows[0]["Id"]);
                            sqlQuery = "Update MeterModels SET Name=@Name,MinValue=@MinValue, MaxValue=@MaxValue, Accuracy=@Accuracy, CanReadBack=@CanReadBack, Remark=@Remark WHERE Id=" + Id;

                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();
                            responseCode = 1001;
                            SystemLogHelper.Logger.Info("更新仪表型号 " + sqlQuery);
                        }
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error("仪表型号 " + ex.Message, ex.InnerException ?? ex);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return responseCode;
        }

        //FieldTypes
        public int SyncAcquisitionParametersItemsToCloudSystemSequence(int companyId, List<AcquisitionParameters> acquisitionParametersModelSyncItems)
        {
            SetDBInfo();
            int responseCode = 0;
            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                try
                {
                    string sqlQuery = string.Empty;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    //首先必须保证当前账号（公司）必须同时上传各个系统的参数，否则删除时，会删掉以往上传的系统的参数
                    sqlQuery = "delete from FieldTypes where UserId=" + companyId;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlQuery;
                    cmd.ExecuteNonQuery();
                    SystemLogHelper.Logger.Info("删除当前账号所有参数" + sqlQuery);

                    //DataTable acquisitionParamTable = Common.ToDataTable<AcquisitionParameters>(acquisitionParametersModelSyncItems);

                    for (int i = 0; i < acquisitionParametersModelSyncItems.Count; i++)
                    {
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@SyncId", acquisitionParametersModelSyncItems[i].Id);
                        cmd.Parameters.AddWithValue("@Name", acquisitionParametersModelSyncItems[i].Name ?? "");
                        cmd.Parameters.AddWithValue("@MeterDataType", acquisitionParametersModelSyncItems[i].MeterDataType); //企业版为数据类型 0： 累计值，1：瞬时值 etc. 但是目前Cityindata国外那边的处理是MeterDataType是无效的参数
                        cmd.Parameters.AddWithValue("@DefaultUnit", acquisitionParametersModelSyncItems[i].DefaultUnit ?? "");
                        cmd.Parameters.AddWithValue("@InventedParameter", acquisitionParametersModelSyncItems[i].InventedParameter);//企业版为是否是虚拟参数，不参与采集 Cityindata将MeterDataType参数值合并到该参数，并加入了状态量
                        cmd.Parameters.AddWithValue("@Remark", acquisitionParametersModelSyncItems[i].Remark ?? "");
                        cmd.Parameters.AddWithValue("@ParameterType", acquisitionParametersModelSyncItems[i].ParameterType); //系统类型0~9个系统
                        cmd.Parameters.AddWithValue("@UserId", companyId);
                        cmd.Parameters.AddWithValue("@RecordDeleted", 0);

                        sqlQuery = "INSERT INTO FieldTypes(SyncId,Name,MeterDataType,DefaultUnit,InventedParameter,Remark,UserId,ParameterType,RecordDeleted) VALUES(@SyncId,@Name,@MeterDataType,@DefaultUnit,@InventedParameter,@Remark,@UserId,@ParameterType,@RecordDeleted)";

                        cmd.CommandText = sqlQuery;
                        cmd.ExecuteNonQuery();
                        SystemLogHelper.Logger.Info("插入参数" + sqlQuery);
                        responseCode = 1001;
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return responseCode;
        }

        //Area/Regions
        public int SyncAreaItemsToCloudSystemInSequence(int companyId, List<Areas> areaSyncItems)
        {
            SetDBInfo();
            int responseCode = 0;

            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                try
                {
                    string sqlQuery = string.Empty;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;

                    DataTable dtAreas = new DataTable();
                    //Ist Check item  does not Exist in DB(if exist then update the records other wise insert the  record)
                    for (int i = 0; i < areaSyncItems.Count; i++)
                    {
                        sqlQuery = "Select Id FROM Areas WHERE UserId='" + companyId + "' AND SyncId='" + areaSyncItems[i].Id + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        dtAreas = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtAreas);

                        if (dtAreas.Rows.Count == 0)
                        {
                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@SyncId", areaSyncItems[i].Id);
                            cmd.Parameters.AddWithValue("@Name", areaSyncItems[i].Name ?? "");
                            if (areaSyncItems[i].ParentAreaId != null)
                                cmd.Parameters.AddWithValue("@ParentSyncId", areaSyncItems[i].ParentAreaId);
                            else
                                cmd.Parameters.AddWithValue("@ParentSyncId", DBNull.Value);

                            cmd.Parameters.AddWithValue("@Address", areaSyncItems[i].Address ?? "");
                            cmd.Parameters.AddWithValue("@Acreage", areaSyncItems[i].Acreage ?? "");
                            cmd.Parameters.AddWithValue("@Contact", areaSyncItems[i].Contact ?? "");
                            cmd.Parameters.AddWithValue("@Phone", areaSyncItems[i].Phone ?? "");
                            cmd.Parameters.AddWithValue("@Fax", areaSyncItems[i].Fax ?? "");
                            cmd.Parameters.AddWithValue("@ZipCode", areaSyncItems[i].ZipCode ?? "");
                            cmd.Parameters.AddWithValue("@Email", areaSyncItems[i].Email ?? "");
                            cmd.Parameters.AddWithValue("@Remark", areaSyncItems[i].Remark ?? "");
                            cmd.Parameters.AddWithValue("@DisplayedOrder", areaSyncItems[i].DisplayedOrder);
                            cmd.Parameters.AddWithValue("@AreaType", areaSyncItems[i].AreaType);
                            if (areaSyncItems[i].Coordinate != null)
                                cmd.Parameters.AddWithValue("@Coordinate", areaSyncItems[i].Coordinate);
                            else
                                cmd.Parameters.AddWithValue("@Coordinate", DBNull.Value);
                            cmd.Parameters.AddWithValue("@UserId", companyId);
                            cmd.Parameters.AddWithValue("@RecordDeleted", 0);

                            sqlQuery = "INSERT INTO Areas(SyncId,Name,ParentSyncId,Address,Acreage,Contact,Phone,Fax,ZipCode,Email,Remark,RecordDeleted,DisplayedOrder,Coordinate,UserId, AreaType) VALUES(@SyncId,@Name,@ParentSyncId,@Address,@Acreage,@Contact,@Phone,@Fax,@ZipCode,@Email,@Remark,@RecordDeleted,@DisplayedOrder,@Coordinate,@UserId,@AreaType)";
                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();
                            responseCode = 1001;
                        }
                        else
                        { //update records
                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@SyncId", areaSyncItems[i].Id);
                            cmd.Parameters.AddWithValue("@Name", areaSyncItems[i].Name ?? "");
                            if (areaSyncItems[i].ParentAreaId != null)
                                cmd.Parameters.AddWithValue("@ParentSyncId", areaSyncItems[i].ParentAreaId);
                            else
                                cmd.Parameters.AddWithValue("@ParentSyncId", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Address", areaSyncItems[i].Address ?? "");
                            cmd.Parameters.AddWithValue("@Acreage", areaSyncItems[i].Acreage ?? "");
                            cmd.Parameters.AddWithValue("@Contact", areaSyncItems[i].Contact ?? "");
                            cmd.Parameters.AddWithValue("@Phone", areaSyncItems[i].Phone ?? "");
                            cmd.Parameters.AddWithValue("@Fax", areaSyncItems[i].Fax ?? "");
                            cmd.Parameters.AddWithValue("@ZipCode", areaSyncItems[i].ZipCode ?? "");
                            cmd.Parameters.AddWithValue("@Email", areaSyncItems[i].Email ?? "");
                            cmd.Parameters.AddWithValue("@Remark", areaSyncItems[i].Remark ?? "");
                            cmd.Parameters.AddWithValue("@RecordDeleted", 0);
                            cmd.Parameters.AddWithValue("@DisplayedOrder", areaSyncItems[i].DisplayedOrder);
                            cmd.Parameters.AddWithValue("@AreaType", areaSyncItems[i].AreaType);

                            if (areaSyncItems[i].Coordinate != null)
                                cmd.Parameters.AddWithValue("@Coordinate", areaSyncItems[i].Coordinate);
                            else
                                cmd.Parameters.AddWithValue("@Coordinate", DBNull.Value);
                            cmd.Parameters.AddWithValue("@UserId", companyId);

                            int Id = Convert.ToInt32(dtAreas.Rows[0]["Id"]);
                            sqlQuery = "Update Areas SET Name=@Name,Acreage=@Acreage,Contact=@Contact,Phone=@Phone,Fax=@Fax,ZipCode=@ZipCode,Email=@Email,Remark=@Remark,RecordDeleted=@RecordDeleted,DisplayedOrder=@DisplayedOrder,Coordinate=@Coordinate,AreaType=@AreaType WHERE Id=" + Id;
                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();
                            responseCode = 1001;
                        }
                    }

                    UpdateAreaRegionAssociation(companyId, connection);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return responseCode;
        }

        //Sequence sync form MeterModel FieldType Association
        public int SyncMeterModelFieldTypeItemsToCloudSystemInSequence(int companyId, List<MeterModelFieldTypes> meterModelFieldTypes)
        {
            SetDBInfo();
            int responseCode = 0;
            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                try
                {
                    string sqlQuery = string.Empty;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    DataTable dtMeterModelFieldType = new DataTable();
                    int meterModelId = 0;
                    int fieldTypeId = 0;

                    //Ist Check MeterModel id  does not Exist in DB(if exist then update the records other wise insert the  record)
                    for (int i = 0; i < meterModelFieldTypes.Count; i++)
                    {
                        meterModelId = 0;
                        fieldTypeId = 0;

                        //Get MeterModelId in cloud
                        sqlQuery = "Select Id AS MeterModelId FROM MeterModels WHERE UserId=" + companyId + " AND SyncId=" + meterModelFieldTypes[i].MeterModelId;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;

                        dtMeterModelFieldType = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtMeterModelFieldType);

                        if (dtMeterModelFieldType.Rows.Count > 0)
                        {
                            meterModelId = Convert.ToInt32(dtMeterModelFieldType.Rows[0]["MeterModelId"]);
                        }
                        else
                        {
                            responseCode = 1026;
                            return responseCode;
                        }

                        //Get FieldTypeId in cloud
                        sqlQuery = "Select Id AS FieldTypeId  FROM FieldTypes WHERE UserId=" + companyId + " AND SyncId=" + meterModelFieldTypes[i].FieldTypeId;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        dtMeterModelFieldType = new DataTable();
                        adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtMeterModelFieldType);

                        if (dtMeterModelFieldType.Rows.Count > 0)
                        {
                            fieldTypeId = Convert.ToInt32(dtMeterModelFieldType.Rows[0]["FieldTypeId"]);
                        }
                        else
                        {
                            responseCode = 1027;
                            return responseCode;
                        }

                        //if  MeterModelId and FieldTypeId both exist  cloud
                        if (meterModelId > 0 && fieldTypeId > 0)
                        {
                            sqlQuery = "Select Id AS MeterModelFieldTypeId FROM MeterModelFieldTypes WHERE MeterModelId=" + meterModelId + " AND FieldTypeId=" + fieldTypeId;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sqlQuery;
                            dtMeterModelFieldType = new DataTable();
                            adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dtMeterModelFieldType);
                            //if MeterModelFieldTypeItem not exist on cloud then insert MeterModelFieldTypeItem to cloud
                            if (dtMeterModelFieldType.Rows.Count == 0)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@MeterModelId", meterModelId);
                                cmd.Parameters.AddWithValue("@FieldTypeId", fieldTypeId);
                                sqlQuery = "INSERT INTO MeterModelFieldTypes(MeterModelId,FieldTypeId) VALUES(@MeterModelId,@FieldTypeId)";
                                cmd.CommandText = sqlQuery;
                                cmd.ExecuteNonQuery();
                                responseCode = 1001;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return responseCode;
        }

        //Base Meters
        public int SyncBaseMeterItemsToCloudSystemInSequence(int companyId, List<BaseMeter> baseMeterSyncItems)
        {
            SetDBInfo();
            int responseCode = 0;
            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                try
                {
                    string sqlQuery = string.Empty;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;

                    DataTable dtBaseMeter = new DataTable();
                    for (int i = 0; i < baseMeterSyncItems.Count; i++)
                    {
                        Boolean ProcessCurrentRec = true;
                        responseCode = 1001;
                        Nullable<int> SystemParentAreaId = null;
                        Nullable<int> SystemMeterModelId = null;
                        Nullable<int> SystemEnergyId = null;

                        sqlQuery = "Select Id FROM BaseMeter WHERE UserId=" + companyId + " AND SyncId='" + baseMeterSyncItems[i].Id + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        dtBaseMeter = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtBaseMeter);
                        SystemLogHelper.Logger.Info("仪表查询" + sqlQuery);

                        if (dtBaseMeter.Rows.Count == 0)
                        {
                            DataTable dtInfo = new DataTable();
                            //Get the associated EnergyId for metermodel
                            if (!string.IsNullOrEmpty(baseMeterSyncItems[i].BaseMeterParentAreaId))
                            {
                                dtInfo = new DataTable();
                                sqlQuery = "SELECT Id FROM Areas WHERE UserId=" + companyId + " AND SyncId='" + baseMeterSyncItems[i].BaseMeterParentAreaId + "'";
                                cmd.CommandText = sqlQuery;
                                adapter = new SqlDataAdapter(cmd);
                                adapter.Fill(dtInfo);
                                SystemLogHelper.Logger.Info("区域查询" + sqlQuery);

                                if (dtInfo.Rows.Count == 0)
                                {
                                    ProcessCurrentRec = false;
                                    string errorMessage = "Record with SyncId: " + baseMeterSyncItems[i].Id + " could not be processed because associated ParentAreaId in BaseMeter  item  does not exist in Area list of the company";

                                    SystemLogHelper.Logger.Error(errorMessage);
                                }
                                else
                                {
                                    ProcessCurrentRec = true;
                                    SystemParentAreaId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
                                }
                            }

                            if (ProcessCurrentRec == true)
                            {
                                //Check Energy key
                                if (!string.IsNullOrEmpty(baseMeterSyncItems[i].BaseMeterEnergyId))
                                {
                                    dtInfo = new DataTable();
                                    sqlQuery = "SELECT Id FROM Energys WHERE UserId=" + companyId + " AND SyncId='" + baseMeterSyncItems[i].BaseMeterEnergyId + "'";
                                    cmd.CommandText = sqlQuery;
                                    adapter = new SqlDataAdapter(cmd);
                                    adapter.Fill(dtInfo);
                                    SystemLogHelper.Logger.Info("查询能源" + sqlQuery);

                                    if (dtInfo.Rows.Count == 0)
                                    {
                                        ProcessCurrentRec = false;
                                        string errorMessage = "Record with SyncId: " + baseMeterSyncItems[i].Id + " could not be processed because associated EnergyId in BaseMeter  item  does not exist in Energy list of the company";

                                        SystemLogHelper.Logger.Error(errorMessage);
                                    }
                                    else
                                    {
                                        ProcessCurrentRec = true;
                                        SystemEnergyId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
                                    }
                                }
                            }

                            if (ProcessCurrentRec == true)
                            {
                                //Check MeterModel key
                                if (!string.IsNullOrEmpty(baseMeterSyncItems[i].BaseMeterModelId))
                                {
                                    dtInfo = new DataTable();

                                    sqlQuery = "SELECT Id FROM MeterModels WHERE UserId=" + companyId + " AND SyncId=N'" + baseMeterSyncItems[i].BaseMeterModelId + "'";
                                    cmd.CommandText = sqlQuery;
                                    adapter = new SqlDataAdapter(cmd);
                                    adapter.Fill(dtInfo);
                                    SystemLogHelper.Logger.Info("查询仪表型号 " + sqlQuery);

                                    if (dtInfo.Rows.Count == 0)
                                    {
                                        ProcessCurrentRec = false;
                                        string errorMessage = "Record with SyncId: " + baseMeterSyncItems[i].Id + " could not be processed because associated MeterModelId in BaseMeter  item  does not exist in MeterModel list of the company";

                                        SystemLogHelper.Logger.Info(errorMessage);
                                    }
                                    else
                                    {
                                        ProcessCurrentRec = true;
                                        SystemMeterModelId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
                                    }
                                }
                            }

                            if (ProcessCurrentRec)
                            {
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@SyncId", baseMeterSyncItems[i].Id);
                                cmd.Parameters.AddWithValue("@Name", baseMeterSyncItems[i].Name ?? "");
                                cmd.Parameters.AddWithValue("@ParentSyncId", baseMeterSyncItems[i].BaseMeterParentAreaId);
                                cmd.Parameters.AddWithValue("@EnergySyncId", baseMeterSyncItems[i].BaseMeterEnergyId);

                                if (baseMeterSyncItems[i].BaseMeterModelId != null)
                                    cmd.Parameters.AddWithValue("@MeterModelSyncId", baseMeterSyncItems[i].BaseMeterModelId);
                                else
                                    cmd.Parameters.AddWithValue("@MeterModelSyncId", DBNull.Value);

                                cmd.Parameters.AddWithValue("@Manufacturer", baseMeterSyncItems[i].Manufacturer ?? "");
                                cmd.Parameters.AddWithValue("@InitialValue", baseMeterSyncItems[i].InitialValue);
                                cmd.Parameters.AddWithValue("@MeterNumber", baseMeterSyncItems[i].MeterNumber ?? "");
                                cmd.Parameters.AddWithValue("@Remark", baseMeterSyncItems[i].Remark ?? "");
                                cmd.Parameters.AddWithValue("@IsEnabled", baseMeterSyncItems[i].IsEnabled);
                                cmd.Parameters.AddWithValue("@IsRobotizationCollect", baseMeterSyncItems[i].IsRobotizationCollect);
                                cmd.Parameters.AddWithValue("@RecordDeleted", 0);
                                cmd.Parameters.AddWithValue("@IsVirtualMeter", baseMeterSyncItems[i].IsVirtualMeter);
                                cmd.Parameters.AddWithValue("@DisplayedOrder", baseMeterSyncItems[i].DisplayedOrder);
                                cmd.Parameters.AddWithValue("@UserId", companyId);
                                cmd.Parameters.AddWithValue("@BaseMeterType", baseMeterSyncItems[i].BaseMeterType);

                                cmd.Parameters.AddWithValue("@ParentAreaId", SystemParentAreaId);
                                cmd.Parameters.AddWithValue("@EnergyId", SystemEnergyId);
                                if (baseMeterSyncItems[i].BaseMeterModelId != null && SystemMeterModelId.GetValueOrDefault() > 0)
                                    cmd.Parameters.AddWithValue("@MeterModelId", SystemMeterModelId);
                                else
                                    cmd.Parameters.AddWithValue("@MeterModelId", DBNull.Value);

                                sqlQuery = "INSERT INTO BaseMeter(SyncId,Name,ParentSyncId,ParentAreaId,EnergySyncId,EnergyId,MeterModelSyncId,MeterModelId,Manufacturer,InitialValue,MeterNumber,Remark,IsEnabled,IsRobotizationCollect,RecordDeleted,IsVirtualMeter,DisplayedOrder,UserId,BaseMeterType) VALUES(@SyncId,@Name,@ParentSyncId,@ParentAreaId,@EnergySyncId,@EnergyId,@MeterModelSyncId,@MeterModelId,@Manufacturer,@InitialValue,@MeterNumber,@Remark,@IsEnabled,@IsRobotizationCollect,@RecordDeleted,@IsVirtualMeter,@DisplayedOrder,@UserId,@BaseMeterType)";

                                cmd.CommandText = sqlQuery;
                                cmd.ExecuteNonQuery();
                                responseCode = 1001;
                                SystemLogHelper.Logger.Info("插入仪表 " + sqlQuery);
                            }
                        }
                        else
                        { //update records

                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@SyncId", baseMeterSyncItems[i].Id);
                            cmd.Parameters.AddWithValue("@Name", baseMeterSyncItems[i].Name ?? "");
                            cmd.Parameters.AddWithValue("@Manufacturer", baseMeterSyncItems[i].Manufacturer ?? "");
                            cmd.Parameters.AddWithValue("@InitialValue", baseMeterSyncItems[i].InitialValue);
                            cmd.Parameters.AddWithValue("@MeterNumber", baseMeterSyncItems[i].MeterNumber ?? "");
                            cmd.Parameters.AddWithValue("@Remark", baseMeterSyncItems[i].Remark ?? "");
                            cmd.Parameters.AddWithValue("@IsEnabled", baseMeterSyncItems[i].IsEnabled);
                            cmd.Parameters.AddWithValue("@IsRobotizationCollect", baseMeterSyncItems[i].IsRobotizationCollect);
                            cmd.Parameters.AddWithValue("@RecordDeleted", 0);
                            cmd.Parameters.AddWithValue("@IsVirtualMeter", baseMeterSyncItems[i].IsVirtualMeter);
                            cmd.Parameters.AddWithValue("@DisplayedOrder", baseMeterSyncItems[i].DisplayedOrder);
                            cmd.Parameters.AddWithValue("@UserId", companyId);
                            cmd.Parameters.AddWithValue("@BaseMeterType", baseMeterSyncItems[i].BaseMeterType);

                            int Id = Convert.ToInt32(dtBaseMeter.Rows[0]["Id"]);

                            sqlQuery = "Update BaseMeter SET Name=@Name,Manufacturer=@Manufacturer,InitialValue=@InitialValue,MeterNumber=@MeterNumber,Remark=@Remark,IsEnabled=@IsEnabled,IsRobotizationCollect=@IsRobotizationCollect,RecordDeleted=@RecordDeleted,IsVirtualMeter=@IsVirtualMeter,DisplayedOrder=@DisplayedOrder,BaseMeterType=@BaseMeterType WHERE Id=" + Id;

                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();
                            responseCode = 1001;
                            SystemLogHelper.Logger.Info("更新仪表 " + sqlQuery);
                        }
                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return responseCode;
        }

        //BaseMeterAquisitionParameter Sequence
        public Dictionary<int, int> SyncBaseMeterAcquisitionsParameterItemsToCloudSystemInSequence(int companyId, List<BaseMeterAcuisitionParameter> baseMeterAcuisitionParameterSyncItems)
        {
            SetDBInfo();

            ConcurrentDictionary<int, int> BTFMAPPING = new ConcurrentDictionary<int, int>();
            #region 注释
            //    for (int i = 0; i < baseMeterAcuisitionParameterSyncItems.Count; i++)
            //    {

            //        Boolean ProcessCurrentRec = true;
            //        int SystemMeterId = 0;
            //        int SystemFieldTypeId = 0;

            //        sqlQuery = "Select bmft.Id FROM BaseMeterFieldType bmft INNER JOIN BaseMeter bm ON bmft.MeterId=bm.Id WHERE bm.UserId='" + companyId + "'  AND  bmft.SyncId='" + baseMeterAcuisitionParameterSyncItems[i].Id + "'";

            //        cmd.CommandType = CommandType.Text;
            //        cmd.CommandText = sqlQuery;
            //        dtBaseMeterAcquisitionsParameters = new DataTable();
            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(dtBaseMeterAcquisitionsParameters);

            //        if (dtBaseMeterAcquisitionsParameters.Rows.Count == 0)
            //        {
            //            DataTable dtInfo = new DataTable();
            //            //Get the associated EnergyId for metermodel
            //            if (!string.IsNullOrEmpty(baseMeterAcuisitionParameterSyncItems[i].BaseMeterId))
            //            {
            //                dtInfo = new DataTable();

            //                sqlQuery = "SELECT Id FROM BaseMeter WHERE UserId='" + companyId + "' AND SyncId='" + baseMeterAcuisitionParameterSyncItems[i].BaseMeterId + "'";
            //                cmd.CommandText = sqlQuery;

            //                adapter = new SqlDataAdapter(cmd);
            //                adapter.Fill(dtInfo);

            //                if (dtInfo.Rows.Count == 0 && Convert.ToInt32(dtInfo.Rows[0]["Id"]) > 0)
            //                {
            //                    ProcessCurrentRec = false;
            //                    string errorMessage = "Record with SyncId: " + baseMeterAcuisitionParameterSyncItems[i].Id + " could not be processed because associated MeterId in BaseMeterAcquisitionsParameter  item  does not exist in BaseMeter list of the company";

            //                    CreateDetailLogEnetryOnError(mainLogId, "BaseMeterAcquisitionsParameter", errorMessage);
            //                }
            //                else
            //                {
            //                    ProcessCurrentRec = true;
            //                    SystemMeterId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
            //                }
            //            }

            //            if (ProcessCurrentRec == true)
            //            {
            //                //Check Energy key
            //                if (!string.IsNullOrEmpty(baseMeterAcuisitionParameterSyncItems[i].AcuisitionParameterId))
            //                {
            //                    dtInfo = new DataTable();

            //                    sqlQuery = "SELECT Id FROM FieldTypes WHERE UserId='" + companyId + "' AND SyncId='" + baseMeterAcuisitionParameterSyncItems[i].AcuisitionParameterId + "'";
            //                    cmd.CommandText = sqlQuery;

            //                    adapter = new SqlDataAdapter(cmd);
            //                    adapter.Fill(dtInfo);

            //                    if (dtInfo.Rows.Count == 0)
            //                    {
            //                        ProcessCurrentRec = false;
            //                        string errorMessage = "Record with SyncId: " + baseMeterAcuisitionParameterSyncItems[i].Id + " could not be processed because associated AcuisitionParameterId(FieldTypeId) in BaseMeterAcquisitionsParameter  item  does not exist in AcuisitionParameter list of the company";

            //                        CreateDetailLogEnetryOnError(mainLogId, "BaseMeterAcquisitionsParameter", errorMessage);
            //                    }
            //                    else
            //                    {
            //                        ProcessCurrentRec = true;
            //                        SystemFieldTypeId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
            //                    }
            //                }
            //            }

            //            if (ProcessCurrentRec)
            //            {
            //                cmd.Parameters.Clear();
            //                cmd.Parameters.AddWithValue("@SyncId", baseMeterAcuisitionParameterSyncItems[i].Id);
            //                cmd.Parameters.AddWithValue("@Unit", baseMeterAcuisitionParameterSyncItems[i].Unit ?? "");
            //                cmd.Parameters.AddWithValue("@MeterSyncId", baseMeterAcuisitionParameterSyncItems[i].BaseMeterId);
            //                cmd.Parameters.AddWithValue("@FiledTypeSyncId", baseMeterAcuisitionParameterSyncItems[i].AcuisitionParameterId);
            //                cmd.Parameters.AddWithValue("@PNumber", baseMeterAcuisitionParameterSyncItems[i].PNumber ?? "");
            //                cmd.Parameters.AddWithValue("@Factor", baseMeterAcuisitionParameterSyncItems[i].Factor);
            //                cmd.Parameters.AddWithValue("@Interval", baseMeterAcuisitionParameterSyncItems[i].Interval);


            //                if (SystemMeterId != null)
            //                    cmd.Parameters.AddWithValue("@MeterId", SystemMeterId);
            //                else
            //                    cmd.Parameters.AddWithValue("@MeterId", DBNull.Value);
            //                if (SystemFieldTypeId != null)
            //                    cmd.Parameters.AddWithValue("@FieldTypeId", SystemFieldTypeId);
            //                else
            //                    cmd.Parameters.AddWithValue("@FieldTypeId", DBNull.Value);


            //                sqlQuery = "INSERT INTO BaseMeterFieldType(SyncId,Unit,MeterSyncId,MeterId,FiledTypeSyncId,FieldTypeId,PNumber,Factor,Interval) VALUES(@SyncId,@Unit,@MeterSyncId,@MeterId,@FiledTypeSyncId,@FieldTypeId,@PNumber,@Factor,@Interval);select SCOPE_IDENTITY()";
            //                cmd.CommandText = sqlQuery;
            //                int btf = cmd.ExecuteNonQuery();
            //                BTFMAP.Add(int.Parse(baseMeterAcuisitionParameterSyncItems[i].Id), btf);
            //            }

            //        }
            //        else
            //        {
            //            //update records
            //            cmd.Parameters.Clear();
            //            cmd.Parameters.AddWithValue("@SyncId", baseMeterAcuisitionParameterSyncItems[i].Id);
            //            cmd.Parameters.AddWithValue("@Unit", baseMeterAcuisitionParameterSyncItems[i].Unit ?? "");
            //            cmd.Parameters.AddWithValue("@MeterSyncId", baseMeterAcuisitionParameterSyncItems[i].BaseMeterId);
            //            cmd.Parameters.AddWithValue("@FiledTypeSyncId", baseMeterAcuisitionParameterSyncItems[i].AcuisitionParameterId);
            //            cmd.Parameters.AddWithValue("@PNumber", baseMeterAcuisitionParameterSyncItems[i].PNumber ?? "");
            //            cmd.Parameters.AddWithValue("@Factor", baseMeterAcuisitionParameterSyncItems[i].Factor);
            //            cmd.Parameters.AddWithValue("@Interval", baseMeterAcuisitionParameterSyncItems[i].Interval);

            //            int Id = Convert.ToInt32(dtBaseMeterAcquisitionsParameters.Rows[0]["Id"]);
            //            sqlQuery = "Update BaseMeterFieldType SET Unit=@Unit,PNumber=@PNumber,Factor=@Factor,Interval=@Interval WHERE Id=" + Id;
            //            cmd.CommandText = sqlQuery;
            //            cmd.ExecuteNonQuery();
            //            BTFMAP.Add(int.Parse(baseMeterAcuisitionParameterSyncItems[i].Id), Id);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    connection.Close();
            //    CreateDetailLogEnetryOnError(mainLogId, "BaseMeterAcquisitionsParameter", ex.Message);
            //    throw ex;
            //}
            //finally
            //{
            //    connection.Close();
            //} 
            #endregion

            Parallel.ForEach(baseMeterAcuisitionParameterSyncItems, item =>
            //foreach (var item in baseMeterAcuisitionParameterSyncItems)
            {
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    connection.Open();
                    try
                    {
                        string sqlQuery = string.Empty;
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        DataTable dtBaseMeterAcquisitionsParameters = new DataTable();
                        Boolean ProcessCurrentRec = true;
                        int SystemMeterId = 0;
                        int SystemFieldTypeId = 0;

                        sqlQuery = "Select bmft.Id FROM BaseMeterFieldType bmft INNER JOIN BaseMeter bm ON bmft.MeterId=bm.Id WHERE bm.UserId=" + companyId + " AND  bmft.SyncId='" + item.Id + "'";

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtBaseMeterAcquisitionsParameters);
                        SystemLogHelper.Logger.Info("查询仪表参数" + sqlQuery);

                        DataTable dtInfo = new DataTable();
                        //Get the associated MeterId
                        if (!string.IsNullOrEmpty(item.BaseMeterId))
                        {
                            dtInfo = new DataTable();
                            sqlQuery = "SELECT Id FROM BaseMeter WHERE UserId=" + companyId + " AND SyncId='" + item.BaseMeterId + "'";
                            cmd.CommandText = sqlQuery;
                            adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dtInfo);
                            SystemLogHelper.Logger.Info("查询仪表" + sqlQuery);

                            if (dtInfo.Rows.Count == 0 && Convert.ToInt32(dtInfo.Rows[0]["Id"]) > 0)
                            {
                                ProcessCurrentRec = false;
                                string errorMessage = "Record with SyncId: " + item.Id + " could not be processed because associated MeterId in BaseMeterAcquisitionsParameter  item  does not exist in BaseMeter list of the company";

                                SystemLogHelper.Logger.Error(errorMessage);
                            }
                            else
                            {
                                ProcessCurrentRec = true;
                                SystemMeterId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
                            }
                        }

                        if (ProcessCurrentRec == true)
                        {
                            //Check FieldType Id
                            if (!string.IsNullOrEmpty(item.AcuisitionParameterId))
                            {
                                dtInfo = new DataTable();
                                sqlQuery = "SELECT Id FROM FieldTypes WHERE UserId=" + companyId + " AND SyncId='" + item.AcuisitionParameterId + "'";
                                cmd.CommandText = sqlQuery;
                                adapter = new SqlDataAdapter(cmd);
                                adapter.Fill(dtInfo);
                                SystemLogHelper.Logger.Info("查询参数" + sqlQuery);

                                if (dtInfo.Rows.Count == 0)
                                {
                                    ProcessCurrentRec = false;
                                    string errorMessage = "Record with SyncId: " + item.Id + " could not be processed because associated AcuisitionParameterId(FieldTypeId) in BaseMeterAcquisitionsParameter  item  does not exist in AcuisitionParameter list of the company";

                                    SystemLogHelper.Logger.Error(errorMessage);
                                }
                                else
                                {
                                    ProcessCurrentRec = true;
                                    SystemFieldTypeId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
                                }
                            }
                        }

                        if (dtBaseMeterAcquisitionsParameters.Rows.Count == 0)
                        {
                            if (ProcessCurrentRec)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@SyncId", item.Id);
                                cmd.Parameters.AddWithValue("@Unit", item.Unit ?? "");
                                if (item.BaseMeterId == null)
                                    cmd.Parameters.AddWithValue("@MeterSyncId", DBNull.Value);
                                else
                                    cmd.Parameters.AddWithValue("@MeterSyncId", item.BaseMeterId);
                                if (item.AcuisitionParameterId == null)
                                    cmd.Parameters.AddWithValue("@FiledTypeSyncId", DBNull.Value);
                                else
                                    cmd.Parameters.AddWithValue("@FiledTypeSyncId", item.AcuisitionParameterId);
                                cmd.Parameters.AddWithValue("@PNumber", item.PNumber ?? "");
                                cmd.Parameters.AddWithValue("@Factor", item.Factor);
                                cmd.Parameters.AddWithValue("@Interval", item.Interval);
                                cmd.Parameters.AddWithValue("@MeterId", SystemMeterId);
                                cmd.Parameters.AddWithValue("@FieldTypeId", SystemFieldTypeId);

                                sqlQuery = "INSERT INTO BaseMeterFieldType(SyncId,Unit,MeterSyncId,MeterId,FiledTypeSyncId,FieldTypeId,PNumber,Factor,Interval) VALUES(@SyncId,@Unit,@MeterSyncId,@MeterId,@FiledTypeSyncId,@FieldTypeId,@PNumber,@Factor,@Interval);select SCOPE_IDENTITY()";
                                cmd.CommandText = sqlQuery;
                                int btf = int.Parse(cmd.ExecuteScalar().ToString());
                                SystemLogHelper.Logger.Info("更新仪表参数 " + sqlQuery);
                                BTFMAPPING.AddOrUpdate(int.Parse(item.Id), btf, (k, y) => btf);
                            }
                        }
                        else
                        {
                            //update records
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@SyncId", item.Id);
                            cmd.Parameters.AddWithValue("@Unit", item.Unit ?? "");
                            if (item.BaseMeterId == null)
                                cmd.Parameters.AddWithValue("@MeterSyncId", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@MeterSyncId", item.BaseMeterId);
                            if (item.AcuisitionParameterId == null)
                                cmd.Parameters.AddWithValue("@FiledTypeSyncId", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@FiledTypeSyncId", item.AcuisitionParameterId);
                            cmd.Parameters.AddWithValue("@PNumber", item.PNumber ?? "");
                            cmd.Parameters.AddWithValue("@Factor", item.Factor);
                            cmd.Parameters.AddWithValue("@Interval", item.Interval);
                            cmd.Parameters.AddWithValue("@FieldTypeId", SystemFieldTypeId);

                            int Id = Convert.ToInt32(dtBaseMeterAcquisitionsParameters.Rows[0]["Id"]);
                            sqlQuery = "Update BaseMeterFieldType SET SyncId =@SyncId, Unit=@Unit, MeterSyncId=@MeterSyncId, FiledTypeSyncId=@FiledTypeSyncId, PNumber=@PNumber, Factor=@Factor, Interval=@Interval, FieldTypeId =@FieldTypeId WHERE Id=" + Id;
                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();
                            // BTFMAPPING.AddOrUpdate(int.Parse(item.Id), Id);
                            SystemLogHelper.Logger.Info("更新仪表参数 " + sqlQuery);
                            BTFMAPPING.AddOrUpdate(int.Parse(item.Id), Id, (k, v) => Id);
                        }

                        SystemLogHelper.Logger.Info("完成一个Id迭代 " + item.Id);
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            });

            return BTFMAPPING.ToDictionary(a => a.Key, a => a.Value);
        }

        //VirtualMeter AquisitionParamter
        public Dictionary<int, int> SyncVirtualMeterParameterInfoItemsToCloudSystemInSequence(int companyId, List<VirtualMeterParameterInfo> virtualMeterParameterInfoItems)
        {
            SetDBInfo();
            ConcurrentDictionary<int, int> VBMFTMAP = new ConcurrentDictionary<int, int>();
            Parallel.ForEach(virtualMeterParameterInfoItems, item =>
            {
                using (var connection = new SqlConnection(_DBConnStr))
                {
                    connection.Open();
                    try
                    {
                        string sqlQuery = string.Empty;
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        DataTable dtBaseMeterAcquisitionsParameters = new DataTable();
                        Boolean ProcessCurrentRec = true;
                        int SystemMeterId = 0;
                        int SystemFieldTypeId = 0;

                        sqlQuery = "Select bmft.Id FROM BaseMeterFieldType bmft INNER JOIN BaseMeter bm ON bmft.MeterId=bm.Id WHERE bm.UserId=" + companyId + " AND bmft.SyncId='" + item.Id + "'";

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlQuery;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtBaseMeterAcquisitionsParameters);
                        SystemLogHelper.Logger.Info("查询仪表参数" + sqlQuery);

                        if (dtBaseMeterAcquisitionsParameters.Rows.Count == 0)
                        {
                            DataTable dtInfo = new DataTable();
                            //Get the associated EnergyId for metermodel
                            if (!string.IsNullOrEmpty(item.MeterSyncId))
                            {
                                dtInfo = new DataTable();
                                sqlQuery = "SELECT Id FROM BaseMeter WHERE UserId=" + companyId + " AND SyncId='" + item.MeterSyncId + "'";
                                cmd.CommandText = sqlQuery;
                                adapter = new SqlDataAdapter(cmd);
                                adapter.Fill(dtInfo);
                                SystemLogHelper.Logger.Info("查询仪表" + sqlQuery);

                                if (dtInfo.Rows.Count == 0 && Convert.ToInt32(dtInfo.Rows[0]["Id"]) > 0)
                                {
                                    ProcessCurrentRec = false;
                                    string errorMessage = "Record with SyncId: " + item.Id + " could not be processed because associated MeterId in BaseMeterAcquisitionsParameter  item  does not exist in BaseMeter list of the company";

                                    SystemLogHelper.Logger.Error(errorMessage);
                                }
                                else
                                {
                                    ProcessCurrentRec = true;
                                    SystemMeterId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
                                }
                            }

                            if (ProcessCurrentRec == true)
                            {
                                //Check Energy key
                                if (!string.IsNullOrEmpty(item.FieldTypeSyncId))
                                {
                                    dtInfo = new DataTable();
                                    sqlQuery = "SELECT Id FROM FieldTypes WHERE UserId=" + companyId + " AND SyncId='" + item.FieldTypeSyncId + "'";
                                    cmd.CommandText = sqlQuery;
                                    adapter = new SqlDataAdapter(cmd);
                                    adapter.Fill(dtInfo);
                                    SystemLogHelper.Logger.Info("查询参数" + sqlQuery);

                                    if (dtInfo.Rows.Count == 0)
                                    {
                                        ProcessCurrentRec = false;
                                        string errorMessage = "Record with SyncId: " + item.Id + " could not be processed because associated AcuisitionParameterId(FieldTypeId) in BaseMeterAcquisitionsParameter  item  does not exist in AcuisitionParameter list of the company";

                                        SystemLogHelper.Logger.Error(errorMessage);
                                    }
                                    else
                                    {
                                        ProcessCurrentRec = true;
                                        SystemFieldTypeId = Convert.ToInt32(dtInfo.Rows[0]["Id"]);
                                    }
                                }
                            }

                            if (ProcessCurrentRec)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@SyncId", item.Id);
                                cmd.Parameters.AddWithValue("@Unit", item.Unit ?? "");
                                if (item.MeterSyncId == null)
                                    cmd.Parameters.AddWithValue("@MeterSyncId", DBNull.Value);
                                else
                                    cmd.Parameters.AddWithValue("@MeterSyncId", item.MeterSyncId);
                                if (item.FieldTypeSyncId == null)
                                    cmd.Parameters.AddWithValue("@FiledTypeSyncId", DBNull.Value);
                                else
                                    cmd.Parameters.AddWithValue("@FiledTypeSyncId", item.FieldTypeSyncId);
                                cmd.Parameters.AddWithValue("@PNumber", item.PNumber ?? "");
                                cmd.Parameters.AddWithValue("@Factor", item.Factor);
                                cmd.Parameters.AddWithValue("@Interval", item.Interval);
                                cmd.Parameters.AddWithValue("@MeterId", SystemMeterId);
                                cmd.Parameters.AddWithValue("@FieldTypeId", SystemFieldTypeId);

                                sqlQuery = "INSERT INTO BaseMeterFieldType(SyncId,Unit,MeterSyncId,MeterId,FiledTypeSyncId,FieldTypeId,PNumber,Factor,Interval) VALUES(@SyncId,@Unit,@MeterSyncId,@MeterId,@FiledTypeSyncId,@FieldTypeId,@PNumber,@Factor,@Interval);select SCOPE_IDENTITY()";
                                cmd.CommandText = sqlQuery;
                                int btf = int.Parse(cmd.ExecuteScalar().ToString());
                                SystemLogHelper.Logger.Info("更新仪表参数 " + sqlQuery);
                                //更新VirtualMeterParameterInfo表
                                cmd.Parameters.Clear();
                                sqlQuery = string.Format(@"IF EXISTS(SELECT 1 FROM VirtualMeterParameterInfo WHERE id ={0})
                                                                              UPDATE VirtualMeterParameterInfo SET Name = '{1}', CalculateFormula = '{2}', MeterDataType = {3}, RecordDeleted = 0, ShowCalculateFormula = '{4}', AssociatedParameterIds = '{5}'
                                                                              WHERE id ={0}
                                                                              ELSE
  	                                                                            INSERT INTO VirtualMeterParameterInfo(Id, Name, CalculateFormula, MeterDataType, RecordDeleted, ShowCalculateFormula, AssociatedParameterIds ) VALUES({0},'{1}','{2}',{3},0,'{4}','{5}')", btf, item.Name, item.CalculateFormula, item.MeterDataType, item.ShowCalculateFormula, item.AssociatedParameterIds);
                                cmd.CommandText = sqlQuery;
                                cmd.ExecuteNonQuery();

                                SystemLogHelper.Logger.Info("更新仪表虚拟参数参数 " + sqlQuery);
                                VBMFTMAP.AddOrUpdate(item.Id, btf, (k, y) => btf);
                            }
                        }
                        else
                        {
                            //update records
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@SyncId", item.Id);
                            cmd.Parameters.AddWithValue("@Unit", item.Unit ?? "");
                            if (item.MeterSyncId == null)
                                cmd.Parameters.AddWithValue("@MeterSyncId", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@MeterSyncId", item.MeterSyncId);
                            if (item.FieldTypeSyncId == null)
                                cmd.Parameters.AddWithValue("@FiledTypeSyncId", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@FiledTypeSyncId", item.FieldTypeSyncId);
                            cmd.Parameters.AddWithValue("@PNumber", item.PNumber ?? "");
                            cmd.Parameters.AddWithValue("@Factor", item.Factor);
                            cmd.Parameters.AddWithValue("@Interval", item.Interval);

                            int Id = Convert.ToInt32(dtBaseMeterAcquisitionsParameters.Rows[0]["Id"]);
                            sqlQuery = "Update BaseMeterFieldType SET Unit=@Unit,PNumber=@PNumber,Factor=@Factor,Interval=@Interval WHERE Id=" + Id;
                            cmd.CommandText = sqlQuery;
                            SystemLogHelper.Logger.Info("更新仪表参数 " + sqlQuery);
                            cmd.ExecuteNonQuery();
                            // BTFMAPPING.AddOrUpdate(int.Parse(item.Id), Id);
                            //更新VirtualMeterParameterInfo表
                            sqlQuery = string.Format(@"IF EXISTS(SELECT 1 FROM VirtualMeterParameterInfo WHERE id ={0})
                                                                              UPDATE VirtualMeterParameterInfo SET Name = '{1}', CalculateFormula = '{2}', MeterDataType = {3}, RecordDeleted = 0, ShowCalculateFormula = '{4}', AssociatedParameterIds = '{5}'
                                                                              WHERE id ={0}
                                                                              ELSE
  	                                                                            INSERT INTO VirtualMeterParameterInfo(Id, Name, CalculateFormula, MeterDataType, RecordDeleted, ShowCalculateFormula, AssociatedParameterIds ) VALUES({0},'{1}','{2}',{3},0,'{4}','{5}')", Id, item.Name, item.CalculateFormula, item.MeterDataType, item.ShowCalculateFormula, item.AssociatedParameterIds);
                            cmd.CommandText = sqlQuery;
                            cmd.ExecuteNonQuery();

                            SystemLogHelper.Logger.Info("更新仪表虚拟参数参数 " + sqlQuery);
                            VBMFTMAP.AddOrUpdate(item.Id, Id, (k, v) => Id);
                        }

                        SystemLogHelper.Logger.Info("完成一个Id迭代 " + item.Id);
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            });

            //上传更新MeterModelFieldType表
            UpdateMeterModelFieldType(companyId);

            return VBMFTMAP.ToDictionary(a => a.Key, a => a.Value);
        }

        private void UpdateMeterModelFieldType(int companyId)
        {
            SetDBInfo();
            string sqlQuery = @"delete from meterModelFieldTypes where metermodelId in(select id from metermodels where userid =" + companyId + ");select f.id as MeterModelId, m.id as FieldTypeId from basemeter a inner join MeterModels f on a.MeterModelId = f.id inner join Basemeterfieldtype bf on bf.meterid = a.id inner join Fieldtypes m on bf.fieldtypeid = m.id where a.userid = " + companyId;

            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                SqlTransaction transaction = null;
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    transaction = connection.BeginTransaction();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, transaction))
                    {
                        bulkCopy.DestinationTableName = "MeterModelFieldTypes";
                        bulkCopy.ColumnMappings.Add("MeterModelId", "MeterModelId");
                        bulkCopy.ColumnMappings.Add("FieldTypeId", "FieldTypeId");

                        bulkCopy.WriteToServer(dataTable);
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.InnerException ?? ex, "上传仪表型号参数失败，" + ex.Message);
                }
            }
        }

        #endregion

        #region Sync History Record

        public bool SyncUpdateTime<T>(List<T> recordList, string tableName, int userId, string[] bmfIds) where T : BaseRecord
        {
            var record = recordList.OrderBy(a => a.HTime);
            string sqlQuery = "";
            SqlCommand cmd = new SqlCommand();
            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                try
                {
                    foreach (var bmfId in bmfIds)
                    {
                        var maxHime = recordList.Where(a => a.BaseMeterAcquisitionParameterId == bmfId).Max(a => a.HTime);
                        var maxDateTime = maxHime.ToString("yyyy-MM-dd HH:mm:00");
                        sqlQuery += string.Format(@"IF EXISTS(SELECT 1 FROM CompanyTableSyncHistory ctsh WHERE ctsh.UserId ={0} AND ctsh.TableName='{1}' AND ctsh.BmfId={2} AND ctsh.SyncedUpToDateTime<='{3}')
                          UPDATE CompanyTableSyncHistory SET SyncedUpToDateTime = '{3}' WHERE UserId = {0} AND TableName='{1}' AND BmfId={2}
                          ELSE
  	                        INSERT INTO CompanyTableSyncHistory(UserId, TableName, SyncRecordCount, SyncedUpToDateTime, UpdatedOn, BmfId ) VALUES ({0}, '{1}', 0, '{3}', GETDATE(), {2})", userId, tableName, bmfId, maxDateTime);
                    }

                    cmd.CommandText = sqlQuery;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                    cmd.Dispose();
                    return false;
                }
            }

            cmd.Dispose();
            return true;
        }

        public int SyncBaseRecord<T>(int userId, List<T> minutesRecordSyncItems, string tableName) where T : BaseRecord
        {
            SetDBInfo();
            int responseCode = 0;

            using (var connection = new SqlConnection(_DBConnStr))
            {
                connection.Open();
                try
                {
                    //string sqlQuery = "";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    string[] bmfIdsArray = minutesRecordSyncItems.Select(a => a.BaseMeterAcquisitionParameterId.ToString()).Distinct().ToArray();
                    DataTable minuteTable = Common.ToBaseRecordDataTable(minutesRecordSyncItems);
                    SqlTransaction transaction = connection.BeginTransaction();
                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
                    {
                        sqlBulkCopy.DestinationTableName = tableName;
                        sqlBulkCopy.ColumnMappings.Add("Id", "SyncId");
                        sqlBulkCopy.ColumnMappings.Add("TotalData", "TotalData");
                        sqlBulkCopy.ColumnMappings.Add("SameCompareTotalData", "SameCompareTotalData");
                        sqlBulkCopy.ColumnMappings.Add("LinkCompareTotalData", "LinkCompareTotalData");
                        sqlBulkCopy.ColumnMappings.Add("TotalMoney", "TotalMoney");
                        sqlBulkCopy.ColumnMappings.Add("SameCompareTotalMoney", "SameCompareTotalMoney");
                        sqlBulkCopy.ColumnMappings.Add("LinkCompareTotalMoney", "LinkCompareTotalMoney");
                        sqlBulkCopy.ColumnMappings.Add("HTime", "HTime");
                        sqlBulkCopy.ColumnMappings.Add("BaseMeterFieldTypeId", "BaseMeterFieldTypeId");

                        sqlBulkCopy.WriteToServer(minuteTable);

                        transaction.Commit();
                    }

                    //更改最后更新时间
                    //var dic = minutesRecordSyncItems.OrderBy(a => a.HTime).ToDictionary(a => a.BaseMeterAcquisitionParameterId, val => val.HTime);
                    SyncUpdateTime(minutesRecordSyncItems, tableName, userId, bmfIdsArray);
                    responseCode = 1001;
                    return responseCode;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int SyncMinutesRecordItemsToCloudSystemInSequence(int userId, List<MinutesRecord> minutesRecordSyncItems)
        {
            return SyncBaseRecord<MinutesRecord>(userId, minutesRecordSyncItems, "MinutesRecord");
        }

        public int SyncHourRecordItemsToCloudSystemInSequence(int userId, List<HourRecord> hourRecordSyncItems)
        {
            return SyncBaseRecord<HourRecord>(userId, hourRecordSyncItems, "HourRecord");
        }

        public int SyncDayRecordItemsToCloudSystemInSequence(int userId, List<DayRecord> dayRecordSyncItems)
        {
            return SyncBaseRecord<DayRecord>(userId, dayRecordSyncItems, "DayRecord");
        }

        public int SyncMonthRecordItemsToCloudSystemInSequence(int userId, List<MonthRecord> monthRecordSyncItems)
        {
            return SyncBaseRecord<MonthRecord>(userId, monthRecordSyncItems, "MonthRecord");
        }

        public int SyncYearRecordItemsToCloudSystemInSequence(int userId, List<YearRecord> yearRecordSyncItems)
        {
            return SyncBaseRecord<YearRecord>(userId, yearRecordSyncItems, "YearRecord");
        }

        #endregion

        #region Private Methods

        private int UpdateAreaRegionAssociation(int userId, SqlConnection conn)
        {
            SqlCommand cmd = null;
            int updatedRowCount = 0;
            try
            {
                cmd = new SqlCommand();


                cmd.Connection = conn;

                cmd.CommandText = "SSP_Sync_UpdateAreaAssociation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                updatedRowCount = cmd.ExecuteNonQuery();

                cmd.Dispose();
                return updatedRowCount;
            }
            catch (Exception ex)
            {
                cmd = null;
                throw ex;
            }


        }

        #endregion
    }
}