using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SianalR.MSSQL.Monitor
{
    [HubName("DataHub")]
    public class DataHub : Hub
    {
        public static void ShowChangedData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DataHub>();
            context.Clients.All.displayStatus();
        }
    }
}