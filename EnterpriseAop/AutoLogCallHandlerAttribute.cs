using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseAop
{
    public class AutoLogCallHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            return new AutoLogCallHandler() { Order = this.Order };
        }
    }
}
