using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Aspects.Exception
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ExceptionInterceptorAttribute : InterceptorAttribute
    {
        public override void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (System.Exception ex)
            {
                var currentColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Error:");
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine();

                Console.ForegroundColor = currentColor;


                if (invocation.Method.ReturnType != Type.GetType("System.Void"))
                    invocation.ReturnValue = CreateTypeDefaultValue(invocation.Method.ReturnType);
            }
        }

        private object CreateTypeDefaultValue(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }

    }
}
