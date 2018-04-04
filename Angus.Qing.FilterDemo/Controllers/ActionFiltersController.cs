using Angus.Qing.FilterDemo.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.Qing.FilterDemo.Controllers
{
    //Controller本身也有继承接口IActionFilter，所以做ActionFilter时，我们可以写一个Controller的基类，然后所有的Controller都继承该基类，这样的好处就是不需要每一个Action上面，都标记ActionFilter属性
    public class ActionFiltersController : Controller
    {
        //
        // GET: /ActionFilters/
        [ActionFilters]
        public ActionResult Index()
        {
            return View();
        }

        [ActionFilters(ActionName = "Index")] //当前Details Action给了Index的权限
        public ActionResult Details()
        {
            return View();
        }

        [ActionFilters]
        public ActionResult Test()
        {
            return View();
        }
    }
}
