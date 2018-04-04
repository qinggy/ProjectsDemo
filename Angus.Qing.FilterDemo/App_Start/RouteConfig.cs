using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Angus.Qing.FilterDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "Test", controller = "Home", action = "Index", xx = "as", id = "dd" },
                constraints: new { },
                namespaces: new string[] { "" }
            );
        }
    }
}