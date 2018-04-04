using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redis.Demo.SimpleBlogSystemDemo
{
    public class BlogPostComment
    {
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
