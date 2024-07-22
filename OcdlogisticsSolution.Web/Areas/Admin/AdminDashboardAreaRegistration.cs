using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard
{
    public class AdminDashboardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminDashboard_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller="Panel", id = UrlParameter.Optional }
            );
        }
    }
}