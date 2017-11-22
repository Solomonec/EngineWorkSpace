using System.Web.Mvc;

namespace MetroSupport.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new {controller="Users", action = "IT", id = UrlParameter.Optional }
            );
        }
    }
}
