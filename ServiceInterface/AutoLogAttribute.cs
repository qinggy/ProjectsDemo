using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace ServiceInterface
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoLogAttribute : ContextAttribute, IContributeObjectSink
    {
        public AutoLogAttribute()
            : base("AutoLog")
        {
        }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new AutoLogHandler(nextSink);
        }
    }
}
