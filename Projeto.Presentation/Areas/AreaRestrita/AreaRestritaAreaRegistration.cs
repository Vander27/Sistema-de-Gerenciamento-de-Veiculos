using System.Web.Mvc;

namespace Projeto.Presentation.Areas.AreaRestrita
{
    public class AreaRestritaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AreaRestrita";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AreaRestrita_default",
                "AreaRestrita/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}