using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Inspinia_MVC5.Tags;
using Inspinia_MVC5.Extencion;

namespace Inspinia_MVC5.Controllers
{
    [PermisoAttribute()]
    public class SolicitudMantenimientoController : Controller
    {
        private Contexto db = new Contexto();

        [PermisoAttribute(Accion = "Index", Controlador = "SolicitudMantenimiento")]
        public async Task<ActionResult> Index()
        {
            Usuario usu = (Usuario)Session["Usuario"];

            if (usu.EsChofer)
            {
                var solicitudmantenimiento = db.SolicitudMantenimiento.Where(x => x.Activo == true && x.Desaprobado == false && x.IdChofer == usu.IdUsuario).Include(s => s.Usuario).Include(s => s.Vehiculo);
                return View(await solicitudmantenimiento.ToListAsync());
            }
            else
            {
                var solicitudmantenimiento = db.SolicitudMantenimiento.Where(x => x.Activo == true && x.Desaprobado == false).Include(s => s.Usuario).Include(s => s.Vehiculo);
                return View(await solicitudmantenimiento.ToListAsync());
            }        
        }

        [PermisoAttribute(Accion = "Create", Controlador = "SolicitudMantenimiento")]
        public ActionResult Create(SolicitudMantenimiento solicitudMantenimiento = null, bool xyz = false)
        {

            Usuario usu = (Usuario)Session["Usuario"];

            ViewBag.IdChofer = new SelectList(db.Usuario.Where(x=>x.IdUsuario == usu.IdUsuario), "IdUsuario", "Nombres");
            ViewBag.IdVehiculo = new SelectList(db.VehiculoChofer.Where(x=>x.IdChofer == usu.IdUsuario && x.Activo == true)
                                                                .Select(x => new { idVehiculo = x.Vehiculo.idVehiculo, placa = x.Vehiculo.placaLetras + x.Vehiculo.placaNumeros }), "idVehiculo", "placa");
            if (solicitudMantenimiento.TipoMantenimiento == null)
            {
                solicitudMantenimiento.TipoMantenimiento = "MANPRE";
            }

            ViewBag.TipoMantenimiento = new SelectList(db.Parametros.Where(x => x.codigo == "MANPRE" || x.codigo == "MANCOR" && x.activo == true)
                                                                    .Select(x => new { codigo = x.codigo, valor = x.valor_cadena_1 }),"codigo","valor");
            string[] subMan = db.Parametros.Where(x => x.codigo == solicitudMantenimiento.TipoMantenimiento && x.activo == true).Select(x => x.valor_cadena_2).FirstOrDefault().Split(';');
            ViewBag.SubTipoMantimiento = new SelectList(subMan.Select((r, index) => new { Text = r, Value = index }), "Text", "Text");

            if (solicitudMantenimiento.FechaIngreso < DateTime.Today.AddYears(-1000))
            {
                solicitudMantenimiento.FechaIngreso = DateTime.Today;
                solicitudMantenimiento.FechaEstimadaSalida = DateTime.Today.AddDays(1);
            }

           return View(solicitudMantenimiento);
            
        }

        [HttpPost]
        public ActionResult ComboMantenimiento(SolicitudMantenimiento solicitudMantenimiento)
        {
            return RedirectToAction("Create", solicitudMantenimiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SolicitudMantenimiento solicitudMantenimiento)
        {
            if (ModelState.IsValid)
            {
                solicitudMantenimiento.Activo = true;
                db.SolicitudMantenimiento.Add(solicitudMantenimiento);
                await db.SaveChangesAsync();

                /*envio de correo*/
                string xml = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Extencion/PlantillasCorreo/PlantillaSolicitudMantenimiento.html")));
                var html = xml.ToString();

                db = new Contexto();

                Parametros parametro = db.Parametros.First(x => x.codigo == "CORREOADMI" && x.activo == true);

                Usuario chofer = db.Usuario.First(x => x.IdUsuario == solicitudMantenimiento.IdChofer);
                Vehiculo vehiculo = db.Vehiculo.First(x => x.idVehiculo == solicitudMantenimiento.IdVehiculo);

                html = html.Replace("@chofer", chofer.Nombres + "  " + chofer.Apellidos)
                           .Replace("@vehiculo", vehiculo.VehiculoMarca.nombre + " - " + vehiculo.VehiculoTipo.nombre + " - " + vehiculo.modelo)
                           .Replace("@tipo", solicitudMantenimiento.TipoMantenimiento)
                           .Replace("@detalle", solicitudMantenimiento.Detalle)
                           .Replace("@fechaingreso", solicitudMantenimiento.FechaIngreso.ToShortDateString())
                           .Replace("@fechasalida", solicitudMantenimiento.FechaEstimadaSalida.ToShortDateString())
                           .Replace("@nombres", parametro.valor_cadena_1);

                Correo.EnviarCorreoGmail(parametro.valor_cadena_2, "Solicitud de Mantenimiento", html);
                /**/

                return RedirectToAction("Index");
            }

            Usuario usu = (Usuario)Session["Usuario"];
            ViewBag.IdChofer = new SelectList(db.Usuario.Where(x => x.IdUsuario == usu.IdUsuario), "IdUsuario", "Nombres");
            ViewBag.IdVehiculo = new SelectList(db.VehiculoChofer.Where(x => x.IdChofer == usu.IdUsuario && x.Activo == true).Select(x => new { idVehiculo = x.Vehiculo.idVehiculo, placa = x.Vehiculo.placaLetras + x.Vehiculo.placaNumeros }), "idVehiculo", "placa");

            ViewBag.TipoMantenimiento = new SelectList(db.Parametros.Where(x => x.codigo == "MANPRE" || x.codigo == "MANCOR" && x.activo == true)
                                                                    .Select(x => new { codigo = x.codigo, valor = x.valor_cadena_1 }), "codigo", "valor", solicitudMantenimiento.TipoMantenimiento);
            string[] subMan = db.Parametros.Where(x => x.codigo == solicitudMantenimiento.TipoMantenimiento && x.activo == true).Select(x => x.valor_cadena_2).FirstOrDefault().Split(';');
            ViewBag.SubTipoMantimiento = new SelectList(subMan.Select((r, index) => new { Text = r, Value = index }), "Text", "Text", solicitudMantenimiento.SubTipoMantimiento);

            return View(solicitudMantenimiento);
        }

        [PermisoAttribute(Accion = "Edit", Controlador = "SolicitudMantenimiento")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudMantenimiento solicitudMantenimiento = await db.SolicitudMantenimiento.FindAsync(id);
            if (solicitudMantenimiento == null)
            {
                return HttpNotFound();
            }
            Usuario usu = (Usuario)Session["Usuario"];
            ViewBag.IdChofer = new SelectList(db.Usuario.Where(x => x.IdUsuario == usu.IdUsuario), "IdUsuario", "Nombres");
            ViewBag.IdVehiculo = new SelectList(db.VehiculoChofer.Where(x => x.IdChofer == usu.IdUsuario && x.Activo == true).Select(x => new { idVehiculo = x.Vehiculo.idVehiculo, placa = x.Vehiculo.placaLetras + x.Vehiculo.placaNumeros }), "idVehiculo", "placa");
            return View(solicitudMantenimiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdSolicitud,IdVehiculo,IdChofer,TipoMantenimiento,Detalle,FechaIngreso,FechaEstimadaSalida,Aprobado,Activo")] SolicitudMantenimiento solicitudMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudMantenimiento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            Usuario usu = (Usuario)Session["Usuario"];

            ViewBag.IdChofer = new SelectList(db.Usuario.Where(x => x.IdUsuario == usu.IdUsuario), "IdUsuario", "Nombres");
            ViewBag.IdVehiculo = new SelectList(db.VehiculoChofer.Where(x => x.IdChofer == usu.IdUsuario && x.Activo == true).Select(x => new { idVehiculo = x.Vehiculo.idVehiculo, placa = x.Vehiculo.placaLetras + x.Vehiculo.placaNumeros }), "idVehiculo", "placa");
            return View(solicitudMantenimiento);
        }

        [PermisoAttribute(Accion = "Delete", Controlador = "SolicitudMantenimiento")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudMantenimiento solicitudMantenimiento = await db.SolicitudMantenimiento.FindAsync(id);
            if (solicitudMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(solicitudMantenimiento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SolicitudMantenimiento solicitudMantenimiento = await db.SolicitudMantenimiento.FindAsync(id);
            //db.SolicitudMantenimiento.Remove(solicitudMantenimiento);
            solicitudMantenimiento.Activo = false;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [PermisoAttribute(Accion = "Aprobar", Controlador = "SolicitudMantenimiento")]
        public async Task<ActionResult> Aprobar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudMantenimiento solicitudMantenimiento = await db.SolicitudMantenimiento.FindAsync(id);
            if (solicitudMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(solicitudMantenimiento);
        }

        [HttpPost, ActionName("Aprobar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AprobarConfirmed(int id)
        {
            SolicitudMantenimiento solicitudMantenimiento = await db.SolicitudMantenimiento.FindAsync(id);
            //db.SolicitudMantenimiento.Remove(solicitudMantenimiento);
            solicitudMantenimiento.Aprobado = true;
            await db.SaveChangesAsync();

            EnviarCorreoSolicitud(solicitudMantenimiento, "Aprobado");

            return RedirectToAction("Index");
        }

        [PermisoAttribute(Accion = "Desaprobar", Controlador = "SolicitudMantenimiento")]
        public async Task<ActionResult> Desaprobar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudMantenimiento solicitudMantenimiento = await db.SolicitudMantenimiento.FindAsync(id);
            if (solicitudMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(solicitudMantenimiento);
        }

        [HttpPost, ActionName("Desaprobar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DesaprobarConfirmed(int id)
        {
            SolicitudMantenimiento solicitudMantenimiento = await db.SolicitudMantenimiento.FindAsync(id);
            //db.SolicitudMantenimiento.Remove(solicitudMantenimiento);
            solicitudMantenimiento.Desaprobado = true;
            await db.SaveChangesAsync();

            EnviarCorreoSolicitud(solicitudMantenimiento, "Rechazado");

            return RedirectToAction("Index");
        }

        void EnviarCorreoSolicitud(SolicitudMantenimiento solicitudMantenimiento, string respuesta) {
            try
            {
                /*envio de correo*/
                string xml = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Extencion/PlantillasCorreo/PlantillaSolMan_Apro_Rech.html")));
                var html = xml.ToString();

                db = new Contexto();

                Usuario chofer = db.Usuario.First(x => x.IdUsuario == solicitudMantenimiento.IdChofer);
                Vehiculo vehiculo = db.Vehiculo.First(x => x.idVehiculo == solicitudMantenimiento.IdVehiculo);

                html = html.Replace("@vehiculo", vehiculo.VehiculoMarca.nombre + " - " + vehiculo.VehiculoTipo.nombre + " - " + vehiculo.modelo)
                           .Replace("@tipo", solicitudMantenimiento.TipoMantenimiento)
                           .Replace("@detalle", solicitudMantenimiento.Detalle)
                           .Replace("@fechaingreso", solicitudMantenimiento.FechaIngreso.ToShortDateString())
                           .Replace("@fechasalida", solicitudMantenimiento.FechaEstimadaSalida.ToShortDateString())
                           .Replace("@nombres", chofer.Nombres + "  " + chofer.Apellidos)
                           .Replace("@estado", respuesta);

                Correo.EnviarCorreoGmail(chofer.Correo, "Respuesta Solicitud de Mantenimiento", html);
                /**/
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
