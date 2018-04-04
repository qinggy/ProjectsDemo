using System;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ServiceInterface
{
    public class AutoLogHandler : IMessageSink
    {

        /// <summary>
        /// 保存下一个接收器
        /// </summary>
        private IMessageSink nextSink;

        /// <summary>
        /// 必须实现的IMessageSink接口属性
        /// </summary>
        public IMessageSink NextSink
        {
            get
            {
                return nextSink;
            }
        }

        /// <summary>
        /// 在构造器中初始化下一个接收器
        /// </summary>
        /// <param name="next"></param>
        public AutoLogHandler(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        /// <summary>
        /// 实现IMessageSink的接口方法，当消息传递的时候，该方法被调用
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMessage retMsg = null;
            IMethodCallMessage call = msg as IMethodCallMessage;

            //如果方法没有打上AutoLogMethod标记
            if (call == null || (Attribute.GetCustomAttribute(call.MethodBase, typeof(AutoLogMethodAttribute))) == null)
            {
                retMsg = NextSink.SyncProcessMessage(msg);
            }
            else
            {
                //拦截消息，做前处理
                Preprocess(msg);
                //传递消息给下一个接收器
                retMsg = nextSink.SyncProcessMessage(msg);
                //调用返回时进行拦截，并进行后处理
                Postprocess(msg, retMsg);
            }

            return retMsg;
        }

        /// <summary>
        /// IMessageSink接口方法，用于异步处理，我们不实现异步处理，所以简单返回null,
        /// 不管是同步还是异步，这个方法都需要定义
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="replySink"></param>
        /// <returns></returns>
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return null;
        }

        /// <summary>
        /// 前处理.
        /// </summary>
        /// <param name="msg"></param>
        private void Preprocess(IMessage msg)
        {
            //检查是否是方法调用，我们只拦截Order的Submit方法。
            IMethodCallMessage call = msg as IMethodCallMessage;
            if (call == null) return;
            StringBuilder buff = new StringBuilder();
            buff.AppendFormat("$LOG$:开始调用{0}方法。参数数量：{1}", call.MethodName, call.InArgCount);
            buff.AppendLine();
            for (int i = 0; i < call.InArgCount; i++)
            {
                buff.AppendFormat("  参数[{0}] : {1}", i + 1, call.InArgs[i]);
                buff.AppendLine();
            }
            Console.Write(buff.ToString());
        }

        /// <summary>
        /// 后处理
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="retMsg"></param>
        private void Postprocess(IMessage msg, IMessage retMsg)
        {
            IMethodCallMessage call = msg as IMethodCallMessage;
            if (call == null)
                return;
            StringBuilder buff = new StringBuilder();
            buff.AppendFormat("$LOG$:调用{0}方法结束", call.MethodName);
            buff.AppendLine();
            Console.Write(buff.ToString());
        }
    }
}
