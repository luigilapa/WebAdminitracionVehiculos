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
    public class ParametrosController : Controller
    {
        private Contexto db = new Contexto();

        [PermisoAttribute(Accion = "Index", Controlador = "Parametros")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Parametros.Where(x=>x.activo == true).ToListAsync());
        }

        // GET: /Parametros/Create
        [PermisoAttribute(Accion = "Create", Controlador = "Parametros")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Parametros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Parametros parametros)
        {
            if (ModelState.IsValid)
            {
                parametros.activo = true;
                db.Parametros.Add(parametros);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(parametros);
        }

        // GET: /Parametros/Edit/5
        [PermisoAttribute(Accion = "Edit", Controlador = "Parametros")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametros parametros = await db.Parametros.FindAsync(id);
            if (parametros == null)
            {
                return HttpNotFound();
            }
            return View(parametros);
        }

        // POST: /Parametros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Parametros parametros)
        {
            if (ModelState.IsValid)
            {
                parametros.activo = true;
                db.Entry(parametros).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parametros);
        }

        // GET: /Parametros/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "Parametros")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametros parametros = await db.Parametros.FindAsync(id);
            if (parametros == null)
            {
                return HttpNotFound();
            }
            return View(parametros);
        }

        // POST: /Parametros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parametros parametros = await db.Parametros.FindAsync(id);
            parametros.activo = false;
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
