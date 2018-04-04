using System.Web;
using System.Web.Mvc;

namespace IS.Esd.VersionManager.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}