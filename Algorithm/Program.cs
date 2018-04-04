using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * 1，简单的执行Python脚本
             * */
            //var engine = IronPython.Hosting.Python.CreateEngine();
            //engine.CreateScriptSourceFromString("print 'hello world!'").Execute();

            /**
             * 2，从Python文件中读取方法
             * */
            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();
            var source = engine.CreateScriptSourceFromFile("python.py");
            source.Execute(scope);

            var say_hello = scope.GetVariable<Func<object>>("say_hello");
            say_hello();

            var get_text = scope.GetVariable<Func<object>>("get_text");
            var text = get_text().ToString();
            Console.WriteLine(text);

            var add = scope.GetVariable<Func<object, object, object>>("add");
            var result1 = add(1, 2);
            Console.WriteLine(result1);

            var result2 = add("hello ", "world");
            Console.WriteLine(result2);


            Console.ReadLine();
        }
    }
}
