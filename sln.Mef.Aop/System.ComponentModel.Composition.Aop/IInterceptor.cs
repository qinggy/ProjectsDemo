using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{
    /// <summary>
    /// 拦截器
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// 执行拦截方法
        /// </summary>
        /// <param name="invocation">拦截的一些上下文信息</param>
        void Intercept(IInvocation invocation);

        /// <summary>
        /// 拦截器的执行顺序
        /// </summary>
        int Order { get; }

    }



}
