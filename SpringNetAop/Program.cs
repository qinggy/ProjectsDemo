using Spring.Context;
using Spring.Context.Support;
using SpringNetAop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringNetAop
{
    class Program
    {
        static void Main(string[] args)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            IStudentService command = (IStudentService)context["myStudentService"];
            command.GoToSchool("guwei4037", "一");
            command.GoHome("guwei4037", "一");

            ITeacherService teacherCmd = (ITeacherService)context["myTeacherService"];
            teacherCmd.GoToClass("李宪","计算机");
            Console.ReadLine();
        }
    }
}
