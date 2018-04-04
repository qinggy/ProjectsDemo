using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{

    /// <summary>
    /// 支持 AOP 的导出
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AOPExportAttribute : ExportAttribute
    {
        #region ctor

        public AOPExportAttribute() : base() { this.AOPEnabled = true; }

        public AOPExportAttribute(string contractName) : base(contractName) { this.AOPEnabled = true; }
        public AOPExportAttribute(Type contractType) : base(contractType) { this.AOPEnabled = true; }
        public AOPExportAttribute(string contractName, Type contractType) : base(contractName, contractType) { this.AOPEnabled = true; }

        #endregion


        /// <summary>
        /// 是否启用 AOP
        /// </summary>
        [DefaultValue(true)]
        public bool AOPEnabled { get; set; }

    }
}
