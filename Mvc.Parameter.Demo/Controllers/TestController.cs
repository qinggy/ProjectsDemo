using Mvc.Parameter.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Parameter.Demo.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveSingleEntity(UserLogin userLogin)
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult SaveMultiEntity(UserLogin userLogin, Contact contact)
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult SaveMultiListEntity(List<UserLogin> userLogin, Contact contact)
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult SaveMultiListEntities(List<UserLogin> userLogin, List<Contact> contact)
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult SaveDoubleEntity(List<UserLogin> userLogin, List<Contact> contact, string code)
        {
            return Json("");
        }
    }
}
