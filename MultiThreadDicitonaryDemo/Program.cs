using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadDicitonaryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueueTest();
            ConcurrentBagTest();
            ConcurrentDictionaryTest();
        }

        /**
         * 该类有两个重要的方法用来访问队列中的元素.分别是:
            Ø TryDequeue 尝试移除并返回位于队列头开始处的对象.
            Ø TryPeek尝试返回位于队列头开始处的对象但不将其移除.
            现在,在多任务访问集合元素时,我们只需要使用TryDequeue或TryPeek方法,就可以安全的访问集合中的元素了.
         * */

        /**
         * ConcurrentStack 表示线程安全的后进先出(LIFO)栈.它也有几个有用的方法,分别是:
            Ø TryPeek:尝试返回栈顶处的元素,但不移除.
            Ø TryPop: 尝试返回栈顶处的元素并移除.
            Ø TryPopRange: 尝试返回栈顶处开始指定范围的元素并移除.
            在访问集合中的元素时,我们就可以上述方法.具体代码实例于上面的ConcurrentQueue类似
         * */
        static void ConcurrentQueueTest()
        {
            ConcurrentQueue<int> sharedQueue = new ConcurrentQueue<int>();

            for (int i = 0; i < 1000; i++)
            {
                sharedQueue.Enqueue(i);
            }

            int itemCount = 0;
            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    while (sharedQueue.Count > 0)
                    {
                        int queueElement;
                        bool gotElement = sharedQueue.TryDequeue(out queueElement);
                        if (gotElement)
                        {
                            Interlocked.Increment(ref itemCount);
                        }
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Items processed:{0}", itemCount);

            Console.WriteLine("Press Enter to finish");

            Console.ReadLine();
        }

        /**
         * 该类有两个重要的方法用来访问队列中的元素.分别是:
            Ø TryTake 尝试移除并返回位于队列头开始处的对象.
            Ø TryPeek尝试返回位于队列头开始处的对象但不将其移除.
         * */
        static void ConcurrentBagTest()
        {
            ConcurrentBag<int> sharedBag = new ConcurrentBag<int>();
            for (int i = 0; i < 1000; i++)
            {
                sharedBag.Add(i);
            }

            int itemCount = 0;
            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    while (sharedBag.Count > 0)
                    {
                        int queueElement;
                        bool gotElement = sharedBag.TryTake(out queueElement);
                        if (gotElement)
                            Interlocked.Increment(ref itemCount);
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Items processed:{0}", itemCount);
            Console.WriteLine("Press Enter to finish");
            Console.ReadLine();
        }


        /**
         * 实现的是一个键-值集合类.它提供的方法有:
            Ø TryAdd:尝试向集合添加一个键-值
            Ø TryGetValue:尝试返回指定键的值.
            Ø TryRemove:尝试移除指定键处的元素.
            Ø TryUpdate:尝试更新指定键的值
         * */
        static void ConcurrentDictionaryTest()
        {
            BankAccount account = new BankAccount();
            ConcurrentDictionary<object, int> sharedDict = new ConcurrentDictionary<object, int>();

            Task<int>[] tasks = new Task<int>[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                sharedDict.TryAdd(i, account.Balance);
                tasks[i] = new Task<int>((keyObj) =>
                {
                    int currentValue;
                    bool gotValue;

                    for (int j = 0; j < 1000; j++)
                    {
                        gotValue = sharedDict.TryGetValue(keyObj, out currentValue);
                        sharedDict.TryUpdate(keyObj, currentValue + 1, currentValue);
                    }

                    int result;
                    gotValue = sharedDict.TryGetValue(keyObj, out result);
                    if (gotValue)
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception(String.Format("No data item available for key {0}", keyObj));
                    }
                }, i);

                tasks[i].Start();
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                account.Balance += tasks[i].Result;
            }

            Console.WriteLine("Expected value {0}, Balance: {1}", 10000, account.Balance);
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }

        class BankAccount
        {
            public int Balance
            {
                get;
                set;
            }
        }
    }
}
