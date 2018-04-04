using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Angus.IService
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AopAttribute : ExportAttribute, IFlyMetadata
    {
        public AopAttribute(string methodName)
        {
            this.MethodName = methodName;
        }

        public string MethodName { get; set; }
    }
}
