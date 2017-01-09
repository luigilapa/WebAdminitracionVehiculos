using Inspinia_MVC5.Extencion;
using Inspinia_MVC5.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    [PermisoAttribute()]
    public class InicioController : Controller
    {
        //[PermisoAttribute(Accion ="Index",Controlador ="Inicio")]
        public ActionResult Index()
        {
            //Correo.SendEmailOutlook();
            return View();
        }
    }
}