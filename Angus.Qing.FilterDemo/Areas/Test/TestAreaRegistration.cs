using System.Web.Mvc;

namespace Angus.Qing.FilterDemo.Areas.Test
{
    public class TestAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Test";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Test_default",
                "Test/{controller}/{action}/{id}",
                new { area = "Test", action = "Index", id = UrlParameter.Optional },
                new { },
                new string[] { "Test" }
            );
        }
    }
}
