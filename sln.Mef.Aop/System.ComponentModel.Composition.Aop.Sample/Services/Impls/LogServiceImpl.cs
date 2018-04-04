using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Services.Impls
{
    [Export(typeof(ILogService))]
    public class LogServiceImpl : ILogService
    {
        public void Log(string log)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Log:");
            Console.WriteLine(log);
            Console.WriteLine();

            Console.ForegroundColor = currentColor;
        }
    }
}
