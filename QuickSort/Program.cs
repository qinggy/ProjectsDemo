using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入待排序数列(以\",\"分割):");
            string _s = Console.ReadLine();
            string[] _sArray = _s.Split(",".ToCharArray());
            int _nLength = _sArray.Length;
            int[] _nArray = new int[_nLength];
            for (int i = 0; i < _nLength; i++)
            {
                _nArray[i] = Convert.ToInt32(_sArray[i]);
            }
            QuickSort(_nArray, 0, _nLength - 1);
            Console.WriteLine("排序后的数列为:");
            string str = "";
            foreach (int _n in _nArray)
            {
                str += "," + _n;
            }
            Console.WriteLine("排序后结果为：" + str.Substring(1));
            Console.ReadKey();
        }

        public static void QuickSort(int[] _pnArray, int _pnLow, int _pnHigh)
        {
            if (_pnLow >= _pnHigh) return;

            int _nPivotIndex = QuickSort_Once(_pnArray, _pnLow, _pnHigh);
            QuickSort(_pnArray, _pnLow, _nPivotIndex - 1);
            QuickSort(_pnArray, _nPivotIndex + 1, _pnHigh);
        }

        private static int QuickSort_Once(int[] _pnArray, int _pnLow, int _pnHigh)
        {
            int nPivot = _pnArray[_pnLow];
            int i = _pnLow, j = _pnHigh;

            while (i < j)
            {
                while (_pnArray[j] >= nPivot && i < j) j--;
                _pnArray[i] = _pnArray[j];
                while (_pnArray[i] <= nPivot && i < j) i++;
                _pnArray[j] = _pnArray[i];
            }

            string str = "";
            foreach (int _n in _pnArray)
            {
                str += "," + _n;
            }
            Console.WriteLine("排序后结果为：" + str.Substring(1));
            _pnArray[i] = nPivot;

            return i;
        }
    }
}
