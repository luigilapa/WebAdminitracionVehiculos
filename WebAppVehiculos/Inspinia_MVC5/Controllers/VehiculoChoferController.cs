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
    public class VehiculoChoferController : Controller
    {
        private Contexto db = new Contexto();

        [PermisoAttribute(Accion = "Index", Controlador = "VehiculoChofer")]
        public async Task<ActionResult> Index()
        {
            var vehiculochofer = db.VehiculoChofer.Where(x=>x.Activo == true).Include(v => v.Usuario).Include(v => v.Vehiculo);
            return View(await vehiculochofer.ToListAsync());
        }

        [PermisoAttribute(Accion = "Create", Controlador = "VehiculoChofer")]
        public ActionResult Create()
        {
            ViewBag.IdChofer = new SelectList(db.Usuario.Where(x=>x.EsChofer == true && x.Activo == true).Select(x=> new { IdUsuario = x.IdUsuario, Nombres = x.Nombres + " " + x.Apellidos }), "IdUsuario", "Nombres");
            ViewBag.IdVehiculo = new SelectList(db.Vehiculo.Where(x=>x.activo == true).Select(x=> new { idVehiculo  = x.idVehiculo, placa = x.placaLetras+x.placaNumeros +" - " + x.VehiculoMarca.nombre +" - "+x.VehiculoTipo.nombre}), "idVehiculo", "placa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehiculoChofer vehiculoChofer)
        {
            if (ModelState.IsValid)
            {

                if (!db.VehiculoChofer.Where(x=>x.Activo == true && x.IdVehiculo == vehiculoChofer.IdVehiculo).Any())
                {
                    if (!db.VehiculoChofer.Where(x => x.Activo == true && x.IdChofer == vehiculoChofer.IdChofer).Any())
                    {
                        vehiculoChofer.FechaAsignacion = DateTime.Now;
                        vehiculoChofer.Activo = true;

                        db.VehiculoChofer.Add(vehiculoChofer);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("IdChofer", "El chofer ya tiene un vehículo asignado.");
                    }
                }
                else
                {
                    ModelState.AddModelError("IdVehiculo", "El vehículo ya se encuentra asignado.");
                }
            }

            ViewBag.IdChofer = new SelectList(db.Usuario.Where(x => x.EsChofer == true && x.Activo == true).Select(x => new { IdUsuario = x.IdUsuario, Nombres = x.Nombres + " " + x.Apellidos }), "IdUsuario", "Nombres");
            ViewBag.IdVehiculo = new SelectList(db.Vehiculo.Select(x => new { idVehiculo = x.idVehiculo, placa = x.placaLetras + x.placaNumeros }), "idVehiculo", "placa", vehiculoChofer.IdVehiculo);
            return View(vehiculoChofer);
        }


        // GET: /VehiculoChofer/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "VehiculoChofer")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculoChofer vehiculoChofer = await db.VehiculoChofer.FindAsync(id);
            if (vehiculoChofer == null)
            {
                return HttpNotFound();
            }
            return View(vehiculoChofer);
        }

        // POST: /VehiculoChofer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehiculoChofer vehiculoChofer = await db.VehiculoChofer.FindAsync(id);
            vehiculoChofer.Activo = false;
            vehiculoChofer.FechaDesasignacion = DateTime.Now;
            //db.VehiculoChofer.Remove(vehiculoChofer);
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
