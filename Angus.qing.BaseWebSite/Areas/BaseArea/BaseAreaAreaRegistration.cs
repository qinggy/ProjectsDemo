using System.Web.Mvc;

namespace Angus.qing.BaseWebSite.Areas.BaseArea
{
    public class BaseAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BaseArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BaseArea_default",
                "BaseArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
