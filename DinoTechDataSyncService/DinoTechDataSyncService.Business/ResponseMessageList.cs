using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DinoTechDataSyncService.Business
{
    public class ResponseMessageList : List<ResponseStatus>
    {

        public ResponseMessageList()
        {
            this.Add(new ResponseStatus { Code = "1000", Status = Enums.SyncStatus.Success.ToString(), Message = "Submitted row  synced on cloud successfully" });

            this.Add(new ResponseStatus { Code = "1001", Status = Enums.SyncStatus.Success.ToString(), Message = "Submitted rows  synced successfully" });
            this.Add(new ResponseStatus { Code = "1002", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully, error occured during sync" });
            this.Add(new ResponseStatus { Code = "1003", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully, because some or all rows already has been synced" });
            this.Add(new ResponseStatus { Code = "1004", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully, because row have dependency on other table rows which need sync first" });
            this.Add(new ResponseStatus { Code = "1005", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully, aleast one area should be the parent Area" });
            this.Add(new ResponseStatus { Code = "1006", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully, all meters should have associated  parent area(at least one parent area should be in the system). provide sync of Area items first" });
            this.Add(new ResponseStatus { Code = "1007", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,EnergyIds associated with BaseMeter items should be first synced before sync of BaseMeter items" });
            this.Add(new ResponseStatus { Code = "1008", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,MeterModelIds associated with BaseMeter items should be first synced before sync of BaseMeter items" });
            this.Add(new ResponseStatus { Code = "1009", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,All BaseAcquisitionParameter items should have associated BaseMeterId" });

            this.Add(new ResponseStatus { Code = "1010", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,BaseMeterIds associated with  BaseAcquisitionParameter items  should be first synced before sync of BaseAcquisitionParameter items" });
            this.Add(new ResponseStatus { Code = "1011", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,AcuisitionParameterId associated with  BaseAcquisitionParameter items  should be first synced before sync of BaseAcquisitionParameter items" });
            this.Add(new ResponseStatus { Code = "1012", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,BaseMeterIds, AcuisitionParameterIds associated with  BaseAcquisitionParameter items  should be first synced before sync of BaseAcquisitionParameter items" });


            this.Add(new ResponseStatus { Code = "1013", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,BaseMeterAcquisitionParameterId associated with  BaseMeterAcquisitionParameter items  should be first synced before sync of minutes record items" });
            this.Add(new ResponseStatus { Code = "1014", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,BaseMeterAcquisitionParameterId associated with  BaseMeterAcquisitionParameter items  should be first synced before sync of hour record items" });
            this.Add(new ResponseStatus { Code = "1015", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,BaseMeterAcquisitionParameterId associated with  BaseMeterAcquisitionParameter items  should be first synced before sync of day record items" });

            this.Add(new ResponseStatus { Code = "1016", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,BaseMeterAcquisitionParameterId associated with  BaseMeterAcquisitionParameter items  should be first synced before sync of month record items" });
            this.Add(new ResponseStatus { Code = "1017", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,BaseMeterAcquisitionParameterId associated with  BaseMeterAcquisitionParameter items  should be first synced before sync of year record items" });
            this.Add(new ResponseStatus { Code = "1018", Status = Enums.SyncStatus.Success.ToString(), Message = "Sync completed successfully and log updated" });
            this.Add(new ResponseStatus { Code = "1019", Status = Enums.SyncStatus.Failure.ToString(), Message = "Sync completed successfully, error occured while updating log entry" });

            this.Add(new ResponseStatus { Code = "1020", Status = Enums.SyncStatus.Success.ToString(), Message = "Sync completed successfully for table" });
            this.Add(new ResponseStatus { Code = "1021", Status = Enums.SyncStatus.Failure.ToString(), Message = "Error occurred on updating Sync detail sync log for table" });
            this.Add(new ResponseStatus { Code = "1022", Status = Enums.SyncStatus.Success.ToString(), Message = "No row synced, rows were already synced previously" });
            this.Add(new ResponseStatus { Code = "1023", Status = Enums.SyncStatus.Failure.ToString(), Message = "rows not synced, some error has been occured" });
            this.Add(new ResponseStatus { Code = "1024", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully, Because row against BaseMeterFieldTypeId with same datetime is already exist" });
            this.Add(new ResponseStatus { Code = "1025", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted row not synced successfully, some error has been occured" });
            this.Add(new ResponseStatus { Code = "1026", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,MeterModelId does not exist in the system, missing MeterModel Item(s) should be synced first " });
            this.Add(new ResponseStatus { Code = "1027", Status = Enums.SyncStatus.Failure.ToString(), Message = "Submitted rows not synced successfully,FieldTypelId does not exist in the system, missing FieldType Item(s) should be synced first " });

        }




    }
}
