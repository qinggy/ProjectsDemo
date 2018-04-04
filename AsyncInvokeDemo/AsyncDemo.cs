using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncInvokeDemo
{
    public class AsyncDemo
    {
        public delegate string AsyncMethodCaller();

        public string CallUrl()
        {
            Thread.Sleep(5000);
            return "the Async Method Call";
        }
    }
}
