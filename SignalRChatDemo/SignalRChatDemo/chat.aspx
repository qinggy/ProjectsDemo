<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits="SignalRChatDemo.chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SignalR 聊天室</title>
    <style>
        #userName {
            display: none;
            color: red;
        }
    </style>
    <%-- 引用 jQuery 的參考 --%>
    <script src="/Scripts/jquery-1.6.4.min.js"></script>
    <%-- 引用 SignalR 的參考 --%>
    <script src="/Scripts/jquery.signalR-2.0.3.min.js"></script>

    <%-- 這邊滿重要的，這個參考是動態產生的，當我們 build 之後才會動態建立這個資料夾，且需引用在 jQuery 和 signalR 之後 --%>
    <script src="/signalr/hubs"></script>
    <script type="text/javascript">
        var userID = "";

        $(function () {

            while (userID.length == 0) {
                userID = window.prompt(" 請輸入使用者名稱 ");
                if (!userID)
                    userID = "";
            }
            $("#userName").append(userID).show();

            // 建立與 Server 端的 Hub 的物件，注意 Hub 的開頭字母一定要為小寫
            var chat = $.connection.codingChatHub;

            // 建立連線後，我們接著來定義 client 端的 function 來讓 Server 端的 hub 呼叫。

            chat.client.hello = function (message) {
                $("#messageList").append("<li>" + message + "</li");
            }
            chat.client.sendAllMessge = function (message) {
                $("#messageList").append("<li>" + message + "</li");
                $("#message").val('');
            }

            // 將連線打開
            $.connection.hub.start().done(function () {
                // 當連線完成後，呼叫 Server 端的 hello 方法，並傳送使用者姓名給 Server
                chat.server.hello(userID);
            });;

            $("#send").click(function () {
                // 呼叫 Server 端的 sendMessage 方法，並傳送使用者姓名及訊息內容給 Server
                chat.server.sendMessage(userID, $("#message").val());
            });

        });
    </script>
</head>
<body>
    <p id="userName">Hi ！ </p>
    <div id="messageBox">
        <p>聊天室內容 </p>
        <ul id="messageList"></ul>
    </div>
    <div>
        <input type="text" id="message" />
        <input type="button" id="send" value=" 發送 " />
    </div>
</body>
</html>
