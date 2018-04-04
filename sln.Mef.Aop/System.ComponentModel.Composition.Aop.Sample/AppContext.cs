using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample
{
    /// <summary>
    /// 当前整个应用的上下文
    /// </summary>
    sealed class AppContext
    {
        private static readonly AppContext _current = new AppContext();
        public static AppContext Current { get { return _current; } }

        /// <summary>
        /// 执行该应用的用户，登录用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

    }
}
