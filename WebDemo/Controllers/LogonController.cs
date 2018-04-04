using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class LogonController : Controller
    {
        //
        // GET: /Logon/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(UserInfo user)
        {
            SchoolDbContext dbContext =new SchoolDbContext();
            
            var loginUser = dbContext.UserInfo.Where(u => u.UName.Equals(user.UName) && u.UPwd.Equals(user.UPwd)).FirstOrDefault();

            if (loginUser == null)
            {
                return Content("用户名密码错误！");
            }
            else
            {
                Guid sessionId = Guid.NewGuid();//申请了一个模拟的GUID：SessionId

                //把sessionid写到客户端浏览器里面去累
                Response.Cookies["sessionId"].Value = sessionId.ToString();
                
                //
                MemcacheHelper.Set(sessionId.ToString(), loginUser, DateTime.Now.AddMinutes(20));

                //用户登录成功之后要保存用户的登录的数据：

                //Session["loginUser"] = loginUser;
                return Content("ok");
            }

        }


        public ActionResult ValidateCode()
        {
         ValidateCodeHelper helper =new ValidateCodeHelper();
            string strCode = helper.CreateValidateCode(4);
            Session["validateCode"] = strCode;

            var byteData = helper.CreateValidateGraphic(strCode);
            return File(byteData, "image/jpeg");
        }
    }
}
