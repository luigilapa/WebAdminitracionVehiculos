using Inspinia_MVC5.Tags;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    [PermisoAttribute()]
    public class ErrorController : Controller
    {
        public ActionResult Error403()
        {
            return View();
        }
    }
}