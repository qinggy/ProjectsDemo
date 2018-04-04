using Esd.EnergyPec.CommonImp;
using Esd.EnergyPec.Services;
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

namespace Esd.EnergyPec.Service
{
    public partial class VerCheckService : ServiceBase
    {
        ServiceHost host;

        public VerCheckService()
        {
            InitializeComponent();
        }

        //#if DEBUG
        //        private System.Threading.AutoResetEvent _stoppingSignal = new System.Threading.AutoResetEvent(false);
        //        public void DebugRun()
        //        {
        //            OnStart(Environment.GetCommandLineArgs());
        //            _stoppingSignal.WaitOne();
        //        }
        //#endif

        protected override void OnStart(string[] args)
        {
            string baseUriStr = ConfigurationManager.AppSettings["BaseUri"].ToString();

            host = new ServiceHost(typeof(CheckUpdateService));
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

            host.AddServiceEndpoint(typeof(ICheckUpdateService),
                new NetTcpBinding(SecurityMode.None, false) { MaxReceivedMessageSize = 2147483647 },
                new Uri(baseUriStr));

            host.Open();
        }

        protected override void OnStop()
        {
            host.Close();
            //#if DEBUG
            //            _stoppingSignal.Set();
            //#endif
        }
    }
}
