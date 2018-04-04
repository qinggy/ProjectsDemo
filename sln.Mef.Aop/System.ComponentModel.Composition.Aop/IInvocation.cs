using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{
    /// <summary>
    /// 拦截时的一些信息
    /// </summary>
    public interface IInvocation
    {
        /// <summary>
        /// 获取拦截方法的参数列表
        /// </summary>
        object[] Arguments { get; }

        /// <summary>
        /// 获取拦截方法的泛型参数
        /// </summary>
        Type[] GenericArguments { get; }

        /// <summary>
        /// 获取拦截方法的信息
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// 获取代理对象
        /// </summary>
        object Proxy { get; }

        /// <summary>
        /// 被创建成代理的对象，即原始对象
        /// </summary>
        object Target { get; }

        /// <summary>
        /// 获取或设定返回值
        /// </summary>
        object ReturnValue { get; set; }

        /// <summary>
        /// 获取目标类型，即被拦截的类型（非代理类型）
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// 获取指定所以的参数值
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <returns>返回指定的参数值，或者抛出异常</returns>
        /// <exception cref="IndexOutOfRangeException">IndexOutOfRangeException</exception>
        object GetArgumentValue(int index);

        /// <summary>
        /// 设定指定索引的参数值
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <param name="value">要设定的值</param>
        /// <exception cref="IndexOutOfRangeException">IndexOutOfRangeException</exception>
        void SetArgumentValue(int index, object value);

        /// <summary>
        /// 执行下一个拦截器，如果没有拦截器了，则执行被拦截的方法
        /// </summary>
        void Proceed();
    }
}
