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
    public class OpcionController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Opcion
        [PermisoAttribute(Accion ="Index",Controlador = "Opcion")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Opcion.Where(x=>x.activo == true).OrderBy(x=>x.modulo).ThenBy(x=>x.controlador).ThenBy(x=>x.accion).ToListAsync());
        }

        // GET: Opcion/Create
        [PermisoAttribute(Accion = "Create", Controlador = "Opcion")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opcion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Opcion opcion)
        {
            if (ModelState.IsValid)
            {
                opcion.activo = true;
                db.Opcion.Add(opcion);
                await db.SaveChangesAsync();

                db.Rol.Where(x => x.Activo == true).ToList()
                    .ForEach(x => db.OpcionRol.Add(new OpcionRol{ IdRol = x.IdRol, IdOpcion = opcion.idOpcion, Activo = true, TienePermiso = false }));

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(opcion);
        }

        // GET: Opcion/Edit/5
        [PermisoAttribute(Accion = "Edit", Controlador = "Opcion")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = await db.Opcion.FindAsync(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // POST: Opcion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Opcion opcion)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(opcion).State = EntityState.Modified;
                Opcion _opcion = await db.Opcion.FindAsync(opcion.idOpcion);

                _opcion.descripcion = opcion.descripcion;
                _opcion.controlador = opcion.controlador;
                _opcion.accion = opcion.accion;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(opcion);
        }

        // GET: Opcion/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "Opcion")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = await db.Opcion.FindAsync(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // POST: Opcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Opcion opcion = await db.Opcion.FindAsync(id);
            //db.Opcion.Remove(opcion);
            opcion.activo = false;

            db.OpcionRol.Where(x => x.IdOpcion == id).ToList()
                .ForEach(x => x.Activo = false);

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
