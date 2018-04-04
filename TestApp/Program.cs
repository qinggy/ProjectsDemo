using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex reg = new Regex(@"P\d+", RegexOptions.IgnoreCase);
            string a = "(P25+p4)*P5";
            var vitualIdList = reg.Matches(a);
            if (vitualIdList != null)
            {
                for (int i = 0; i < vitualIdList.Count; i++)
                {

                }
            }


        }
    }
}
