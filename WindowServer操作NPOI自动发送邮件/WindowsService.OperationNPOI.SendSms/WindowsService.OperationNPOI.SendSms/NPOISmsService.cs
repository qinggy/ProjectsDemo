using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using WindowsService.OperationNPOI.SendSms.Utilities;

namespace WindowsService.OperationNPOI.SendSms
{
    public partial class NPOISmsService : ServiceBase
    {
        private Timer timer;
        private double Interval = 0;

        public NPOISmsService()
        {
            InitializeComponent();
            string strInterval = ConfigurationManager.AppSettings["ScanInterval"];
            Interval = string.IsNullOrEmpty(strInterval) ? TimeSpan.FromMinutes(15).TotalMilliseconds : TimeSpan.FromMinutes(double.Parse(strInterval)).TotalMilliseconds;
        }

#if DEBUG
        private System.Threading.AutoResetEvent _stoppingSignal = new System.Threading.AutoResetEvent(false);
        public void DebugRun()
        {
            OnStart(Environment.GetCommandLineArgs());
            _stoppingSignal.WaitOne();
        }

#endif

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = Interval;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            timer_Elapsed(null, null);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //System.Threading.Thread.Sleep(6000);
            if (!ConfigFileHelper.VerfyConfigExists())
            {
                return;
            }

            //获取当前服务器时间更新报表状态
            DateTime dtNow = DateTime.Now;
            ConfigFileHelper.UpdateReportState(Interval, dtNow);

            //获取当前服务器时间生成报表文件，发送报表文件
            ConfigFileHelper.UpdateReportDataAndSendEmail(dtNow);
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            timer.Stop();
            timer.Dispose();
            timer.Close();
#if DEBUG
            _stoppingSignal.Set();
#endif
        }
    }
}
