using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Demo.Demo
{
    public class Question
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public List<string> Tags { get; set; }
    }
}
