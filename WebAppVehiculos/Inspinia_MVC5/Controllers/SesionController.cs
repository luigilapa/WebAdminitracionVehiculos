using System.Linq;
using System.Web.Mvc;
using Modelo;
using Inspinia_MVC5.Extencion;
using Inspinia_MVC5.Tags;
using System.Xml.Linq;
using System.Xml;
using System;

namespace Inspinia_MVC5.Controllers
{
    public class SesionController : Controller
    {
        Contexto db = new Contexto();

        public ActionResult Autenticar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(string usuario, string clave)
        {
            try
            {
                //string cadena = Seguridad.Encriptar("12345");
                string claveEncrictada = Seguridad.Encriptar(clave);
                Usuario login = db.Usuario.Where(x => x.NombreUsuario == usuario && x.Clave == claveEncrictada && x.Activo == true).FirstOrDefault();
                if (login != null)
                {
                    //SessionHelper.AddUserToSession(login.idUsuario.ToString()); 
                    Session["Usuario"] = login;
                    Session["Opciones"] = login.Rol.OpcionRol.Where(x=>x.Activo == true && x.TienePermiso == true).Select(x=>x.Opcion).ToList();
                    Session["UsuarioNombre"] = login.Nombres;
                    Session["UsuarioRol"] = login.Rol.NombreRol;

                    string xml = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Extencion/PlantillasCorreo/PlantillaInicioSesion.html")));
                    var html = xml.ToString();

                    html = html.Replace("@nombres", login.Nombres + " " + login.Apellidos);
                    html = html.Replace("@fecha", DateTime.Now.ToLongDateString()+" "+DateTime.Now.ToShortTimeString());

                    Correo.EnviarCorreoGmail(login.Correo, "Informe de inicio de sesión", html);

                    return RedirectToAction("Index", "Inicio");
            }
            else
            {
                return View();
            }
            }
            catch (Exception)
            {
                return View(); 
            }
         
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            Session["Opciones"] = null;
            Session["UsuarioNombre"] = null;
            Session["UsuarioRol"] = null;
            return RedirectToAction("Autenticar", "Sesion");
        }

        [PermisoAttribute()]
        public ActionResult CambiarCredenciales() {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarCredenciales(UsuarioCredenciales uc)
        {
            if (uc.NuevoUsuario != null || uc.NuevaClave != null)
            {
                Usuario logiado = (Usuario)Session["Usuario"];
                if (uc.Usuario == logiado.NombreUsuario && uc.Clave == Seguridad.Encriptar(logiado.Clave))
                {
                    Usuario usuario = db.Usuario.First(x => x.IdUsuario == logiado.IdUsuario);
                    usuario.NombreUsuario = uc.NuevoUsuario != null ? uc.NuevoUsuario : usuario.NombreUsuario;
                    usuario.Clave = uc.NuevaClave != null ? Seguridad.Encriptar(uc.NuevaClave) : usuario.Clave;
                    db.SaveChanges();
                    return RedirectToAction("CerrarSesion");
                }
            }
            return View();
        }
    }
}