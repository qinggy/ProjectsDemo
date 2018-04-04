using System.Web;
using System.Web.Mvc;

namespace Angus.qing.BaseWebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}