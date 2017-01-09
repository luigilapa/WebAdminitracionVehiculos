using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Modelo;
using Inspinia_MVC5.Tags;
using System.Configuration;
using System.IO;
using System.Web;
using Inspinia_MVC5.Extencion;
using System;

namespace AdministracionVehiculos.Web.Controllers
{
    [PermisoAttribute()]
    public class UsuariosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Usuarios
        [PermisoAttribute(Accion = "Index", Controlador = "Usuarios")]
        public async Task<ActionResult> Index()
        {
            var usuario = db.Usuario.Where(x=>x.Activo == true).OrderBy(x=>x.Rol.NombreRol).ThenBy(x=>x.Nombres).ThenBy(x=>x.Apellidos).Include(u => u.Rol);
            return View(await usuario.ToListAsync());
        }


        // GET: Usuarios/Create
        [PermisoAttribute(Accion = "Create", Controlador = "Usuarios")]
        public ActionResult Create()
        {
            ViewBag.IdRol = ObtenerListaTiposUsurios();
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                bool flagLicencia = true;
                if (Validaciones.VerificarCedula(usuario.Cedula))
                {
                    if (usuario.FechaNacimiento == null || usuario.FechaNacimiento < DateTime.Today)
                    {

                        var rol = db.Rol.Where(r => r.IdRol == usuario.IdRol).FirstOrDefault();
                        if (rol.NombreRol == "Chofer")
                        {
                            usuario.EsChofer = true;

                            if (usuario.FechaVencimientoLicencia == null || usuario.FechaVencimientoLicencia > DateTime.Today)
                            {
                                flagLicencia = true;
                            }
                            else {
                                flagLicencia = false;
                            }
                        }

                        if (flagLicencia)
                        {
                            usuario.Activo = true;
                            usuario.NombreUsuario = usuario.Cedula;
                            usuario.Clave = Seguridad.Encriptar(usuario.Cedula);
                            db.Usuario.Add(usuario);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("FechaVencimientoLicencia", "La Fecha de Vencimiento Licencia debe ser mayor a la fecha actual.");
                        }
                    }
                    else {
                        ModelState.AddModelError("FechaNacimiento", "La Fecha de Nacimiento debe ser menor a la fecha actual.");
                    }
                    
                }
                else {
                    ModelState.AddModelError("Cedula", "El número de cédula no es válido.");
                }
            }
            ViewBag.IdRol = ObtenerListaTiposUsurios((int)usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [PermisoAttribute(Accion = "Edit", Controlador = "Usuarios")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = ObtenerListaTiposUsurios((int)usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                bool flagLicencia = true;
                if (Validaciones.VerificarCedula(usuario.Cedula)) {

                    if (usuario.FechaNacimiento == null || usuario.FechaNacimiento < DateTime.Today)
                    {
                        var rol = db.Rol.Where(r => r.IdRol == usuario.IdRol).FirstOrDefault();

                        if (rol.NombreRol == "Chofer")
                        {
                            usuario.EsChofer = true;

                            if (usuario.FechaVencimientoLicencia == null || usuario.FechaVencimientoLicencia > DateTime.Today)
                            {
                                flagLicencia = true;
                            }
                            else
                            {
                                flagLicencia = false;
                            }
                        }

                            if (flagLicencia)
                            {
                                Usuario _usuario = await db.Usuario.FindAsync(usuario.IdUsuario);

                                _usuario.Nombres = usuario.Nombres;
                                _usuario.Apellidos = usuario.Apellidos;
                                _usuario.Cedula = usuario.Cedula;
                                _usuario.IdRol = usuario.IdRol;
                                _usuario.Correo = usuario.Correo;
                                _usuario.Telefono = usuario.Telefono;
                                _usuario.EsChofer = usuario.EsChofer;
                                _usuario.Direccion = usuario.Direccion;
                                _usuario.FechaNacimiento = usuario.FechaNacimiento;
                                _usuario.FechaVencimientoLicencia = usuario.FechaVencimientoLicencia;
                                _usuario.TipoLicencia = usuario.TipoLicencia;

                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ModelState.AddModelError("FechaVencimientoLicencia", "La Fecha de Vencimiento Licencia debe ser mayor a la fecha actual.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("FechaNacimiento", "La Fecha de Nacimiento debe ser menor a la fecha actual.");
                        }
                    }
                else
                {
                    ModelState.AddModelError("Cedula", "El número de cédula no es válido.");
                }
            }
            ViewBag.IdRol = ObtenerListaTiposUsurios((int)usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [PermisoAttribute(Accion = "Delete", Controlador = "Usuarios")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = await db.Usuario.FindAsync(id);
            //db.usuario.Remove(usuario);
            usuario.Activo = false;
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

        private SelectList ObtenerListaTiposUsurios(int id = 0)
        {
            return new SelectList(db.Rol.Where(x=>x.Activo == true).OrderBy(x=>x.NombreRol), "IdRol", "NombreRol", id);
        }

        string RutaBaseDeImagenes() {
            return ConfigurationManager.AppSettings["rutaImagenes"];
        }

        //void GuardarImagen(string ruta, int idUsuario) {
        //    if (!Directory.Exists(RutaBaseDeImagenes()))
        //    {
        //        Directory.CreateDirectory(RutaBaseDeImagenes());
        //    }

        //    System.IO.File.Copy(ruta, idUsuario.ToString()+"_"+ System.DateTime.Today.ToShortDateString().Replace("/",""), true);
        //}
    }

   
}
