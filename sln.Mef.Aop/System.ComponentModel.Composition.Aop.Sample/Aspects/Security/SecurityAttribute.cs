using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Aspects.Security
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class SecurityAttribute : Attribute
    {
        public SecurityAttribute(string role)
        {
            this.Role = role;
        }

        public string Role { get; private set; }
    }
}
