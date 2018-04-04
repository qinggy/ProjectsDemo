using Angus.Qing.FilterDemo.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.Qing.FilterDemo.Controllers
{
    public class ExceptionController : Controller
    {
        //
        // GET: /Exception/
        [ExceptionFilters(View = "Exception")]
        public ActionResult Index()
        {
            throw new NullReferenceException("测试抛出异常！");
        }

        [ExceptionFilters(View = "ExceptionDetail")]
        public ActionResult Details()
        {
            int id = int.Parse("Hello Word!");
            return View();
        }
    }
}
