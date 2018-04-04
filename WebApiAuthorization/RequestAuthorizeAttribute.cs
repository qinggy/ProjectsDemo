using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace WebApiAuthorization
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //从http请求Header中获取验证凭证
            var authorization = actionContext.Request.Headers.Authorization;
            if (authorization != null && authorization.Parameter != null)
            {
                // 获取加密后的用户登录凭证
                var encryptTicket = authorization.Parameter;
                if (ValidateTicket(encryptTicket))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }

            }
            //如果无法取到认证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }

        }

        private bool ValidateTicket(string encryptTicket)
        {
            //解密用户数据
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;

            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);

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