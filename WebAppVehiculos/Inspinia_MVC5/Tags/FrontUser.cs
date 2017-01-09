using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelo;


namespace Inspinia_MVC5.Tags
{
    public class FrontUser
    {
        public static bool TienePermiso(string Controlador, string Accion)
        {
            Usuario usuario = ObtenerUsuario();
            if (usuario == null)
            {
                return false;
            }
            else
            {
                ICollection<Opcion> opciones = ObtenerOpciones();
                return opciones.Where(x => x.controlador == Controlador && x.accion == Accion).Any();
            }
            // return true; //!usuario.Rol.Permiso.Where(x => x.PermisoID == valor).Any();
        }


        public static bool EstaLogiado() {
            Usuario usuario = ObtenerUsuario();
            if (usuario == null)
            {
                return false;
            }
            else
            {
                return true;
            }
           }

        private static Usuario ObtenerUsuario()
        {
            return (Usuario)HttpContext.Current.Session["Usuario"];
        }

        private static ICollection<Opcion> ObtenerOpciones()
        {
            return (ICollection<Opcion>)HttpContext.Current.Session["Opciones"];
        }
    }
}