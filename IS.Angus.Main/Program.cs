using IS.Angus.IService;
using IS.Angus.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IS.Angus.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //IFly fly = new BirdFly();
            //fly.Fly();

            //fly = new PlaneFly();
            //fly.Fly();

            //Console.ReadKey();

            AggregateCatalog catalog = new AggregateCatalog();
            DirectoryCatalog directory = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
            //catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetAssembly(typeof(IFly))));
            catalog.Catalogs.Add(directory);
            CompositionContainer container = new CompositionContainer(catalog);
            //container.SatisfyImportsOnce(new Program());

            foreach (var item in container.GetExportedValues<Action>())
            {
                Console.WriteLine("Before Action");
                item();
                Console.WriteLine("End Action");
            }
            //foreach (var item in container.GetExports<IFlyMetadata>())
            //{
            //    //item.Fly();
            //    //item.Value.
            //}

            Console.ReadKey();
        }
    }
}
