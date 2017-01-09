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
    public class RolController : Controller                                                                                                                                                                                                                                    
    {
        private Contexto db = new Contexto();

        // GET: VehiculoMarcas
        [PermisoAttribute(Accion = "Index", Controlador = "Rol")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Rol.Where(x=>x.Activo == true).OrderBy(x=>x.NombreRol).ToListAsync());
        }

        // GET: VehiculoMarcas/Create
        [PermisoAttribute(Accion = "Create", Controlador = "Rol")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehiculoMarcas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdRol,NombreRol,Descripcion")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                rol.Activo = true;
                db.Rol.Add(rol);
                await db.SaveChangesAsync();

                db.Opcion.Where(x => x.activo == true).ToList()
                    .ForEach(x => db.OpcionRol.Add(new OpcionRol {
                                                       IdRol = rol.IdRol,
                                                       IdOpcion = x.idOpcion,
                                                       TienePermiso = false,
                                                       Activo = true
                                                    }));

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(rol);
        }

        // GET: VehiculoMarcas/Edit/5
        [PermisoAttribute(Accion = "Edit", Controlador = "Rol")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = await db.Rol.FindAsync(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: VehiculoMarcas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdRol,NombreRol,Descripcion")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                Rol _rol = await db.Rol.FindAsync(rol.IdRol);

                //db.Entry(vehiculoMarca).State = EntityState.Modified;

                _rol.NombreRol       = rol.NombreRol;
                _rol.Descripcion = rol.Descripcion;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        // GET: VehiculoMarcas/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "Rol")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = await db.Rol.FindAsync(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: VehiculoMarcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rol rol = await db.Rol.FindAsync(id);
            //db.vehiculoMarca.Remove(vehiculoMarca);
            rol.Activo = false;

            db.OpcionRol.Where(x => x.IdRol == id).ToList()
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
