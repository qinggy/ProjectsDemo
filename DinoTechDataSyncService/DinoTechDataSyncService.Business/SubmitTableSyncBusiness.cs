using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinoTechDataSyncService.Repository;
using DinoTechDataSyncService.Domain;
using DinoTechDataSyncService.Repository.Utilities;

using System.Data;
using DinoTechDataSyncService.Utility;

namespace DinoTechDataSyncService.Business
{
    public class SubmitTableSyncBusiness
    {

        public DinoTechDataSyncServiceHandler serviceHandler = new DinoTechDataSyncServiceHandler();
        public ResponseMessageList serviceResponseMessageList = new ResponseMessageList();

        public SubmitTableSyncBusiness()
        {
        }

        public int GetCompanyTableRecCountInfo(int companyId, string tableName)
        {

            return serviceHandler.GetCompanyTableRecCountInfo(companyId, tableName);

        }

        public string GetCompanyTableLastSyncDateTimeInfo(int companyId, string tableName, int bmftId)
        {
            return serviceHandler.GetCompanyTableLastSyncDateTimeInfo(companyId, tableName, bmftId);
        }

        public bool UpdateCompanyTableRecCountInfo(int companyId, string tableName, int tableSyncRecCount)
        {
            return serviceHandler.UpdateCompanyTableRecCountInfo(companyId, tableName, tableSyncRecCount);
        }

        public bool UpdateCompanyTableLastSyncDateTimeInfo(int companyId, string tableName, string currentTableLastSyncDateTime)
        {

            return serviceHandler.UpdateCompanyTableLastSyncDateTimeInfo(companyId, tableName, currentTableLastSyncDateTime);

        }

        public bool UpdateFinalUpdationOnSyncTables(int companyId)
        {

            return serviceHandler.UpdateFinalUpdationOnSyncTables(companyId);
        }

        /// <summary>
        /// Method for Energy Table Sync 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="energySyncItems"></param>
        /// <returns></returns>        
        public ResponseStatus ProcessSyncAllEnergyItems(int companyId, List<Energy> energySyncItems)
        {
            try
            {
                int response = serviceHandler.SyncEnergyItemsToCloudSystemInSequence(companyId, energySyncItems);
                if (response == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (response == 1022)
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1022").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                }
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw ex;
            }
        }

        public ResponseStatus ProcessSyncAllAcquisitionParametersItems(int companyId, List<AcquisitionParameters> AcquisitionParametersSyncItems)
        {
            try
            {
                int response = serviceHandler.SyncAcquisitionParametersItemsToCloudSystemSequence(companyId, AcquisitionParametersSyncItems);
                if (response == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Method to Sync Meter Models to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="meterModelSyncItems"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncAllMeterModelItems(int companyId, List<MeterModel> meterModelSyncItems)
        {
            try
            {
                int response = serviceHandler.SyncMeterModelItemsToCloudSystemInSequence(companyId, meterModelSyncItems);
                if (response == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                }
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException ?? ex);
                throw ex;
            }
        }

        /// <summary>
        ///  Method to Sync Regional Areas to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="AreaSyncItems"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncAllAreaItems(int companyId, List<Areas> areaSyncItems)
        {
            try
            {
                for (int i = 0; i < areaSyncItems.Count; i++)
                {
                    areaSyncItems[i].UserId = companyId;

                    if (!string.IsNullOrEmpty(areaSyncItems[i].ParentAreaId))
                    {
                        //Check parent exist in the list items
                        //if Area configured as sub area or region but its parent does not exist in the list
                        if (areaSyncItems[i].ParentAreaId != null && areaSyncItems[i].ParentAreaId != "-1")
                        {
                            if (areaSyncItems.Where(x => x.Id == areaSyncItems[i].ParentAreaId).Count() == 0)
                            {
                                //Send Error that parent area should exist in item list
                                ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1005").FirstOrDefault();
                                return objResponseStatus;
                            }
                        }
                    }
                }

                int response = serviceHandler.SyncAreaItemsToCloudSystemInSequence(companyId, areaSyncItems);
                if (response == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                }
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Method to Sync MeterModel Association with FieldTypes
        /// That MeterModal support
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="detailLogId"></param>
        /// <param name="mainLogId"></param>
        /// <param name="meterModelFieldTypesItems"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncAllMeterModelFieldTypes(int companyId, List<MeterModelFieldTypes> meterModelFieldTypesItems)
        {
            try
            {
                int response = serviceHandler.SyncMeterModelFieldTypeItemsToCloudSystemInSequence(companyId, meterModelFieldTypesItems);
                if (response == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (response == 1026)
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1026").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (response == 1027)
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1027").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1002").FirstOrDefault();
                    return objResponseStatus;
                }

            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// Method to Sync Base Meters to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="BaseMeterSyncItems"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncAllBaseMeterItems(int companyId, List<BaseMeter> BaseMeterSyncItems)
        {
            try
            {
                int response = serviceHandler.SyncBaseMeterItemsToCloudSystemInSequence(companyId, BaseMeterSyncItems);
                if (response == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    //send error response to client
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                }
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw ex;
            }
        }

        public Dictionary<int, int> ProcessSyncAllBaseMeterAcquisitionsParameterItems(int companyId, List<BaseMeterAcuisitionParameter> baseMeterAcuisitionParameterSyncItems)
        {
            try
            {
                var response = serviceHandler.SyncBaseMeterAcquisitionsParameterItemsToCloudSystemInSequence(companyId, baseMeterAcuisitionParameterSyncItems);
                return response;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw ex;
            }
        }

        public Dictionary<int, int> ProcessSyncAllVirtualMeterParameterInfoItems(int companyId, List<VirtualMeterParameterInfo> virtualMeteAcuisitionParameterItems)
        {
            try
            {
                var response = serviceHandler.SyncVirtualMeterParameterInfoItemsToCloudSystemInSequence(companyId, virtualMeteAcuisitionParameterItems);
                return response;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw;
            }
        }

        #region "Transaction Records Sync"

        /// <summary>
        /// Method to Sync Minutes Records to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncMinutesRecords(int companyId, List<MinutesRecord> minutesRecordItems)
        {
            try
            {
                int responseCode = serviceHandler.SyncMinutesRecordItemsToCloudSystemInSequence(companyId, minutesRecordItems);

                if (responseCode == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1002)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1002").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1014)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1013").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1022)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1022").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                }

            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Method to Sync Hour Records to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncHourRecords(int companyId, List<HourRecord> hourRecordItems)
        {
            try
            {
                int responseCode = serviceHandler.SyncHourRecordItemsToCloudSystemInSequence(companyId, hourRecordItems);
                if (responseCode == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1002)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1002").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1014)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1014").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1022)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1022").FirstOrDefault();
                    return objResponseStatus;
                }
                else
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                }
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.InnerException ?? ex, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Method to Sync Day Records to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncDayRecords(int companyId, List<DayRecord> dayRecordItems)
        {
            try
            {


                int responseCode = serviceHandler.SyncDayRecordItemsToCloudSystemInSequence(companyId, dayRecordItems);

                if (responseCode == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;

                }
                else if (responseCode == 1002)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1002").FirstOrDefault();
                    return objResponseStatus;

                }
                else if (responseCode == 1015)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1015").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1022)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1022").FirstOrDefault();
                    return objResponseStatus;
                }

                else
                {

                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                    //   ResponseStatus objResponseStatus = new ResponseStatus();
                    //objResponseStatus.Code = responseCode.ToString();
                    //objResponseStatus.Message = "ProcessSyncHourRecords Else";
                    //return new ResponseStatus();
                }

                //List<string> listSyncIds = dayRecordItems.Select(x => x.Id).ToList();

                //int rowCountExist = 0;
                //rowCountExist = serviceHandler.CheckAllDayRecordItemsExists(companyId, listSyncIds);


                ////sync all record item received in list
                //if (rowCountExist == 0)
                //{

                //    /* Get the BaseMeterTypeIds from cloud system
                //     * All the BaseMeterTypeIds from related table BaseMeterFieldType should be synced before processing trasaction minute records
                //     * If Any BaseMeterTypeIds reffered in Minutes recors Item list does not exist in cloud system then Sync will not happen
                //     */
                //    //Now Get BaseMeterTypeId(Primary Key, need to associated record items in cloud system ) and SyncId(Id that was received when BaseMeterFieldType table was Synced)
                //    //Get The List of BaseMeterTypeId reffered in item list for sync

                //    List<string> listBaseMeterAcquisitionParameterIds = dayRecordItems.Where(b => b.BaseMeterAcquisitionParameterId != null).Select(x => x.BaseMeterAcquisitionParameterId).Distinct().ToList();

                //    DataTable dtBaseMeterAcquisitionsParameterPrimaryAndSyncIds = serviceHandler.GetBaseMeterAcquisitionsParameterPrimaryAndSyncIds(companyId, listBaseMeterAcquisitionParameterIds);

                //    if (dtBaseMeterAcquisitionsParameterPrimaryAndSyncIds.Rows.Count == listBaseMeterAcquisitionParameterIds.Count)
                //    {
                //        for (int i = 0; i < dayRecordItems.Count; i++)
                //        {

                //            for (int j = 0; j < dtBaseMeterAcquisitionsParameterPrimaryAndSyncIds.Rows.Count; j++)
                //            {
                //                if (dayRecordItems[i].BaseMeterAcquisitionParameterId == (dtBaseMeterAcquisitionsParameterPrimaryAndSyncIds.Rows[j]["BaseMeterFieldTypeSyncId"]).ToString())
                //                {
                //                    dayRecordItems[i].BaseMeterFieldTypeId = Convert.ToInt32((dtBaseMeterAcquisitionsParameterPrimaryAndSyncIds.Rows[j]["BaseMeterFieldTypeId"]));

                //                    break;
                //                }


                //            }
                //        }

                //        //Now Sync All the Records to Cloud System

                //        Boolean response = serviceHandler.SyncDayRecordItemsToCloudSystem(companyId, dayRecordItems);

                //        if (response)
                //        {
                //            //Send success response to client 
                //            ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                //            return objResponseStatus;
                //        }
                //        else
                //        {
                //            //send error response to client

                //            ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1002").FirstOrDefault();
                //            return objResponseStatus;
                //        }


                //    }
                //    else
                //    {
                //        //send error response stating that for some transaction records base BaseMeterTypeId does not exist in cloud system
                //        ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1015").FirstOrDefault();
                //        return objResponseStatus;

                //    }



                //}
                //else
                //{
                //    //Send error response for batch of items  not synced because items in batch fully or partly alerady exist in cloud system
                //    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1003").FirstOrDefault();
                //    return objResponseStatus;
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        /// <summary>
        /// Method to Sync Month Records to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncMonthRecords(int companyId, List<MonthRecord> monthRecordItems)
        {
            try
            {


                int responseCode = serviceHandler.SyncMonthRecordItemsToCloudSystemInSequence(companyId, monthRecordItems);

                if (responseCode == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;

                }
                else if (responseCode == 1002)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1002").FirstOrDefault();
                    return objResponseStatus;

                }
                else if (responseCode == 1016)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1016").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1022)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1022").FirstOrDefault();
                    return objResponseStatus;
                }

                else
                {

                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                    //   ResponseStatus objResponseStatus = new ResponseStatus();
                    //objResponseStatus.Code = responseCode.ToString();
                    //objResponseStatus.Message = "ProcessSyncHourRecords Else";
                    //return new ResponseStatus();
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        /// <summary>
        /// Method to Sync Year Records to Cloud System
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public ResponseStatus ProcessSyncYearRecords(int companyId, List<YearRecord> yearRecordItems)
        {
            try
            {


                int responseCode = serviceHandler.SyncYearRecordItemsToCloudSystemInSequence(companyId, yearRecordItems);

                if (responseCode == 1001)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1001").FirstOrDefault();
                    return objResponseStatus;

                }
                else if (responseCode == 1002)
                {
                    //Send success response to client 
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1002").FirstOrDefault();
                    return objResponseStatus;

                }
                else if (responseCode == 1017)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1017").FirstOrDefault();
                    return objResponseStatus;
                }
                else if (responseCode == 1022)
                {
                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1022").FirstOrDefault();
                    return objResponseStatus;
                }

                else
                {

                    ResponseStatus objResponseStatus = serviceResponseMessageList.Where(x => x.Code == "1023").FirstOrDefault();
                    return objResponseStatus;
                    //   ResponseStatus objResponseStatus = new ResponseStatus();
                    //objResponseStatus.Code = responseCode.ToString();
                    //objResponseStatus.Message = "ProcessSyncHourRecords Else";
                    //return new ResponseStatus();
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        #endregion

    }
}
