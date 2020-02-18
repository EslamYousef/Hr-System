using System.Web.Mvc;

namespace HR.Areas.suberAdmin
{
    public class suberAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "suberAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "suberAdmin_default",
                "suberAdmin/{controller}/{action}/{id}",
                new { controller= "AddSpecificListoOfRoles", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}