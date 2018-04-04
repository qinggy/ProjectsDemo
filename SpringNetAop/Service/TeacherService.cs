using SpringNetAop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringNetAop.Service
{
    public class TeacherService : ITeacherService
    {
        public void GoToClass(string teacherName, string className)
        {
            Console.WriteLine("{1}系的{0}老师去上课了。。。", teacherName, className);
        }
    }
}
