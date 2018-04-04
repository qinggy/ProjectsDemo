using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Wcf.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        ResponseUserTokenInfo ValidateUser(string userId, string password);

        [OperationContract]
        [TokenValidation]
        string GetData(int value);

        [OperationContract]
        [TokenValidation]
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class ResponseUserTokenInfo
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string LogId { get; set; }
    }
}
