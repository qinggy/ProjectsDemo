using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpringNetAop.Aspects
{
    public class LogBeforeAdvice : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            Console.WriteLine("调用前置通知拦截的方法名—>" + method.Name);
            Console.WriteLine("目标—>" + target);
            Console.WriteLine("参数—>");
            if (args != null)
            {
                foreach (object arg in args)
                {
                    Console.WriteLine("\t: " + arg);
                }
            }
        }
    }
}
