using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketDemoClient
{
    class Program
    {
        /**
         * 使用Socket通信，在客户端的创建步骤如下：
         * 步骤一：使用给定的端口和IP创建EndPoint。
         * 步骤二：创建一个Socket。
         * 步骤三：使用Socket对象的Connect()方法，以上面创建的EndPoint为参数向服务端发送连接请求。
         * 步骤四：如果连接成功，使用Socket对象的Send()方法向服务端发送数据。
         * 步骤五：使用Socket对象的Receive()方法接受从服务端返回的信息。
         * 步骤六：通信完毕关闭Socket。
         * **/
        static void Main(string[] args)
        {
            try
            {
                int port = 2000;
                string host = "127.0.0.1";

                IPAddress IP = IPAddress.Parse(host);
                IPEndPoint endPoint = new IPEndPoint(IP, port);

                //创建Socket， 连接服务器
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("Conneting......");
                socket.Connect(endPoint);

                //向服务器发送数据
                string sendStr = "Hello! Socket Server, I will Connet";
                byte[] sendAry = Encoding.ASCII.GetBytes(sendStr);
                Console.WriteLine("Send Message......");
                socket.Send(sendAry, sendAry.Length, 0);

                //接受从服务器发过来的信息
                string receiveStr = "";
                byte[] receiveAry = new byte[1024];
                int bytes = 0;
                bytes = socket.Receive(receiveAry, receiveAry.Length, SocketFlags.None);
                receiveStr += Encoding.ASCII.GetString(receiveAry, 0, bytes);
                Console.WriteLine("Client Get Message {0}", receiveStr);
                socket.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("argumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("Press Enter To Exit");
            Console.ReadKey();
        }
    }
}
