using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SignalRChatDemo
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // 註冊 hub 的的預設路由： ~/signalr/hubs
            //RouteTable.Routes.MapHubs();
        }
    }
}