using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RWLock
{
    class Program
    {
        List<int> list = new List<int>();
        private ReaderWriterLock _rwlock = new ReaderWriterLock();

        static void Main(string[] args)
        {
        }

        private void Read()
        {
            while (true)
            {
                //使用一个 System.Int32 超时值获取读线程锁。
                _rwlock.AcquireReaderLock(100);
                try
                {
                    if (list.Count > 0)
                    {
                        int result = list[list.Count - 1];
                    }
                }
                finally
                {
                    //减少锁计数,释放锁
                    _rwlock.ReleaseReaderLock();
                }
            }
        }

        int WriteCount = 0;//写次数
        private void Write()
        {
            while (true)
            {
                //使用一个 System.Int32 超时值获取写线程锁。
                _rwlock.AcquireWriterLock(100);
                try
                {
                    list.Add(WriteCount++);
                }
                finally
                {
                    //减少写线程锁上的锁计数，释放写锁
                    _rwlock.ReleaseWriterLock();
                }
            }
        }
    }
}
