using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace SimpleWebServer
{
    public partial class WebServer : Form
    {
        Socket watchSocket = null;
        Thread acceptThread = null;
        bool isRunning = false;

        Dictionary<string, Socket> connetionSocketDic = new Dictionary<string, Socket>();

        public WebServer()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            isRunning = true;
            IPAddress ip = IPAddress.Parse(this.txtIP.Text.Trim());
            IPEndPoint endpoint = new IPEndPoint(ip, int.Parse(this.txtPort.Text.Trim()));
            watchSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            watchSocket.Bind(endpoint);
            watchSocket.Listen(10);

            acceptThread = new Thread(WatchConnection);
            acceptThread.IsBackground = true;
            acceptThread.Start();

            //ShowMsg("服务器启动成功......\r\n");
            AppendMsg("服务器启动成功......\r\n");
        }

        private void WatchConnection()
        {
            while (isRunning)
            {
                Socket clientSocket = watchSocket.Accept();
                DataAnalysis dataAnalysis = new DataAnalysis(clientSocket, AppendMsg);
                connetionSocketDic.Add(clientSocket.RemoteEndPoint.ToString(), clientSocket);
                //ShowMsg(clientSocket.RemoteEndPoint + "连接成功......\r\n");
            }
        }

        private void ShowMsg(string msg)
        {
            TextBoxValueSetting valueSetting = new TextBoxValueSetting(this.txtMsg, msg);
            if (valueSetting.TextBox.InvokeRequired)
            {
                SimpleWebServer.DelegateClass.ShowMsgHandler showMsg = new DelegateClass.ShowMsgHandler(valueSetting.SetText);
                valueSetting.TextBox.Invoke(showMsg);
            }
            else
            {
                valueSetting.SetText();
            }
        }

        private void AppendMsg(string msg)
        {
            this.txtMsg.AppendText(msg);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            watchSocket.Close();
        }
    }
}
