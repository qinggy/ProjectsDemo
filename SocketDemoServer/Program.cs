using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketDemoServer
{
    class Program
    {
        /**
             * 使用Socket通信，在服务端的创建步骤如下：
             * 步骤一：使用指定的端口和IP创建一个服务端的EndPoint。
             * 步骤二：创建一个Socket对象。
             * 步骤三：使用Socket对象的Bind()方法绑定EndPoint。
             * 步骤四：使用Socket的Listen()方法开始监听。
             * 步骤五：接受客户端的连接，使用前面创建的Socket对象的Accept()创建新的Socket用于和请求的客户端通信。
             * 步骤六：通信结束关闭Socket。
             * **/
        static void Main(string[] args)
        {
            int port = 2000;
            string host = "127.0.0.1";

            IPAddress IP = IPAddress.Parse(host);
            IPEndPoint endPoint = new IPEndPoint(IP, port);

            //创建Socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(0);
            Console.WriteLine("Waiting for Clients Connection ......");

            //接受客户端的连接并接受数据
            Socket clientSocket = socket.Accept();
            Console.WriteLine("Building Connection ......");
            string receiveStr = string.Empty;
            byte[] receiveAry = new byte[1024];
            int bytes = 0;
            bytes = clientSocket.Receive(receiveAry, receiveAry.Length, 0);
            receiveStr += Encoding.ASCII.GetString(receiveAry, 0, bytes);
            Console.WriteLine("Server Get Message:{0}", receiveStr);

            //向客户端发送数据
            string sendStr = "Ok! Client Send Message Successful!";
            byte[] sendAry = Encoding.ASCII.GetBytes(sendStr);
            clientSocket.Send(sendAry, sendAry.Length, 0);
            clientSocket.Close();
            socket.Close();
            Console.ReadKey();
        }
    }
}
