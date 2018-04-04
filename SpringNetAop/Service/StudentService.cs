using SpringNetAop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringNetAop.Service
{
    public class StudentService : IStudentService
    {
        public void GoToSchool(string studentName, string className)
        {
            Console.WriteLine("计算机({1})班的{0}同学去上学了。。。", studentName, className);
        }

        public void GoHome(string studentName, string className)
        {
            Console.WriteLine("计算机({1})班的{0}同学回家了。。。", studentName, className);
        }
    }  
}
