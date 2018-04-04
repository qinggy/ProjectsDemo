using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.Qing.FilterDemo.Extension
{
    public class ExceptionFilters : HandleErrorAttribute
    {
        /// <summary>
        /// 发生在异常时调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                //获取当前的Controller和Action
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                //创建一个HandleErrorInfo，用于记录异常信息，展示在ExceptionDetail页面
                HandleErrorInfo errorInfo = new HandleErrorInfo(filterContext.Exception, controller, action);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(errorInfo) //设置异常记录ViewData
                };
                filterContext.ExceptionHandled = true;
            }


            //base.OnException(filterContext);
        }
    }
}