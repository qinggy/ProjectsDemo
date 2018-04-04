using MsmqHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Msmq.SendClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始发送Msmq消息");
            List<MsgModel> list = new List<MsgModel>();
            for (int i = 0; i < 10; i++)
            {
                MsgModel msg = new MsgModel
                {
                    Id = (i + 1).ToString(),
                    Name = "Msmq" + i
                };

                list.Add(msg);
            }

            string msmqBody = JsonConvert.SerializeObject(list);
            MsmqHelper.MsmqHelper.SendMessage("cityindata", msmqBody, System.Messaging.MessagePriority.High);
            Console.WriteLine("发送完成");

            Console.ReadKey();
        }
    }
}
