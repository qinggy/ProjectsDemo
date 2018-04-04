using Angus.Qing.FilterDemo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.Qing.FilterDemo.Extension
{
    /// <summary>
    /// Action权限过滤
    /// </summary>
    public class ActionFilters : ActionFilterAttribute
    {
        public string ActionName { get; set; } //当前请求的Action名称

        public string Roles { get; set; } //可以访问当前Action的角色列表，

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string userName = filterContext.HttpContext.User.Identity.Name;
            User user = SampleData.users.Find(a => a.UserName == userName);
            if (user != null)
            {
                //string controller = filterContext.RouteData.Values["controller"];
                string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string area = filterContext.RouteData.Values["area"].ToString();
                if (string.IsNullOrEmpty(ActionName))
                {
                    ActionName = filterContext.ActionDescriptor.ActionName;
                }

                var roleWithControllerAction = SampleData.roleWithControllerAction.Find(a => a.ActionName == ActionName && a.ControllerName == controller);
                if (roleWithControllerAction != null)
                {
                    Roles = roleWithControllerAction.RoleIds;
                }
                if (!string.IsNullOrEmpty(Roles))
                {
                    foreach (string roleid in Roles.Split(','))
                    {
                        if (user.RoleId.ToString() == roleid)
                            return;   //有权限
                    }
                }
                filterContext.Result = new EmptyResult();//请求失败，输出空结果
                HttpContext.Current.Response.Write("对不起，请没有权限！");
            }
            else
            {
                //filterContext.Result = new ViewResult { ViewName = "/Account/LogOn", MasterName = "~/Views/Shared/_Layout" };
                filterContext.Result = new EmptyResult();//请求失败，输出空结果
                HttpContext.Current.Response.Write("对不起，您没有权限！");
            }
            //base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}