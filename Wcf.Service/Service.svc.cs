using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Wcf.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class Service1 : IService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public ResponseUserTokenInfo ValidateUser(string userId, string password)
        {
            ResponseUserTokenInfo responseUserInfo = new ResponseUserTokenInfo();
            string token = string.Empty;
            //校验数据库，判断当前用户是否有效，返回Token对象
            //UserBusiness userBusiness = new UserBusiness();
            ////String timeZoneOffSet = GetTimeZoneOffSet();

            //Domain.User userObj = userBusiness.ValidateUser(userId, password, timeZoneOffSet);
            //if (userObj != null)
            //{
            //    if (string.IsNullOrEmpty(userObj.Token))
            //    {
            //        token = CommonFunctions.GenerateToken() + "_" + userObj.UserId.Encrypt();

            //        //Code for Saving Token to Database
            //        userBusiness.UpdateUserToken(userObj.UserId, token);
            //    }
            //    else
            //    {
            //        token = userObj.Token;
            //    }

            //    responseUserInfo.CustomerId = userObj.UserId;
            //    responseUserInfo.Token = token;
            //    responseUserInfo.LogId = logId.Encrypt();
            //    responseUserInfo.Status = "Success";
            //    responseUserInfo.Message = "User authenticated successfully";
            //    return responseUserInfo;
            //}

            //else
            //{
            //    responseUserInfo.Token = "";
            //    responseUserInfo.Status = "Failure";
            //    responseUserInfo.Message = "Incorrect username or password!";

            //    return responseUserInfo;
            //}

            return responseUserInfo;
        }
    }
}
