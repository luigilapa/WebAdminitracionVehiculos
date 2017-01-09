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
    public class VehiculoTiposController : Controller
    {
        private Contexto db = new Contexto();

        // GET: VehiculoMarcas
        [PermisoAttribute(Accion = "Index", Controlador = "VehiculoTipos")]
        public async Task<ActionResult> Index()
        {
            return View(await db.VehiculoTipo.Where(x=>x.activo == true).OrderBy(x => x.nombre).ToListAsync());
        }

        // GET: VehiculoMarcas/Create
        [PermisoAttribute(Accion = "Create", Controlador = "VehiculoTipos")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehiculoMarcas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idTipo,nombre,descripcion")] VehiculoTipo vehiculoTipo)
        {
            if (ModelState.IsValid)
            {
                vehiculoTipo.activo = true;
                db.VehiculoTipo.Add(vehiculoTipo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehiculoTipo);
        }

        // GET: VehiculoMarcas/Edit/5
        [PermisoAttribute(Accion = "Edit", Controlador = "VehiculoTipos")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculoTipo vehiculoTipo = await db.VehiculoTipo.FindAsync(id);
            if (vehiculoTipo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculoTipo);
        }

        // POST: VehiculoMarcas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idTipo,nombre,descripcion")] VehiculoTipo vehiculoTipo)
        {
            if (ModelState.IsValid)
            {
                VehiculoTipo _vehiculoTipo = await db.VehiculoTipo.FindAsync(vehiculoTipo.idTipo);

                //db.Entry(vehiculoMarca).State = EntityState.Modified;

                _vehiculoTipo.nombre = vehiculoTipo.nombre;
                _vehiculoTipo.descripcion = vehiculoTipo.descripcion;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehiculoTipo);
        }

        // GET: VehiculoMarcas/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "VehiculoTipos")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehiculoTipo vehiculoTipo = await db.VehiculoTipo.FindAsync(id);
            if (vehiculoTipo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculoTipo);
        }

        // POST: VehiculoMarcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehiculoTipo vehiculoTipo = await db.VehiculoTipo.FindAsync(id);
            //db.vehiculoMarca.Remove(vehiculoMarca);
            vehiculoTipo.activo = false;
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
