using Inspinia_MVC5.Tags;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    [PermisoAttribute()]
    public class ReportesController : Controller
    {
        [PermisoAttribute(Accion = "Choferes", Controlador = "Reportes")]
        public ActionResult Choferes()
        {
            return Redirect("/Reporteria/Paginas/ReporteChoferes.aspx");
        }

        [PermisoAttribute(Accion = "Vehiculos", Controlador = "Reportes")]
        public ActionResult Vehiculos()
        {
            return Redirect("/Reporteria/Paginas/ReporteVehiculos.aspx");
        }

        [PermisoAttribute(Accion = "VehiculoChofer", Controlador = "Reportes")]
        public ActionResult VehiculoChofer()
        {
            return Redirect("/Reporteria/Paginas/ReporteVehiculoChofer.aspx");
        }


        [PermisoAttribute(Accion = "RegistroActiviades", Controlador = "Reportes")]
        public ActionResult RegistroActiviades()
        {
            return Redirect("/Reporteria/Paginas/ReporteActividades.aspx");
        }

        [PermisoAttribute(Accion = "SolicitudMantenimiento", Controlador = "Reportes")]
        public ActionResult SolicitudMantenimiento()
        {
            return Redirect("/Reporteria/Paginas/ReporteSolicitudMantenimiento.aspx");
        }
    }
}