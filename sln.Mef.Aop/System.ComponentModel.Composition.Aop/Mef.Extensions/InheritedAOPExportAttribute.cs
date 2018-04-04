using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{

    /// <summary>
    /// 支持 AOP 的导出，且子类也继承该设定 
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class InheritedAOPExportAttribute : InheritedExportAttribute
    {
        #region ctor

        public InheritedAOPExportAttribute() : base() { this.AOPEnabled = true; }
        public InheritedAOPExportAttribute(string contractName) : base(contractName) { this.AOPEnabled = true; }
        public InheritedAOPExportAttribute(Type contractType) : base(contractType) { this.AOPEnabled = true; }
        public InheritedAOPExportAttribute(string contractName, Type contractType) : base(contractName, contractType) { this.AOPEnabled = true; }

        #endregion

        /// <summary>
        /// 是否启用 AOP
        /// </summary>
        [DefaultValue(true)]
        public bool AOPEnabled { get; set; }
    }
}
