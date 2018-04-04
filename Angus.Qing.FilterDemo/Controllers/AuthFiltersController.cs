using Angus.Qing.FilterDemo.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.Qing.FilterDemo.Controllers
{
    public class AuthFiltersController : Controller
    {
        //
        // GET: /AuthFilters/

        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Users = "a,b,c")]
        [Authorize]
        public ActionResult Welcome()
        {
            ViewBag.Message = "普通已授权页面";
            return View();
        }

        [UserAuthorize(AuthorizationFailView = "Error")] //管理员页面
        public ActionResult AdminUser()
        {
            ViewBag.Message = "管理员页面";
            return View("Welcome");
        }

        [UserAuthorize(AuthorizationFailView = "Error")] //会员页面（管理员、会员都可访问）
        public ActionResult SeniorUser()
        {
            ViewBag.Message = "高级会员页面";
            return View("Welcome");
        }

        [UserAuthorize(AuthorizationFailView = "Error")]
        public ActionResult JuniorUser()  //游客页面（管理员、会员、游客都可访问）
        {
            ViewBag.Message = "初级会员页面";
            return View("Welcome");
        }
    }
}
