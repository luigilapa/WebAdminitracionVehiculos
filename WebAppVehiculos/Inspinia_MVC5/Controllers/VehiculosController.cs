using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Modelo;
using Inspinia_MVC5.Tags;
using Inspinia_MVC5.Extencion;
using System;

namespace Inspinia_MVC5.Controllers
{
    [PermisoAttribute()]
    public class VehiculosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Vehiculos
        [PermisoAttribute(Accion = "Index", Controlador = "Vehiculos")]
        public async Task<ActionResult> Index()
        {
            var vehiculo = db.Vehiculo.Where(x => x.activo == true).Include(v => v.VehiculoMarca).Include(v => v.VehiculoTipo)
                    .OrderBy(x=>x.VehiculoMarca.nombre).ThenBy(x=>x.VehiculoTipo.nombre);
            return View(await vehiculo.ToListAsync());
        }

        // GET: Vehiculos/Create
        [PermisoAttribute(Accion = "Create", Controlador = "Vehiculos")]
        public ActionResult Create()
        {
            ViewBag.idMarca = ObtenerListaMarcas();
            ViewBag.idTipo = ObtenerListaTipos();
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                int isNumeric;
                if (int.TryParse(vehiculo.placaNumeros, out isNumeric) && vehiculo.placaNumeros.Length == 4)
                {
                    if (vehiculo.anio <= DateTime.Today.Year + 1)
                    {
                        vehiculo.activo = true;
                        vehiculo.placaLetras = vehiculo.placaLetras.ToUpper();
                        db.Vehiculo.Add(vehiculo);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("anio", "Al año no puede ser mayor a "+ (DateTime.Today.Year+1).ToString());
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("placaNumeros", "Ingrese los 4 números de la placa correctamente.");
                }
            }

            ViewBag.idMarca = ObtenerListaMarcas(vehiculo.idMarca);
            ViewBag.idTipo = ObtenerListaTipos(vehiculo.idTipo);
            return View(vehiculo);
        }

        // GET: Vehiculos/Edit/5
        [PermisoAttribute(Accion = "Edit", Controlador = "Vehiculos")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = await db.Vehiculo.FindAsync(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMarca = ObtenerListaMarcas(vehiculo.idMarca);
            ViewBag.idTipo = ObtenerListaTipos(vehiculo.idTipo);
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                int isNumeric;
                if (int.TryParse(vehiculo.placaNumeros, out isNumeric) && vehiculo.placaNumeros.Length == 4)
                {
                    if (vehiculo.anio <= DateTime.Today.Year + 1)
                    {
                        Vehiculo _vehiculo = await db.Vehiculo.FindAsync(vehiculo.idVehiculo);

                        _vehiculo.placaLetras = vehiculo.placaLetras.ToUpper();
                        _vehiculo.placaNumeros = vehiculo.placaNumeros;
                        _vehiculo.idMarca = vehiculo.idMarca;
                        _vehiculo.idTipo = vehiculo.idTipo;
                        _vehiculo.modelo = vehiculo.modelo;
                        _vehiculo.anio = vehiculo.anio;
                        _vehiculo.color1 = vehiculo.color1;
                        _vehiculo.descripcion = vehiculo.descripcion;

                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("anio", "Al año no puede ser mayor a " + (DateTime.Today.Year+1).ToString());
                    }
                }
                else
                {
                    ModelState.AddModelError("placaNumeros", "Ingrese los 4 números de la placa correctamente.");
                }
               
            }
            ViewBag.idMarca = ObtenerListaMarcas(vehiculo.idMarca);
            ViewBag.idTipo = ObtenerListaTipos(vehiculo.idTipo);
            return View(vehiculo);
        }

        // GET: Vehiculos/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "Vehiculos")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = await db.Vehiculo.FindAsync(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vehiculo _vehiculo = await db.Vehiculo.FindAsync(id);
            _vehiculo.activo = false;
           //'db.vehiculo.Remove(vehiculo);
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

        private SelectList ObtenerListaMarcas(int id = 0) {
           return new SelectList(db.VehiculoMarca.Where(x=>x.activo == true).OrderBy(x=>x.nombre), "idMarca", "nombre", id);
        }

        private SelectList ObtenerListaTipos(int id = 0)
        {
            return new SelectList(db.VehiculoTipo.Where(x => x.activo == true).OrderBy(x => x.nombre), "idTipo", "nombre", id);
        }
    }
}