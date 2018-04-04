using Angus.Qing.FilterDemo.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Angus.Qing.FilterDemo.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model)
        {
            if (model.UserName.Trim() == model.Password.Trim()) //伪代码
            {
                //if (model.RememberMe)
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, true); //2880分钟有效的Cookie
                //}
                //else
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, false);//当前会话的Cookie
                //}
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                return RedirectToAction("Welcome", "AuthFilters");
            }
            else
                return View(model);
        }

        public ActionResult LogOut()
        {
            return View("LogOn");
        }
    }
}
