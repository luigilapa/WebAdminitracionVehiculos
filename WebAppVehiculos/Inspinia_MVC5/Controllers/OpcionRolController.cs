using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Modelo;
using Inspinia_MVC5.Tags;

namespace Inspinia_MVC5.Controllers
{
    [PermisoAttribute()]
    public class OpcionRolController : Controller
    {
        private Contexto db = new Contexto();

        [PermisoAttribute(Accion = "Permisos", Controlador = "OpcionRol")]
        public ActionResult Permisos(int id, string modulo = "")
        {

            ViewBag.IdRol = id;
            ViewBag.Rol = db.Rol.FirstOrDefault(x => x.IdRol == id).NombreRol;

            if (modulo == string.Empty)
            {
                var primermodulo = db.Opcion.Where(x => x.activo == true).OrderBy(x => x.modulo).First();
                modulo = primermodulo != null ? primermodulo.modulo : "";
            }
            ViewBag.Modulo = modulo;

            ViewBag.FiltroModulo = new SelectList(db.Opcion.Where(x => x.activo == true).Select(x=> new { x.modulo }).Distinct().OrderBy(x => x.modulo), "modulo", "modulo", modulo);

            var permisos = db.OpcionRol.Where(x => x.IdRol == id && x.Activo == true && x.Opcion.modulo == modulo)
                                       .Include(o => o.Opcion)
                                       .Include(r => r.Rol)
                                       .OrderBy(x => x.Opcion.modulo)
                                       .ThenBy(x => x.Opcion.controlador)
                                       .ThenBy(x => x.Opcion.accion);

            return View(permisos);
        }

        [PermisoAttribute(Accion = "CambiarPermiso", Controlador = "OpcionRol")]
        public ActionResult CambiarPermiso(int id, string modulo = "")
        {
            OpcionRol permiso = db.OpcionRol.FirstOrDefault(x => x.IdOpcionRol == id);

            if (permiso != null) {
                permiso.TienePermiso = !permiso.TienePermiso;
                db.SaveChanges();
            }

            return RedirectToAction("Permisos",new { id = permiso.IdRol, modulo = modulo });
        }

  
        //List<OpcionExt> ObtenerOpciones(int idRol) {
        //    List<OpcionExt> opciones = new List<OpcionExt>();
        //    db.Opcion.Where(x => x.activo == true).ToList()
        //                                          .ForEach(x => opciones.Add(new OpcionExt {
        //                                              Id = x.idOpcion,
        //                                              Controlador = x.controlador,
        //                                              Accion = x.accion,
        //                                              Bloqueo = db.OpcionRol.FirstOrDefault(y => y.IdRol == idRol && y.IdOpcion == x.idOpcion) != null ? db.OpcionRol.FirstOrDefault(y => y.IdRol == idRol && y.IdOpcion == x.idOpcion).Activo : false  
        //                                          }));

        //    return opciones;
        //}
    }
}