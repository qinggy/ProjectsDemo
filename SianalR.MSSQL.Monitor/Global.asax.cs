using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SianalR.MSSQL.Monitor
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //开启SQL改变通知
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }


        protected void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}