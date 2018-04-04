using CalculatorContract;
using System.ComponentModel.Composition;

namespace CompositionHelper
{
    [Export(typeof(ICalculator))]
    [ExportMetadata("CalciMetaData", "Div")]
    public class Divide : ICalculator
    {
        public int GetNumber(int num1, int num2)
        {
            return num1 / num2;
        }
    }
}
