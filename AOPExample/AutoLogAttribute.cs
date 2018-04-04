using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace AOPExample
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoLogAttribute : ContextAttribute
    {
        public AutoLogAttribute()
            : base("AutoLog")
        { }

        public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
        {
            ctorMsg.ContextProperties.Add(new AutoLogProperty());
        }
    }
}
