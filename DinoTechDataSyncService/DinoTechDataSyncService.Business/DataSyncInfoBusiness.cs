using DinoTechDataSyncService.Domain;
using DinoTechDataSyncService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DinoTechDataSyncService.Business
{
    public class DataSyncInfoBusiness
    {
        public DinoTechDataSyncServiceHandler serviceHandler = new DinoTechDataSyncServiceHandler();
        public ResponseMessageList serviceResponseMessageList = new ResponseMessageList();
        public List<EnergyPowerMeterDataTree> GetListOfAllAreaMeterAndParametersOnCloud(int companyId)
        {
            List<EnergyPowerMeterDataTree> listOfAllAreaMeterAndParametersOnCloud;
            try
            {
                listOfAllAreaMeterAndParametersOnCloud = this.serviceHandler.GetListOfAllAreaMeterAndParametersOnCloud(companyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listOfAllAreaMeterAndParametersOnCloud;
        }
        public ResponseStatus SyncEnergyMeterDataToCloud(EnergyMeterHistoryDataRecord energyMeterHistoryDataRecord)
        {
            ResponseStatus result;
            try
            {
                int num = this.serviceHandler.SyncEnergyMeterDataToCloud(energyMeterHistoryDataRecord);
                if (num == 1000)
                {
                    ResponseStatus responseStatus = (
                        from x in this.serviceResponseMessageList
                        where x.Code == "1000"
                        select x).FirstOrDefault<ResponseStatus>();
                    result = responseStatus;
                }
                else if (num == 1024)
                {
                    ResponseStatus responseStatus2 = (
                        from x in this.serviceResponseMessageList
                        where x.Code == "1024"
                        select x).FirstOrDefault<ResponseStatus>();
                    result = responseStatus2;
                }
                else
                {
                    ResponseStatus responseStatus3 = (
                        from x in this.serviceResponseMessageList
                        where x.Code == "1025"
                        select x).FirstOrDefault<ResponseStatus>();
                    result = responseStatus3;
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
