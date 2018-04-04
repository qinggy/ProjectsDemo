using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPExample
{
    [AutoLog]
    public class ObjectClassAOP : ContextBoundObject
    {
        [Method]
        public void Test1()
        {
            Console.WriteLine("Test");
        }
        [Method]
        public void Test2(string para)
        {
            Console.WriteLine("Test:" + para);
        }
        public void Test3(int para)
        {
            Console.WriteLine("Test:" + para);
        }
    }
}
