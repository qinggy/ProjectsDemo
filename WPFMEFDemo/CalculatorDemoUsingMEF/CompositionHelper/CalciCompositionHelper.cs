using CalculatorContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CompositionHelper
{
    public class CalciCompositionHelper
    {
        [ImportMany]
        public System.Lazy<ICalculator, IDictionary<string, object>>[] CalciPlugins { get; set; }

        /// <summary>
        /// Assembles the Calculator Components
        /// </summary>
        public void AssembleCalculatorComponents()
        {
            try
            {
                /*
                //Step 1: Initializes a new instance of the 
                //System.ComponentModel.Composition.Hosting.AssemblyCatalog  
                //class with the current executing assembly.
                var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

                //Step 2: The assembly obtained in step 1 are added to the CompositionContainer
                var container = new CompositionContainer(catalog);

                //Step 3: Composable parts are created here 
                //the Import and Export components
                //assembles here
                container.ComposeParts(this);
                */

                //Creating an instance of aggregate catalog. It aggregates other catalogs
                var aggregateCatalog = new AggregateCatalog();

                //Build the directory path where the parts will be available
                var directoryPath =
                    string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                                .Split('\\').Reverse().Skip(3).Reverse().Aggregate((a, b) => a + "\\" + b)
                                , "\\", "ExportComponents\\components");

                //Load parts from the available dlls in the specified path using the directory catalog
                var directoryCatalog = new DirectoryCatalog(directoryPath, "*.dll");

                //Load parts from the current assembly if available
                var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

                //Add to the aggregate catalog
                aggregateCatalog.Catalogs.Add(directoryCatalog);
                aggregateCatalog.Catalogs.Add(asmCatalog);

                //Crete the composition container
                var container = new CompositionContainer(aggregateCatalog);

                // Composable parts are created here i.e. the Import and Export components assembles here
                container.ComposeParts(this);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send the result back to the client
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public int GetResult(int num1, int num2, string operationType)
        {
            int result = 0;
            foreach (var CalciPlugin in CalciPlugins)
            {
                if ((string)CalciPlugin.Metadata["CalciMetaData"] == operationType)
                {
                    result = CalciPlugin.Value.GetNumber(num1, num2);
                    break;
                }
            }

            return result;
        }
    }
}
