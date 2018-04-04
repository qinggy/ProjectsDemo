using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR简单通讯
{
    [HubName("ViewDataHub")]
    public class ViewDataHub : Hub
    {
        public string Hello(string message)
        {
            return "hello " + message;
        }

        public void SendMessage(string message)
        {
            Clients.All.talk(message);
        }
    }
}