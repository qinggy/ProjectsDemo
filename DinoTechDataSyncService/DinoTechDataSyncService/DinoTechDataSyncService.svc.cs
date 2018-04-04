using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DinoTechDataSyncService.Domain;
using DinoTechDataSyncService.Business;
using DinoTechDataSyncService.WCF.Utilities;
using System.Xml.Serialization;
using System.IO;
using System.Web;
using DinoTechDataSyncService.WCF.FileUtilities;
using DinoTechDataSyncService.Utility;

namespace DinoTechDataSyncService.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [XmlSerializerFormat]
    public class DinoTechDataSyncService : IDinoTechDataSyncService
    {
        public int CompanyId { get; set; }

        public ResponseUserTokenInfo ValidateUser(string userId, string password, string timeZoneOffSet)
        {
            ResponseUserTokenInfo responseUserInfo = new ResponseUserTokenInfo();
            string token = String.Empty;
            UserBusiness userBusiness = new UserBusiness();
            //String timeZoneOffSet = GetTimeZoneOffSet();

            Domain.User userObj = userBusiness.ValidateUser(userId, password, timeZoneOffSet);
            if (userObj != null)
            {
                if (string.IsNullOrEmpty(userObj.Token))
                {
                    token = CommonFunctions.GenerateToken() + "_" + userObj.UserId.Encrypt();

                    //Code for Saving Token to Database
                    userBusiness.UpdateUserToken(userObj.UserId, token);
                }
                else
                {
                    token = userObj.Token;
                }

                // responseUserInfo.CompanyId = userObj.UserId.Encrypt();
                responseUserInfo.CustomerId = userObj.UserId;
                responseUserInfo.Token = token;
                responseUserInfo.Status = Enums.TokenValidateStatus.Success.ToString();
                responseUserInfo.Message = "User authenticated successfully";
                return responseUserInfo;
            }

            else
            {
                // responseUserInfo.CompanyId = "";
                responseUserInfo.Token = "";
                responseUserInfo.Status = Enums.TokenValidateStatus.Failure.ToString();
                responseUserInfo.Message = "Incorrect username or password!";
                return responseUserInfo;

            }

        }

        private int GetCompanyId()
        {
            try
            {
                int companyId = 0;
                string token = Convert.ToString(OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("TokenHeader", "TokenNameSpace")).Trim();
                string strEncryptedCompanyId = token.Substring(token.IndexOf('_') + 1);
                companyId = strEncryptedCompanyId.Decrypt();

                return companyId;
            }
            catch (Exception ex)
            { throw ex; }
        }

        private int GetMainLogId()
        {

            try
            {

                int logId = 0;

                string strEncryptedLogId = Convert.ToString(OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("LogHeader", "LogNameSpace")).Trim();


                logId = strEncryptedLogId.Decrypt();

                return logId;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public ResponseStatus SubmitEnergies(List<Energy> energyItems)
        {
            try
            {
                CompanyId = GetCompanyId();

                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncAllEnergyItems(CompanyId, energyItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;

                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info("SubmitEnergies Code " + description);
                else
                    SystemLogHelper.Logger.Error("SubmitEnergies - Else: " + description);

                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitMeterModels(List<MeterModel> meterModelItems)
        {
            try
            {
                CompanyId = GetCompanyId();

                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncAllMeterModelItems(CompanyId, meterModelItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;

                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info(description);
                else
                    SystemLogHelper.Logger.Error(description);

                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitAquisitionParameters(List<AcquisitionParameters> acquisitionParametersItems)
        {
            try
            {
                CompanyId = GetCompanyId();

                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncAllAcquisitionParametersItems(CompanyId, acquisitionParametersItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;
                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info(description);
                else
                    SystemLogHelper.Logger.Error(description);

                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitRegionalAreas(List<Areas> areaItems)
        {
            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncAllAreaItems(CompanyId, areaItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;

                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info(description);
                else
                    SystemLogHelper.Logger.Error(description);


                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw new FaultException<ServiceError>(objServiceError);
            }

        }

        public ResponseStatus SubmitMeterModelFieldTypes(List<MeterModelFieldTypes> meterModelFieldTypesItems)
        {
            try
            {
                CompanyId = GetCompanyId();

                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncAllMeterModelFieldTypes(CompanyId, meterModelFieldTypesItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;

                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info(description);
                else
                    SystemLogHelper.Logger.Error(description);

                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitBaseMeters(List<BaseMeter> baseMeterItems)
        {
            try
            {
                CompanyId = GetCompanyId();

                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncAllBaseMeterItems(CompanyId, baseMeterItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;
                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info(description);
                else
                    SystemLogHelper.Logger.Error(description);

                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }

        }

        public Dictionary<int, int> SubmitBaseMeterAcquisitionParameters(List<BaseMeterAcuisitionParameter> baseMeteAcuisitionParameterrItems)
        {
            try
            {
                CompanyId = GetCompanyId();

                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                var objResponse = submitTableSyncBusiness.ProcessSyncAllBaseMeterAcquisitionsParameterItems(CompanyId, baseMeteAcuisitionParameterrItems);

                if (objResponse == null)
                    SystemLogHelper.Logger.Error("仪表参数更新值失败，没有返回本地参数和平台参数对照表");
                else
                    SystemLogHelper.Logger.Error("仪表参数更新成功 " + objResponse.Count + "条记录");

                return objResponse;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public Dictionary<int, int> SubmitVirtualMeterAcquisitionParameters(List<VirtualMeterParameterInfo> virtualMeteAcuisitionParameterItems)
        {
            try
            {
                CompanyId = GetCompanyId();

                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                var objResponse = submitTableSyncBusiness.ProcessSyncAllVirtualMeterParameterInfoItems(CompanyId, virtualMeteAcuisitionParameterItems);

                if (objResponse == null)
                    SystemLogHelper.Logger.Error("虚拟仪表参数更新值失败，没有返回本地参数和平台参数对照表");
                else
                    SystemLogHelper.Logger.Error("虚拟仪表参数更新成功 " + objResponse.Count + "条记录");

                return objResponse;
            }
            catch (Exception ex)
            {
                ServiceError objService = new ServiceError();
                objService.Status = Enums.SyncStatus.Failure.ToString();
                objService.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw new FaultException<ServiceError>(objService);
            }
        }

        //Last Sync Rec Count
        public int GetMinutesRecordSyncCount()
        {

            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                int tblSyncRecCount = submitTableSyncBusiness.GetCompanyTableRecCountInfo(CompanyId, "MinutesRecord");
                return tblSyncRecCount;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public int GetHourRecordSyncCount()
        {
            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                int tblSyncRecCount = submitTableSyncBusiness.GetCompanyTableRecCountInfo(CompanyId, "HourRecord");
                return tblSyncRecCount;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public int GetDayRecordSyncCount()
        {
            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                int tblSyncRecCount = submitTableSyncBusiness.GetCompanyTableRecCountInfo(CompanyId, "DayRecord");
                return tblSyncRecCount;

            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public int GetMonthRecordSyncCount()
        {
            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                int tblSyncRecCount = submitTableSyncBusiness.GetCompanyTableRecCountInfo(CompanyId, "MonthRecord");
                return tblSyncRecCount;

            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public int GetYearRecordSyncCount()
        {

            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                int tblSyncRecCount = submitTableSyncBusiness.GetCompanyTableRecCountInfo(CompanyId, "YearRecord");
                return tblSyncRecCount;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        //Last Sync Date
        public string GetMinutesRecordsLastSyncDateTime(int bmftId)
        {

            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                string lastSyncDateTime = submitTableSyncBusiness.GetCompanyTableLastSyncDateTimeInfo(CompanyId, "MinutesRecord", bmftId);
                return lastSyncDateTime;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        //dec 11
        public string GetInstantaneousDataLastSyncDateTime(string bmfId, int dType)
        {
            try
            {
                DataServiceHelper helper = new DataServiceHelper();
                var lastDateTime = helper.GetLastUpdateTime(bmfId, dType);
                return lastDateTime;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public string GetHourRecordLastSyncDateTime(int bmftId)
        {
            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                string lastSyncDateTime = submitTableSyncBusiness.GetCompanyTableLastSyncDateTimeInfo(CompanyId, "HourRecord", bmftId);
                return lastSyncDateTime;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public string GetDayRecordsLastSyncDateTime(int bmftId)
        {

            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                string lastSyncDateTime = submitTableSyncBusiness.GetCompanyTableLastSyncDateTimeInfo(CompanyId, "DayRecord", bmftId);
                return lastSyncDateTime;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public string GetMonthRecordLastSyncDateTime(int bmftId)
        {

            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                string lastSyncDateTime = submitTableSyncBusiness.GetCompanyTableLastSyncDateTimeInfo(CompanyId, "MonthRecord", bmftId);
                return lastSyncDateTime;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public string GetYearRecordLastSyncDateTime(int bmftId)
        {
            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                string lastSyncDateTime = submitTableSyncBusiness.GetCompanyTableLastSyncDateTimeInfo(CompanyId, "YearRecord", bmftId);
                return lastSyncDateTime;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitMinutesRecords(List<MinutesRecord> minutesRecordItems)
        {
            DateTime start = DateTime.Now;
            CompanyId = GetCompanyId();
            try
            {
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncMinutesRecords(CompanyId, minutesRecordItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;
                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info("SubmitMinutesRecords-Code" + description);
                else
                    SystemLogHelper.Logger.Error("SubmitMinutesRecords-Error" + description);

                DateTime end = DateTime.Now;
                SystemLogHelper.Logger.Info("Minute Speed Time " + (end - start).Milliseconds + " ms");
                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitHourRecords(List<HourRecord> hourRecordItems)
        {
            DateTime start = DateTime.Now;
            CompanyId = GetCompanyId();
            try
            {
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncHourRecords(CompanyId, hourRecordItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;
                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info("SubmitHourRecords-Code" + description);
                else
                    SystemLogHelper.Logger.Error("SubmitHourRecords-Else" + description);

                DateTime end = DateTime.Now;
                SystemLogHelper.Logger.Info("Hour Speed Time " + (end - start).Milliseconds + " ms");
                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitDayRecords(List<DayRecord> dayRecordItems)
        {
            DateTime start = DateTime.Now;
            CompanyId = GetCompanyId();
            try
            {
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncDayRecords(CompanyId, dayRecordItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;
                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info("SubmitDayRecords-Code" + description);
                else
                    SystemLogHelper.Logger.Error("SubmitDayRecords-Else" + description);

                DateTime end = DateTime.Now;
                SystemLogHelper.Logger.Info("Day Speed Time " + (end - start).Milliseconds + " ms");
                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitMonthRecords(List<MonthRecord> monthRecordItems)
        {
            DateTime start = DateTime.Now;
            CompanyId = GetCompanyId();
            try
            {
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncMonthRecords(CompanyId, monthRecordItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;
                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info("SubmitMonthRecords-Code" + description);
                else
                    SystemLogHelper.Logger.Error("SubmitMonthRecords-Else" + description);

                DateTime end = DateTime.Now;
                SystemLogHelper.Logger.Info("Month Speed Time " + (end - start).Milliseconds + " ms");
                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SubmitYearRecords(List<YearRecord> yearRecordItems)
        {
            DateTime start = DateTime.Now;
            CompanyId = GetCompanyId();
            try
            {
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                ResponseStatus objResponseStatus = submitTableSyncBusiness.ProcessSyncYearRecords(CompanyId, yearRecordItems);

                string description = "Message Code: " + objResponseStatus.Code + " , Message Detail:" + objResponseStatus.Message;
                if (objResponseStatus.Code == "1001")
                    SystemLogHelper.Logger.Info("SubmitYearRecords-Code" + description);
                else
                    SystemLogHelper.Logger.Error("SubmitYearRecords-Else" + description);

                DateTime end = DateTime.Now;
                SystemLogHelper.Logger.Info("Year Speed Time " + (end - start).Milliseconds + " ms");
                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, objServiceError.ErrorMessage);
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public List<EnergyPowerMeterDataTree> GetListOfAllAreaMeterAndParametersOnCloud()
        {
            CompanyId = GetCompanyId();

            try
            {

                List<EnergyPowerMeterDataTree> listEnergyPowerMeterDataTree = new List<EnergyPowerMeterDataTree>();
                DataSyncInfoBusiness dataSyncInfoBusiness = new DataSyncInfoBusiness();


                listEnergyPowerMeterDataTree = dataSyncInfoBusiness.GetListOfAllAreaMeterAndParametersOnCloud(CompanyId);


                return listEnergyPowerMeterDataTree;

            }
            catch (Exception ex)
            {

                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus SyncEnergyMeterDataToCloud(List<EnergyMeterHistoryDataRecord> energyMeterHistoryDataRecord)
        {
            CompanyId = GetCompanyId();

            try
            {


                DataSyncInfoBusiness dataSyncInfoBusiness = new DataSyncInfoBusiness();
                ResponseStatus objResponseStatus = new ResponseStatus();
                foreach (var energyMeter in energyMeterHistoryDataRecord)
                {
                    objResponseStatus = dataSyncInfoBusiness.SyncEnergyMeterDataToCloud(energyMeter);
                    if (objResponseStatus.Status == "1025")
                        break;
                }

                return objResponseStatus;

            }
            catch (Exception ex)
            {

                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }

        public ResponseStatus UpdateSyncLogInfo()
        {
            try
            {
                CompanyId = GetCompanyId();
                SubmitTableSyncBusiness submitTableSyncBusiness = new SubmitTableSyncBusiness();
                submitTableSyncBusiness.UpdateFinalUpdationOnSyncTables(CompanyId);

                return new ResponseStatus();
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = Enums.SyncStatus.Failure.ToString();
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }


        }

        public ResponseStatus AddRealTimeBTMeterData(RequestBTMData requestBTMeterData)
        {
            try
            {
                ResponseStatus objResponseStatus = new ResponseStatus();
                bool response = false;

                List<InstantaneousDatas> instantaneousDatas = new List<InstantaneousDatas>();
                List<AddUpData> addUpDatas = new List<AddUpData>();
                int UserId = requestBTMeterData.UserId;
                if (requestBTMeterData.Data != null)
                {
                    foreach (var item in requestBTMeterData.Data)
                    {
                        if (item.DT == 0)
                        {
                            AddUpData data = new AddUpData();
                            data.CollectionTime = DateTime.Parse(item.T);
                            data.MeterFieldTypeId = item.BF;
                            data.MeterValue = item.D;
                            data.UserId = UserId;

                            addUpDatas.Add(data);
                        }
                        else
                        {
                            InstantaneousDatas idata = new InstantaneousDatas();
                            idata.CollectionTime = DateTime.Parse(item.T);
                            idata.MeterFieldTypeId = item.BF;
                            idata.MeterValue = item.D;
                            idata.UserId = UserId;

                            instantaneousDatas.Add(idata);
                        }
                    }
                }

                DataServiceHelper helper = new DataServiceHelper();
                if (instantaneousDatas.Count > 0)
                {
                    response = helper.UploadData(instantaneousDatas);
                }
                if (addUpDatas.Count > 0)
                {
                    response = helper.UploadAddUpData(addUpDatas);
                }

                if (response == true)
                {
                    objResponseStatus.Status = Enums.SyncStatus.Success.ToString();
                    objResponseStatus.Message = "Success";
                }
                else
                {
                    objResponseStatus.Status = Enums.SyncStatus.Failure.ToString();
                    objResponseStatus.Message = "Failure";
                }

                return objResponseStatus;
            }
            catch (Exception ex)
            {
                ServiceError objServiceError = new ServiceError();
                objServiceError.Status = "Failure";
                objServiceError.ErrorMessage = ex.Message;
                throw new FaultException<ServiceError>(objServiceError);
            }
        }
    }
}
