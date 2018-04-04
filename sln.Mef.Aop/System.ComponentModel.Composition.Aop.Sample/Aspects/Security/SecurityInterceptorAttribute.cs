using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Aspects.Security
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class SecurityInterceptorAttribute : InterceptorAttribute
    {
        public override void Intercept(IInvocation invocation)
        {

            var securityAttrs = invocation.Method.GetCustomAttributes(typeof(SecurityAttribute), true);
            if (securityAttrs == null || securityAttrs.Length == 0)
            {
                invocation.Proceed(); return;
            }


            var requiredRoles = securityAttrs.Select(x => ((SecurityAttribute)x).Role);

            if (requiredRoles.Any(x => x == AppContext.Current.Role) == false)
            {
                throw new System.Exception(string.Format("对{0}的访问没有权限", invocation.Method.Name));
            }

            invocation.Proceed();

        }

    }
}
