using Inspinia_MVC5.Tags;
using Modelo;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Inspinia_MVC5.Reporteria.Paginas
{
    public partial class ReporteChoferes : System.Web.UI.Page
    {
        private Contexto db = new Contexto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!FrontUser.TienePermiso(null, null)) {
                if (!FrontUser.EstaLogiado())
                {
                    Response.Redirect("/");
                }
                CargarDatos();
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            ReportViewer.LocalReport.Refresh();
        }

        void CargarDatos() {

            var choferes = db.Usuario
                .Where(x => x.Activo == true && x.EsChofer == true)
                .Select(x => new { Id = x.IdUsuario.ToString(), Nombre = x.Nombres + " " + x.Apellidos })
                .OrderBy(x=>x.Nombre)
                .ToList();

            choferes.Add(new { Id= "", Nombre = "Todos"});

            this.cmbChofer.DataSource = choferes;
            this.cmbChofer.DataValueField = "Id";
            this.cmbChofer.DataTextField = "Nombre";
            this.cmbChofer.DataBind();

            this.cmbChofer.SelectedValue = "";

            //var roles = db.Rol
            //    .Where(x => x.Activo == true)
            //    .Select(x => new { Id = x.IdRol.ToString(), Nombre = x.NombreRol})
            //    .OrderBy(x => x.Nombre)
            //    .ToList();

            //roles.Add(new { Id = "", Nombre = "Todos" });

            //this.CmbRol.DataSource = roles;
            //this.CmbRol.DataValueField = "Id";
            //this.CmbRol.DataTextField = "Nombre";
            //this.CmbRol.DataBind();

            //this.CmbRol.SelectedValue = "";
        }
    }
}