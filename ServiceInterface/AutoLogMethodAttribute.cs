using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterface
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class AutoLogMethodAttribute : Attribute { }
}
