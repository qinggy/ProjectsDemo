using Esd.EnergyPec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace WindowsService.OperationNPOI.SendSms.Utilities
{/// <summary>
    /// Service Factory
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// 创建wcf通道
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateService<T>(string serviceURL)
        {
            var scAttr = typeof(T).GetCustomAttributes(typeof(ServiceContractAttribute), false)
                .FirstOrDefault() as ServiceContractAttribute;
            if (scAttr == null)
            {
                SystemLogHelper.Logger.Error("无法找到服务实现类");
            }

            if (string.IsNullOrEmpty(serviceURL) || !serviceURL.Contains("net.tcp://"))
            {
                serviceURL = "net.tcp://127.0.0.1:28888/EnergyPEC/{0}/";
            }

            var factory =
                new ChannelFactory<T>(new NetTcpBinding(SecurityMode.None, false)
                {
                    MaxReceivedMessageSize = 2147483647,
                    ReaderQuotas = new XmlDictionaryReaderQuotas() { MaxArrayLength = 2147483647, MaxStringContentLength = 2147483647 }
                },
                    string.Format(serviceURL, scAttr.Name ?? typeof(T).Name)
              );

            foreach (OperationDescription operation in factory.Endpoint.Contract.Operations)
            {
                var opdes = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (opdes != null)
                {
                    opdes.MaxItemsInObjectGraph = Int32.MaxValue;
                }
            }

            T result = factory.CreateChannel();

            return result;
        }

        /// <summary>
        /// 关闭通道
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        public static void CloseService<T>(this T service)
        {
            try
            {
                ((IChannel)service).Close();
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException);
            }
        }
    }
}
