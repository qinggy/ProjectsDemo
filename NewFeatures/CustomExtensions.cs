using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewFeatures.Extensions
{
    public static class CustomExtensions
    {
        public static void CopyFrom<T>(this T Target, T source)
        {
            Console.WriteLine("This Source Class's Length is {0}", source.ToString().Length);
        }
    }
}
