using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{
    public class Invocation : IInvocation
    {
        private object _target;
        private object _proxy;
        private MethodInfo _method;

        private List<object> _arguments;

        private Queue<Action<IInvocation>> _invokeQuery;

        public Invocation(object target, object proxy, MethodInfo method, object[] arguments, IInterceptor[] interceptors)
        {
            _target = target;
            _proxy = proxy;
            _method = method;

            _arguments = new List<object>(arguments);


            _invokeQuery = new Queue<Action<IInvocation>>();
            foreach (var item in interceptors)
                _invokeQuery.Enqueue(x => item.Intercept(x));

            _invokeQuery.Enqueue(x => x.ReturnValue = Method.Invoke(Target, Arguments));

        }

        public object[] Arguments
        {
            get { return _arguments.ToArray(); }
        }

        public Type[] GenericArguments
        {
            get { return _method.GetGenericArguments(); }
        }

        public MethodInfo Method
        {
            get { return _method; }
        }

        public object Proxy
        {
            get { return _proxy; }
        }

        public object Target
        {
            get { return _target; }
        }

        public object ReturnValue { get; set; }

        public Type TargetType
        {
            get { return _target.GetType(); }
        }

        public object GetArgumentValue(int index)
        {
            return _arguments[index];
        }

        public void SetArgumentValue(int index, object value)
        {
            _arguments[index] = value;
        }


        public void Proceed()
        {
            if (_invokeQuery.Count > 0)
                _invokeQuery.Dequeue().Invoke(this);
        }


    }
}
