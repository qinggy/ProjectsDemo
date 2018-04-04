using ServiceDao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;

namespace ServiceFactory
{
    public partial class CookServer : ServiceBase
    {
        private ServiceHost host = null;
#if DEBUG
        private System.Threading.AutoResetEvent _stoppingSignal = new System.Threading.AutoResetEvent(false);
#endif

        public CookServer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                string strUri = ConfigurationManager.AppSettings["BaseUri"].ToString();
                Uri BaseUri = new Uri(strUri);
                Type contractType = typeof(CookService);
                host = new ServiceHost(contractType, BaseUri);
                ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                if (debug == null)
                {
                    host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                }
                else
                {
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }

                foreach (Type interfaceType in contractType.GetInterfaces())
                {
                    var contract = ContractDescription.GetContract(interfaceType);

                    //添加操作行为
                    foreach (var op in contract.Operations)
                    {

                    }

                    ServiceEndpoint endpoint = new ServiceEndpoint(
                        contract,
                        new NetTcpBinding(SecurityMode.None, false)
                        {
                            MaxReceivedMessageSize = 2147483647,
                            MaxBufferPoolSize = 2147483647,
                            MaxBufferSize = int.MaxValue,
                            MaxConnections = int.MaxValue,
                            ReceiveTimeout = TimeSpan.MaxValue,
                            SendTimeout = TimeSpan.MaxValue
                        },
                        new EndpointAddress(BaseUri));

                    host.AddServiceEndpoint(endpoint);
                }

                host.Open();
            }
            catch (Exception e)
            {
                throw new EndpointNotFoundException(e.Message);
            }
        }

        protected override void OnStop()
        {
            host.Close();
#if DEBUG
            _stoppingSignal.Set();
#endif
        }


#if DEBUG
        public void DebugRun()
        {
            OnStart(Environment.GetCommandLineArgs());
            _stoppingSignal.WaitOne();
        }

#endif
    }
}
