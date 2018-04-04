using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;

namespace QueueManager
{

    public class QueueManager<T> where T : class
    {
        /// <summary>
        /// 获取队列中数据后，供客户端处理事件执行
        /// </summary>
        public static event Func<Message, bool> QueueMessageBodyHanlder;
        private static AutoResetEvent _stoppingSignal = new AutoResetEvent(false);
        /// <summary>
        /// 记录是否有线程在扫描队列
        /// </summary>
        public static bool IsScan = false;

        /// <summary>
        /// 创建Msmq
        /// </summary>
        /// <param name="queuePath">队列路径</param>
        /// <param name="transactional">是否事务队列，默认false</param>
        public static MessageQueue CreateQueue(string queuePath, bool transactional = false)
        {
            if (!MessageQueue.Exists(queuePath))
            {
                return MessageQueue.Create(queuePath);
            }
            else
            {
                return new MessageQueue(queuePath);
            }
        }

        /// <summary>
        /// 删除队列
        /// </summary>
        /// <param name="queuePath"></param>
        public static void DeleteQueue(string queuePath)
        {
            if (MessageQueue.Exists(queuePath))
            {
                MessageQueue.Delete(queuePath);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T">消息实体类型</typeparam>
        /// <param name="target">消息数据</param>
        /// <param name="queuePath">队列路径</param>
        /// <param name="tran">事物</param>
        /// <returns></returns>
        public static bool SendMessage(T target, string queuePath, MessageQueueTransaction tran = null, string label = null)
        {
            MessageQueue mq = CreateQueue(queuePath);
            Message msg = new Message();
            msg.Body = target;
            if (label != null)
            {
                msg.Label = label;
            }

            msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            if (tran == null)
            {
                mq.Send(msg);
            }
            else
            {
                mq.Send(msg, tran);
            }

            return true;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queuePath">队列路径</param>
        /// <param name="tran">事物队列</param>
        /// <returns></returns>
        public static T ReceiveMessage(string queuePath, MessageQueueTransaction tran = null)
        {
            MessageQueue mq = CreateQueue(queuePath);
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            Message msg = tran == null ? mq.Receive() : mq.Receive(tran);

            return (T)msg.Body;
        }

        /// <summary>
        /// 采用Peek方式接收消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queuePath">队列路径</param>
        /// <returns></returns>
        public static T ReceiveMessageByPeek(string queuePath)
        {
            MessageQueue mq = CreateQueue(queuePath);
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            Message msg = mq.Peek();

            return (T)msg.Body;
        }

        /// <summary>
        /// 通过Peek获取Message
        /// </summary>
        /// <param name="queuePath">队列路径</param>
        /// <returns></returns>
        public static Message ReceiveMessageEntityByPeek(string queuePath)
        {
            MessageQueue mq = CreateQueue(queuePath);
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

            return mq.Peek();
        }

        /// <summary>
        /// 接收所有消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queuePath">队列路径</param>
        /// <returns></returns>
        public static List<T> GetAllMessage(string queuePath)
        {
            MessageQueue mq = CreateQueue(queuePath);
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            Message[] msgs = mq.GetAllMessages();

            List<T> list = new List<T>();
            msgs.ToList().ForEach(o => list.Add((T)o.Body));

            return list;
        }

        /// <summary>
        /// 使用异步方式Peek接收消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queuePath">队列路径</param>
        /// <returns></returns>
        public static void ReceiveMessageByPeekAsync(string queuePath)
        {
            MessageQueue mq = CreateQueue(queuePath);
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            mq.PeekCompleted += mq_PeekCompleted;
            mq.BeginPeek();
        }

        static void mq_PeekCompleted(object sender, PeekCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)sender;
            Message msg = mq.EndPeek(asyncResult.AsyncResult);
            if (QueueMessageBodyHanlder != null)
            {
                if (QueueMessageBodyHanlder.Invoke(msg))
                {
                    mq.ReceiveById(msg.Id);
                }
            }
            QueueManager<T>.IsScan = false;

            mq.BeginPeek();
        }

        /// <summary>
        /// 使用异步方式Receive接收消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queuePath">队列路径</param>
        public static void ReceiveMessageByReceiveAsync(string queuePath)
        {
            MessageQueueTransaction tran = new MessageQueueTransaction();
            MessageQueue mq = CreateQueue(queuePath);
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            mq.ReceiveCompleted += mq_ReceiveCompleted;
            tran.Begin();
            mq.BeginReceive();
            _stoppingSignal.WaitOne();
            tran.Commit();
        }

        static void mq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)sender;
            Message msg = mq.EndReceive(asyncResult.AsyncResult);
            _stoppingSignal.Set();
            if (QueueMessageBodyHanlder != null)
            {
                //如果处理不成功，继续加入队列，等待下次处理
                if (!QueueMessageBodyHanlder.Invoke(msg))
                {
                    SendMessage((T)msg.Body, mq.Path);
                }
            }

            mq.BeginReceive();
        }
    }
}
