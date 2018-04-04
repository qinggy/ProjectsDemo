using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.ComponentModel.Composition.Aop.___Temp
{
    class Target
    {
        public virtual void Method(string a, bool b)
        {
        }
    }


    class Proxy : Target
    {
        private Target _target;
        private IInterceptor[] _interceptors;

        public Proxy(Target target, IInterceptor[] interceptors)
        {
            _target = target;
            _interceptors = interceptors;
        }

        public override void Method(string a, bool b)
        {
            object[] arguments = new object[] { a, b };
            Type[] argumentTypes = new Type[] { a.GetType(), b.GetType() };
            MethodInfo method = ReflectionHelper.GetMethod(_target.GetType(), "Method", argumentTypes, false);

            var invocation = new Invocation(_target, this, method, arguments, _interceptors);

            invocation.Proceed();
        }
    }

}
