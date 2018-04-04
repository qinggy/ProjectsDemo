using InvokeClient.Wcf.Validation.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace InvokeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //校验当前用户是否有权限调用该Wcf服务
            Wcf.Validation.Service.ServiceClient serviceClient = new Wcf.Validation.Service.ServiceClient();
            Wcf.Validation.Service.ResponseUserTokenInfo tokenInfo = serviceClient.ValidateUser("userId", "password");

            //判断返回来的TokenInfo信息，如果通过调用需要的接口
            CompositeType comp = new CompositeType
            {
                BoolValue = true,
                StringValue = "Wcf用户权限校验demo"
            };

            //调用前需要创建MessageHeader
            //方法一
            //serviceClient = CreateMessageHeader(tokenInfo.Token); 

            //方法二
            using (var scope = new OperationContextScope(serviceClient.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", tokenInfo.Token);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                CompositeType composite = serviceClient.GetDataUsingDataContract(comp);
                Console.WriteLine("调用返回" + composite.StringValue);
            }

            //CompositeType composite = serviceClient.GetDataUsingDataContract(comp);

            //Console.WriteLine("调用返回" + composite.StringValue);
            Console.ReadKey();
        }

        static Wcf.Validation.Service.ServiceClient CreateMessageHeader(string serviceToken)
        {
            string userToken = string.Empty;
            MessageHeader header;
            OperationContextScope scope;

            userToken = serviceToken;
            Wcf.Validation.Service.ServiceClient objService = new Wcf.Validation.Service.ServiceClient();

            //Defining the scope
            scope = new OperationContextScope(objService.InnerChannel);

            //Creating the Message header
            header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", userToken);

            //Adding the created Message header with client request
            OperationContext.Current.OutgoingMessageHeaders.Add(header);

            return objService;
        }
    }
}
