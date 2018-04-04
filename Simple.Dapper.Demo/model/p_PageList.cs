using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Dapper.Demo.model
{
    public class p_PageList<T> where T : class, new()
    {
        public string Tables { get; set; }
        public string Fields { get; set; }
        public string OrderFields { get; set; }
        public string Where { get; set; }
        public int PageIndex { get; set; }
        private int _pagesize = 20;
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        public string GroupBy { get; set; }
        public int TotalCount { get; set; }
        public int PageCount
        {
            get { return (int)Math.Ceiling(TotalCount / (double)PageSize); }
        }
        public IEnumerable<T> DataList { get; set; }
    }
}
