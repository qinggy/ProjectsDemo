using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SianalR.MSSQL.Monitor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Values", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "getData",
                url: "{controller}/{action}",
                defaults: new { controller = "Values", action = "getData" }
                );
        }
    }
}