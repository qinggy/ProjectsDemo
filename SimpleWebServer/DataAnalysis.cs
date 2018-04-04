using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Handler = SimpleWebServer.DelegateClass;

namespace SimpleWebServer
{
    public class DataAnalysis
    {
        Socket clientSocket;
        Handler.ShowResult showMsg;
        Thread receiveThread;

        public DataAnalysis(Socket clientSocket, Handler.ShowResult showMsg)
        {
            this.clientSocket = clientSocket;
            this.showMsg = showMsg;

            receiveThread = new Thread(RecMsg);
            receiveThread.IsBackground = true; //即标示着在主线程结束后，该线程也会立即结束（无论该线程是否正真的执行完）
            receiveThread.Start();
        }

        private void RecMsg()
        {
            byte[] receivearr = new byte[1024 * 1024 * 2];
            int length = -1;
            try
            {
                while (true)
                {
                    length = clientSocket.Receive(receivearr);
                    string strReceive = Encoding.UTF8.GetString(receivearr, 0, length);
                    showMsg(strReceive + "\r\n");
                }
            }
            catch (SocketException e)
            {
                showMsg("异常：" + e.InnerException.Message);
            }
            catch(Exception e)
            {
                showMsg("异常：" + e.InnerException.Message);
            }
        }
    }
}
