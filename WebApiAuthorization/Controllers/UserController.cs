using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebApiAuthorization.Models;

namespace WebApiAuthorization.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public object Login(string strUser, string strPwd)
        {
            if (!ValidateUser(strUser, strPwd))
            {
                return new { bRes = false };
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0,
                strUser,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                true,
                string.Format("{0}&{1}", strUser, strPwd),
                FormsAuthentication.FormsCookiePath);
            var userInfo = new UserInfo { bRes = true, UserName = strUser, Password = strPwd, Ticket = FormsAuthentication.Encrypt(ticket) };
            HttpContext.Current.Session[strUser] = userInfo;

            return userInfo;
        }

        private bool ValidateUser(string strUser, string strPwd)
        {
            if (strUser == "admin" && strPwd == "123456")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
