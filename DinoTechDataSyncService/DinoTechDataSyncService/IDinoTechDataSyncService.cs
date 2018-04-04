using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DinoTechDataSyncService.WCF;
using DinoTechDataSyncService.Domain;
using DinoTechDataSyncService.Business;
using DinoTechDataSyncService.WCF.FileUtilities;

namespace DinoTechDataSyncService.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDinoTechDataSyncService
    {

        [OperationContract]
        ResponseUserTokenInfo ValidateUser(string userId, string password, string timeZoneOffSet);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitEnergies(List<Energy> Energies);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitMeterModels(List<MeterModel> meterModelItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitAquisitionParameters(List<AcquisitionParameters> acquisitionParametersItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitRegionalAreas(List<Areas> areaItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitMeterModelFieldTypes(List<MeterModelFieldTypes> meterModelFieldTypesItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitBaseMeters(List<BaseMeter> baseMeterItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        Dictionary<int, int> SubmitBaseMeterAcquisitionParameters(List<BaseMeterAcuisitionParameter> baseMeteAcuisitionParameterItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        Dictionary<int, int> SubmitVirtualMeterAcquisitionParameters(List<VirtualMeterParameterInfo> virtualMeteParameterInfoItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitMinutesRecords(List<MinutesRecord> minutesRecordItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitHourRecords(List<HourRecord> hourRecordItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitDayRecords(List<DayRecord> dayRecordItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitMonthRecords(List<MonthRecord> monthRecordItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SubmitYearRecords(List<YearRecord> yearRecordItems);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus UpdateSyncLogInfo();

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        int GetMinutesRecordSyncCount();

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        int GetHourRecordSyncCount();

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        int GetDayRecordSyncCount();

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        int GetMonthRecordSyncCount();

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        int GetYearRecordSyncCount();

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        string GetMinutesRecordsLastSyncDateTime(int bmftId);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        string GetInstantaneousDataLastSyncDateTime(string bmfId, int dType);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        string GetHourRecordLastSyncDateTime(int bmftId);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        string GetDayRecordsLastSyncDateTime(int bmftId);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        string GetMonthRecordLastSyncDateTime(int bmftId);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        string GetYearRecordLastSyncDateTime(int bmftId);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        List<EnergyPowerMeterDataTree> GetListOfAllAreaMeterAndParametersOnCloud();

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus SyncEnergyMeterDataToCloud(List<EnergyMeterHistoryDataRecord> energyMeterHistoryDataRecord);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(ServiceError))]
        ResponseStatus AddRealTimeBTMeterData(RequestBTMData requestBTMeterData);
    }

    [DataContract]
    public class ServiceError
    {
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }

    }
}

