using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{

    public static class ReflectionHelper
    {
        public static MethodInfo GetMethod(Type type, string name, Type[] types, bool isGeneric)
        {
            if (!isGeneric)
                return type.GetMethod(name, types);

            // 此处对于泛型方法的查找，并不精确，所以可以改进一下
            return type.GetMethods().Where(x => 
                x.Name == name && 
                x.IsGenericMethod && 
                x.GetParameters().Length == types.Length)
                .First();

        }

    }
}
