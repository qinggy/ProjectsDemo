using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseAop
{
    public class Employee : MarshalByRefObject
    {
        public Employee() { }

        public string Name { get; set; }

        [AutoLogCallHandler()]
        public void Work()
        {
            Console.WriteLine("Now is {0},{1} is working hard!", DateTime.Now.ToShortTimeString(), Name);
            throw new Exception("Customer Exception");
        }

        [AutoLogCallHandler()]
        public override string ToString()
        {
            return string.Format("I'm {0}.", Name);
        }  
    }
}
