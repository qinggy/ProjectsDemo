using Angus.Qing.FilterDemo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.Qing.FilterDemo.Extension
{
    public class UserAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// 授权失败时呈现的试图
        /// </summary>
        public string AuthorizationFailView { get; set; }

        /// <summary>
        /// 请求授权时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //string controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string actionName = filterContext.RouteData.Values["action"].ToString().ToLower();
            string action = filterContext.ActionDescriptor.ActionName;
            var roleWithControllerAction = SampleData.roleWithControllerAction.Find(a => a.ControllerName == controller && a.ActionName == action);
            if (roleWithControllerAction != null)
            {
                this.Roles = roleWithControllerAction.RoleIds;
            }

            base.OnAuthorization(filterContext); //进入AuthorizeCore
        }

        /// <summary>
        /// 自定义授权检查（返回False则授权失败）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                string userName = httpContext.User.Identity.Name;//此处设计不佳，因为一般系统中，用户名并不一定是唯一的，所以我们可以设计成成Session中获取当前用户的UserId
                User user = SampleData.users.Find(a => a.UserName == userName);
                if (user != null)
                {
                    foreach (var roleId in Roles.Split(','))
                    {
                        if (roleId == user.RoleId.ToString())
                        {
                            return true;
                        }
                    }

                    return false;
                }
                else
                    return false; //验证不通过，返回false;
            }
            else
                return false; //验证不通过，返回false；//进入HandleUnauthorizedRequest
        }

        /// <summary>
        /// 处理授权失败的Http请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new ViewResult { ViewName = AuthorizationFailView };
            //filterContext.Result = new RedirectResult("/Admin/Dashboard");
        }
    }
}