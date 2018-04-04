using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample
{
    // 一个简单的 ServiceLocator
    static class ServiceLocator
    {
        private static readonly AOPCompositionContainer _container;
        static ServiceLocator()
        {
            var catalog = new AssemblyCatalog(typeof(ServiceLocator).Assembly);
            _container = new AOPCompositionContainer(catalog);
        }

        public static TService GetService<TService>()
        {
            return _container.GetExportedValue<TService>();
        }

        public static IEnumerable<TService> GetServices<TService>()
        {
            return _container.GetExportedValues<TService>();
        }
    }
}
