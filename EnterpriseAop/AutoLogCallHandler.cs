using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EnterpriseAop
{
    public class AutoLogCallHandler : ICallHandler
    {
        private LogWriter logWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

        public AutoLogCallHandler() { }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            StringBuilder sb = null;
            ParameterInfo pi = null;
            string methodName = input.MethodBase.Name;
            logWriter.Write(string.Format("Enter method " + methodName));
            if (input.Arguments != null && input.Arguments.Count > 0)
            {
                sb = new StringBuilder();
                for (int i = 0; i < input.Arguments.Count; i++)
                {
                    pi = input.Arguments.GetParameterInfo(i);
                    sb.Append(pi.Name).Append(" : ").Append(input.Arguments[i]).AppendLine();
                }
                logWriter.Write(sb.ToString());
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            IMethodReturn result = getNext()(input, getNext);
            //如果发生异常则，result.Exception != null
            if (result.Exception != null)
            {
                logWriter.Write("Exception:" + result.Exception.Message);
                //必须将异常处理掉，否则无法继续执行
                result.Exception = null;
            }
            sw.Stop();
            logWriter.Write(string.Format("Exit method {0}, use {1}.", methodName, sw.Elapsed));
            return result;
        }

        public int Order { get; set; }

    }
}
