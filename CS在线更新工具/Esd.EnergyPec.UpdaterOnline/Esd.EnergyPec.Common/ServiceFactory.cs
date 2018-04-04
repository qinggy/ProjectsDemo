using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Esd.EnergyPec.CommonImp
{
    public static class ServiceFactory
    {
        //private static readonly string baseAddress = "net.tcp://172.17.0.2:28890/VerUpdateOnline/CheckUpdateService";
        private static readonly string baseAddress = "net.tcp://{0}:28890/VerUpdateOnline/CheckUpdateService";

        static ServiceFactory()
        {
            string ip = ConfigurationManager.AppSettings.Get("ServerIP");
            baseAddress = string.Format(baseAddress, ip);
        }

        public static T CreateService<T>()
        {
            var factory = new ChannelFactory<T>(new NetTcpBinding(SecurityMode.None, false) { MaxReceivedMessageSize = 2147483647 }, baseAddress);

            T result = factory.CreateChannel();

            return result;
        }

        public static void CloseService<T>(this T Service)
        {
            try
            {
                ((IChannel)Service).Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
