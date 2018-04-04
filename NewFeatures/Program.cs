using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewFeatures.Extensions;

namespace NewFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "This is the C# New Features Demo";
            source.CopyFrom(source);

            Console.ReadKey();
        }
    }
}
