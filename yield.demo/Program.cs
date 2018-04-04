using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace yield.demo
{
    class Program
    {
        //yield关键字用于遍历循环中，yield return用于返回IEnumerable<T>,yield break用于终止循环遍历。
        //通过单步调试发现：
        //虽然2种方法的输出结果是一样的，但运作过程迥然不同。第一种方法，是把结果集全部加载到内存中再遍历；第二种方法，客户端每调用一次，yield return就返回一个值给客户端，是"按需供给"。
        //第一种方法，客户端调用过程大致为：

        //使用yield return，客户端调用过程大致为：

        //使用yield return为什么能保证每次循环遍历的时候从前一次停止的地方开始执行呢？
        //因为，编译器会生成一个状态机来维护迭代器的状态。

        static void Main(string[] args)
        {
            Console.WriteLine("不使用yield return的实现 调用开始");
            foreach (var item in FilterWithoutYield())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("不使用yield return的实现 调用结束");
            Console.ReadKey();

            Console.WriteLine("使用yield return的实现 调用开始");
            foreach (var item in FilterWithYield())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("使用yield return的实现 调用结束");

            Console.ReadLine();
        }

        //不使用yield return的实现
        static IEnumerable<int> FilterWithoutYield()
        {
            List<int> result = new List<int>();
            foreach (int i in GetInitialData())
            {
                if (i > 2)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        //使用yeild return实现
        static IEnumerable<int> FilterWithYield()
        {
            foreach (int i in GetInitialData())
            {
                if (i > 2)
                {
                    yield return i;
                }
            }
            yield break;
            Console.WriteLine("这里的代码不执行");
        }

        static List<int> GetInitialData()
        {
            return new List<int>() { 1, 2, 3, 4 };
        }
    }
}
