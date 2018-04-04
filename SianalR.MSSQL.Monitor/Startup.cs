using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.SqlServer;
using Microsoft.Owin;
using Owin;
using SianalR.MSSQL.Monitor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace SianalR.MSSQL.Monitor
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //开启跨服务器支持，通过连接共同的一个SQL服务器
            var sqlconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GlobalHost.DependencyResolver.UseSqlServer(sqlconn);
            //End
            app.MapSignalR();
        }
    }
}