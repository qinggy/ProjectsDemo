using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;

namespace MSMQ.Demo
{
    class Program
    {
        static Thread PuschThread;
        static bool IsStatus = false;

        static void Main(string[] args)
        {
            Console.WriteLine("开始接受Msmq消息");
            IsStatus = true;
            PuschThread = new Thread(new ThreadStart(ReceiveMessage));
            PuschThread.IsBackground = true;
            PuschThread.Start();

            Console.ReadKey();
        }

        private static void ReceiveMessage()
        {
            try
            {
                while (IsStatus)
                {
                    if (MessageQueue.Exists(MsmqHelper.MsmqHelper.MsmqString))
                    {
                        MessageQueue mq = new MessageQueue(MsmqHelper.MsmqHelper.MsmqString);
                        System.Messaging.Message message = mq.Receive();//Receive()获取并删除， mq.Peek()获取不删除
                        message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                        if (message != null)
                        {
                            //消息类型 对应 FSMP.MSMQ.SendMessage.MessageType值
                            var Lable = message.Label.ToString();
                            string Body = message.Body.ToString();

                            var messagelist = JsonConvert.DeserializeObject<List<MsmqHelper.MsgModel>>(Body);
                            Console.WriteLine("接受一批次Msmq消息");
                            foreach (var item in messagelist)
                            {
                                //var s = PushToMessage.set_pushtosms(item.SchoolCode, item.UserId, item.MessageId, (int)MessageType.Message, item.Content, decimal.MinusOne);
                                //存入数据库，SignalR通知客户端

                                //if (s)
                                //{
                                //    success++;
                                //}
                                //else
                                //{
                                //    fail++;
                                //}

                                Console.WriteLine(item.Name);
                            }
                        }

                        //var body = mq.Receive();//获取最近消息并删除，如果没有消息会一直阻塞当前线程，直到获取到消息或者超时

                        System.Threading.Thread.Sleep(100);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
