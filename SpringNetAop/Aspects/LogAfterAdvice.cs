using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpringNetAop.Aspects
{
    /// <summary>
    /// 后置通知
    /// </summary>
    public class AfterReturningAdvise : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            Console.Out.WriteLine("     后置通知： 方法调用成功，方法名 : " + method.Name);
            Console.Out.WriteLine("     后置通知： 目标为      : " + target);
            Console.Out.WriteLine("     后置通知： 参数 : ");
            if (args != null)
            {
                foreach (object arg in args)
                {
                    Console.Out.WriteLine("\t: " + arg);
                }
            }
            Console.Out.WriteLine("     后置通知：  返回值是 : " + returnValue);
        }
    }
}
