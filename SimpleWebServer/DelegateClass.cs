using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleWebServer
{
    public class DelegateClass
    {
        public delegate void ShowMsgHandler();

        public delegate void ShowResult(string msg);
    }
}
