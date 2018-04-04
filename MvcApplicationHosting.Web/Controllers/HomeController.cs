using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplicationHosting.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult List()
        {
            TestDataContext _obj = new TestDataContext();
            var _res = from r in _obj.Test select r;

            return Json(_res);
        }


    }
}
