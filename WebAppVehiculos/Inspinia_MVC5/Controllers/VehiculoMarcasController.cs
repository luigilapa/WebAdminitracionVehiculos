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
    public class VehiculoMarcasController : Controller
    {
        private Contexto db = new Contexto();

        // [PermisoAttribute(Accion = "Index", Controlador = "VehiculoMarcas")]
        [PermisoAttribute(Accion = "Index", Controlador = "VehiculoMarcas")]
        public async Task<ActionResult> Index()
        {
            return View(await db.VehiculoMarca.Where(x=>x.activo == true).OrderBy(x => x.nombre).ToListAsync());
        }

        // GET: VehiculoMarcas/Create
        [PermisoAttribute(Accion = "Create", Controlador = "VehiculoMarcas")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehiculoMarcas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idMarca,nombre,descripcion")] VehiculoMarca vehiculoMarca)
        {
            if (ModelState.IsValid)
            {
                vehiculoMarca.activo = true;
                db.VehiculoMarca.Add(vehiculoMarca);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehiculoMarca);
        }

        // GET: VehiculoMarcas/Edit/5
        [PermisoAttribute(Accion = "Edit", Controlador = "VehiculoMarcas")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculoMarca vehiculoMarca = await db.VehiculoMarca.FindAsync(id);
            if (vehiculoMarca == null)
            {
                return HttpNotFound();
            }
            return View(vehiculoMarca);
        }

        // POST: VehiculoMarcas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idMarca,nombre,descripcion")] VehiculoMarca vehiculoMarca)
        {
            if (ModelState.IsValid)
            {
                VehiculoMarca _vehiculoMarca = await db.VehiculoMarca.FindAsync(vehiculoMarca.idMarca);

                //db.Entry(vehiculoMarca).State = EntityState.Modified;

                _vehiculoMarca.nombre = vehiculoMarca.nombre;
                _vehiculoMarca.descripcion = vehiculoMarca.descripcion;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehiculoMarca);
        }

        // GET: VehiculoMarcas/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "VehiculoMarcas")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculoMarca vehiculoMarca = await db.VehiculoMarca.FindAsync(id);
            if (vehiculoMarca == null)
            {
                return HttpNotFound();
            }
            return View(vehiculoMarca);
        }

        // POST: VehiculoMarcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehiculoMarca vehiculoMarca = await db.VehiculoMarca.FindAsync(id);
            //db.vehiculoMarca.Remove(vehiculoMarca);
            vehiculoMarca.activo = false;
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
