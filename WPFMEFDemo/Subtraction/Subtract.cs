using CalculatorContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subtraction
{
    [ExportMetadata("CalciMetaData", "Sub"), Export(typeof(ICalculator))]
    public class Subtract : ICalculator
    {
        public Subtract()
        {

        }

        public int GetNumber(int num1, int num2)
        {
            return num1 - num2;
        }
    }
}
