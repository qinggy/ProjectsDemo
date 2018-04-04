using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Hosting
{
    public class AOPCompositionContainer : CompositionContainer
    {
        #region ctor
        public AOPCompositionContainer()
            : base() { }
        public AOPCompositionContainer(params ExportProvider[] providers)
            : base(providers) { }

        public AOPCompositionContainer(ComposablePartCatalog catalog, params ExportProvider[] providers)
            : base(catalog, providers) { }

        public AOPCompositionContainer(ComposablePartCatalog catalog, bool isThreadSafe, params ExportProvider[] providers)
            : base(catalog, isThreadSafe, providers) { }

        #endregion

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            var exports = base.GetExportsCore(definition, atomicComposition);
            return exports.Select(GetAopExportCore);
        }

        protected virtual Export GetAopExportCore(Export export)
        {
            //如果包含AOPEnabled属性且值为True，就重新构造导出对象
            if (!export.Metadata.ContainsKey("AOPEnabled"))
                return export;

            var aspectsEnabled = export.Metadata["AOPEnabled"];

            if ((bool)aspectsEnabled == false)
                return export;

            return new Export(export.Definition, () => Aop.ProxyGenerator.CreateProxy(this, export.Value));
        }

    }

}
