using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Aop.Sample.Services;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Aspects.Log
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class LogInterceptorAttribute : InterceptorAttribute
    {
        [Import]
        protected ILogService LogService { get; set; }

        public override void Intercept(IInvocation invocation)
        {
            LogService.Log(string.Format("Method:{0} User:{1}", invocation.Method.Name, AppContext.Current.User));

            invocation.Proceed();

            if (invocation.ReturnValue != null)
                LogService.Log(string.Format("Method:{0} ReturnValue:{1}", invocation.Method.Name, invocation.ReturnValue));

        }
    }
}
