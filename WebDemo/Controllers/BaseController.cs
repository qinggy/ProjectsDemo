using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //从cookie中获取咱们的 登录的sessionId
            string sessionId = Request["sessionId"];
            if (string.IsNullOrEmpty(sessionId))
            {
                //return RedirectToAction("Login", "Logon");
                Response.Redirect("/Logon/Index");
            }

            object obj = MemcacheHelper.Get(sessionId);
            UserInfo user = obj as UserInfo;
            if (user == null)
            {
                Response.Redirect("/Logon/Index");
            }

            LoginUser = user;
            MemcacheHelper.Set(sessionId, user, DateTime.Now.AddMinutes(20));
        }
       
    }
}
