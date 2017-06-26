using System.Web.Mvc;

namespace MVC.Areas.Guest
{
    public class GuestAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Guest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Guest_default",
                "Guest/{controller}/{action}/{id}",
                new { action = "Index",Controller="Home", id = UrlParameter.Optional },
                  new[] { "Mvc.Areas.Guest.Controllers" }
            );
        }
    }
}
