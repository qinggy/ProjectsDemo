using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class InterceptorAttribute : Attribute, IInterceptor
    {
        /// <summary>
        /// 拦截器类型
        /// </summary>
        public Type Type { get; set; }

        private IInterceptor _interceptor;

        public InterceptorAttribute() { }
        public InterceptorAttribute(Type interceptorType)
        {
            this.Type = interceptorType;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            if (this.Type != null)
            {
                if (_interceptor == null)
                    _interceptor = Activator.CreateInstance(this.Type) as IInterceptor;

                _interceptor.Intercept(invocation);
            }
        }


        /// <summary>
        /// 拦截器的执行顺序
        /// </summary>
        public virtual int Order { get; set; }

    }
}
