using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRChatDemo
{
    public class codingChatHub : Hub
    {
        public void Hello(string name)
        {
            // 這邊會傳入 name 參數
            // 呼叫所有連線狀態中頁面上的 javascript function => hello
            // 透過 server 端呼叫 client 的 javascript function
            string message = " 歡迎使用者 " + name + " 加入聊天室 ";
            Clients.All.hello(message);
        }

        public void SendMessage(string name, string message)
        {
            // 這邊會傳入 name 和 message 參數
            // 並且會呼叫所有連線狀態中頁面上的 javascript function => sendAllMessage
            // 透過 server 端呼叫 client 的 javascript function
            message = name + ":" + message;
            Clients.All.sendAllMessge(message);
        }
    }
}