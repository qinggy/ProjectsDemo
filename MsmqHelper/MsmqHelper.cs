using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace MsmqHelper
{
    public class MsmqHelper
    {
        public static string MsmqString
        {
            get
            {
                string name = "cityindataalarmmsmq";
                return string.Format(@".\Private$\{0}", name);
            }
        }

        public static bool SendMessage(string header, string msmqBody, MessagePriority priority)
        {
            bool IsTrue = false;
            try
            {
                MessageQueue mq = null;
                if (MessageQueue.Exists(MsmqString))
                {
                    mq = new MessageQueue(MsmqString);
                }
                else
                {
                    //如果不存在创建
                    mq = MessageQueue.Create(MsmqString);
                }
                //1. 创建消息
                Message msg = new Message();
                //为了避免存放消息的队列因为计算机的重启等原因而丢失消息，可以设置消息队列对象的Recoverable属性设置为true，从而将消息存放在磁盘上保证消息的传递，默认为false.
                msg.Recoverable = true;
                msg.Priority = priority;
                msg.Label = header;
                msg.Body = msmqBody;
                msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });

                mq.Send(msg);
                IsTrue = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsTrue;
        }
    }
}
