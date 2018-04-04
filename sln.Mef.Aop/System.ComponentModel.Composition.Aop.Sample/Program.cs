using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.Composition.Aop.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext.Current.User = "Sun.M";
            AppContext.Current.Role = "Admin";


            var accoutService = ServiceLocator.GetService<Services.IAccountService>();

            var createResult = accoutService.CreateUser("Sun.M", "1343434");
            Console.WriteLine(createResult); // throw exception : 名称已存在

            AppContext.Current.Role = "Other";
            createResult = accoutService.CreateUser("M.K", "1343434");
            Console.WriteLine(createResult); // throw exception : 没有权限

            AppContext.Current.Role = "Admin";
            createResult = accoutService.CreateUser("M.K", "1343434");
            Console.WriteLine(createResult); // 添加成功


            Console.ReadKey();

        }
    }


}
