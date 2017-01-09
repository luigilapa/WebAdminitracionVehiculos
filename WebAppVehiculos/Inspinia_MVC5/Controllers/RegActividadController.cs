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

namespace Inspinia_MVC5.Controllers
{
    [PermisoAttribute()]
    public class RegActividadController : Controller
    {
        private Contexto db = new Contexto();

        // GET: /RegActividad/
        [PermisoAttribute(Accion = "Index", Controlador = "RegActividad")]
        public async Task<ActionResult> Index()
        {
            Usuario usu = (Usuario)Session["Usuario"];
            var registroactividad = db.RegistroActividad.Where(x=>x.Activo == true && x.IdChofer == usu.IdUsuario).Include(r => r.Usuario).Include(r => r.Vehiculo);
            return View(await registroactividad.OrderBy(x=>x.Fecha).ToListAsync());
        }

        [PermisoAttribute(Accion = "CreateEdit", Controlador = "RegActividad")]
        public ActionResult CreateEdit(int? id = null)
        {
            if (Session["Notificacion"] != null)
            {
                ViewBag.Notificacion = Session["Notificacion"];
                Session["Notificacion"] = null;
            }

            Usuario usu = (Usuario)Session["Usuario"];
            if (id == null)
            {
                ViewBag.IdChofer = new SelectList(db.Usuario.Where(x => x.IdUsuario == usu.IdUsuario), "IdUsuario", "Nombres");
                ViewBag.IdVehiculo = new SelectList(db.VehiculoChofer.Where(x => x.IdChofer == usu.IdUsuario && x.Activo == true).Select(x => new { idVehiculo = x.Vehiculo.idVehiculo, placa = x.Vehiculo.placaLetras + x.Vehiculo.placaNumeros }), "idVehiculo", "placa");
                RegistroActividad nuevo = new RegistroActividad();
                nuevo.Fecha = DateTime.Today;

                return View(nuevo);
            }
            else
            {
                RegistroActividad registroActividad = db.RegistroActividad.Where(x=>x.IdRegistro == id).Include(x=>x.RegistroActividadDetalle).First();
                if (registroActividad == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdChofer = new SelectList(db.Usuario.Where(x => x.IdUsuario == usu.IdUsuario), "IdUsuario", "Nombres", registroActividad.IdChofer);
                ViewBag.IdVehiculo = new SelectList(db.VehiculoChofer.Where(x => x.IdChofer == usu.IdUsuario && x.Activo == true).Select(x => new { idVehiculo = x.Vehiculo.idVehiculo, placa = x.Vehiculo.placaLetras + x.Vehiculo.placaNumeros }), "idVehiculo", "placa", registroActividad.Idvehiculo);
                return View(registroActividad);
            }
        }

        [HttpPost]
        public ActionResult CreateEdit(RegistroActividad registro) {

            if (ModelState.IsValid && ValidarRegistro(registro))
            {
                if (registro.IdRegistro == 0)
                {
                    registro.Activo = true;
                    db.RegistroActividad.Add(registro);
                    db.SaveChanges();
                    return RedirectToAction("CreateEdit", new { id = registro.IdRegistro });
                }
                else
                {
                    RegistroActividad reg = db.RegistroActividad.First(x => x.IdRegistro == registro.IdRegistro);

                    reg.Idvehiculo = registro.Idvehiculo;
                    reg.IdChofer = registro.IdChofer;
                    reg.Fecha = registro.Fecha;
                    reg.KmInicial = registro.KmInicial;
                    reg.KmFinal = registro.KmFinal;

                    db.SaveChanges();
                    return RedirectToAction("CreateEdit", new { id = reg.IdRegistro });
                }
            }
            Usuario usu = (Usuario)Session["Usuario"];

            ViewBag.IdChofer = new SelectList(db.Usuario.Where(x => x.IdUsuario == usu.IdUsuario), "IdUsuario", "Nombres", registro.IdChofer);
            ViewBag.IdVehiculo = new SelectList(db.VehiculoChofer.Where(x => x.IdChofer == usu.IdUsuario && x.Activo == true).Select(x => new { idVehiculo = x.Vehiculo.idVehiculo, placa = x.Vehiculo.placaLetras + x.Vehiculo.placaNumeros }), "idVehiculo", "placa", registro.Idvehiculo);
            return View(registro);

            //return RedirectToAction("Index");
        }

        bool ValidarRegistro(RegistroActividad registro) {

            bool resp = false;

            if (registro.IdRegistro == 0)
            {
                resp = db.RegistroActividad
                    .Where(x => x.IdChofer == registro.IdChofer && 
                                x.Idvehiculo == registro.Idvehiculo && 
                                x.Fecha == registro.Fecha && 
                                x.Activo == true
                                ).Any();
            }
            else
            {
                resp = db.RegistroActividad
                    .Where(x => x.IdChofer == registro.IdChofer && 
                                x.Idvehiculo == registro.Idvehiculo && 
                                x.Fecha == registro.Fecha && 
                                x.Activo == true &&
                                x.IdRegistro != registro.IdRegistro
                                ).Any();
            }

            if (!resp)
            {
                if (registro.KmInicial <= registro.KmFinal)
                {
                    return true;
                }
                else
                {
                    ModelState.AddModelError("KmInicial", "El Km inicial no puede ser mayor al final.");
                    return false;
                }
            }
            else
            {
                ModelState.AddModelError("Fecha", "Ya existe un registro activo con la misma fecha.");
                return false;
            }
        }

        [HttpPost]
        [PermisoAttribute(Accion = "CreateEditDetalle", Controlador = "RegActividad")]
        public ActionResult CreateEditDetalle(RegistroActividadDetalle detalle)
        {
            try
            {
                if (detalle.IdRegistro != 0)
                {
                    if (detalle.IdRegistroDetalle == 0)
                    {
                        detalle.Activo = true;
                        db.RegistroActividadDetalle.Add(detalle);
                        db.SaveChanges();
                    }
                    else
                    {
                        RegistroActividadDetalle edit = db.RegistroActividadDetalle.First(x => x.IdRegistroDetalle == detalle.IdRegistroDetalle);
                        edit.Hora = detalle.Hora;
                        edit.LugarSalida = detalle.LugarSalida;
                        edit.LugarLlegada = detalle.LugarLlegada;
                        edit.Motivo = detalle.Motivo;
                        edit.JefeDepartamental = detalle.JefeDepartamental;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("CreateEdit", new { id = detalle.IdRegistro });
            }
            catch (Exception)
            {
                Session["Notificacion"] = "Error, Verifique la información ingresada!";
                return RedirectToAction("CreateEdit", new { id = detalle.IdRegistro });
            }
        }


        [PermisoAttribute(Accion = "Delete", Controlador = "RegActividad")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroActividad registroActividad = await db.RegistroActividad.FindAsync(id);
            if (registroActividad == null)
            {
                return HttpNotFound();
            }
            return View(registroActividad);
        }

        // POST: /RegActividad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RegistroActividad registroActividad = await db.RegistroActividad.FindAsync(id);
            //db.RegistroActividad.Remove(registroActividad);
            registroActividad.Activo = false;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
