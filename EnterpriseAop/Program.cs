using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseAop
{
    class Program
    {
        private static LogWriter logWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

        static void Main(string[] args)
        {
            Employee emp = PolicyInjection.Create<Employee>();
            emp.Name = "Lele";
            emp.Work();
            Console.WriteLine(emp);
        }
    }
}
