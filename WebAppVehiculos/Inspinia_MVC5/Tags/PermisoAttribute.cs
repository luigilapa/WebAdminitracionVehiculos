using System.Web.Mvc;
using System.Web.Routing;

namespace Inspinia_MVC5.Tags
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        //public RolesPermisos Permiso { get; set; }

        public string Controlador { get; set; }
        public string Accion { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (Controlador != null && Accion != null)
            {
                if (!FrontUser.TienePermiso(Controlador, Accion))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Error",
                        action = "Error403"
                    }));
                }
            }
            else
            {
                if (!FrontUser.EstaLogiado())
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Sesion",
                        action = "Autenticar"
                    }));
                }
            } 
           
        }
    }
}