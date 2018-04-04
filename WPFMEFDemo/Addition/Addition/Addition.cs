using CalculatorContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition
{
    [Export(typeof(ICalculator))]
    [ExportMetadata("CalciMetaData", "Add")]
    public class Add : ICalculator
    {
        public Add()
        {

        }

        public int GetNumber(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
