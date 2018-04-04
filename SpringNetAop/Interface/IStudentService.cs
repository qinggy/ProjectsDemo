using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringNetAop.Interface
{
    public interface IStudentService
    {
        void GoToSchool(string studentName, string className);

        void GoHome(string studentName, string className);
    } 
}
