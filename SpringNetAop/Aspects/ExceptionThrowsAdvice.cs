using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringNetAop.Aspects
{
    public class ExceptionThrowsAdvice : IThrowsAdvice
    {
        public void AfterThrowing(FormatException ex)
        {
            Console.WriteLine("发生异常通知");
            Console.WriteLine(ex.Message);
        }
    }
}
