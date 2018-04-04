using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SignalR简单通讯;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(Startup))]
namespace SignalR简单通讯
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //添加下面这两行代码后，可以轻松实现跨服务器的消息同步
            //即：如果我在两台服务器172.17.0.1、172.17.0.2上面同时发布了该程序，如果我
            //为了使用DependencyResolver，需要引入Microsoft.AspNet.SignalR.SqlServer
            //目前支持Signalr横向扩展的数据库有SQL Server, Azure Service Bus, Redis
            //SignalR支持横向扩展，可以通过Backplanes
            string connection = WebConfigurationManager.ConnectionStrings["signalr"].ConnectionString;
            GlobalHost.DependencyResolver.UseSqlServer(connection);
            app.MapSignalR();
        }
    }
}