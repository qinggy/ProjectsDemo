using ServiceInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SpringNetAop
{
    public class ServiceFactory
    {
        public ICookService CreateFactory()
        {
            var ip = ConfigurationManager.AppSettings["ServerIP"];

            var factory = new ChannelFactory<ICookService>(new NetTcpBinding(SecurityMode.None, false) { MaxReceivedMessageSize = 2147483647 }, string.Format("net.tcp://{0}:28888/CookService/", ip));

            return factory.CreateChannel();
        }
    }
}
