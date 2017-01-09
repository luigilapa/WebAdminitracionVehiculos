using Inspinia_MVC5.Tags;
using Modelo;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Inspinia_MVC5.Reporteria.Paginas
{
    public partial class ReporteVehiculoChofer : System.Web.UI.Page
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

        void CargarDatos()
        {
                var marcas = db.VehiculoMarca
                    .Where(x => x.activo == true)
                    .Select(x => new { Id = x.idMarca.ToString(), Nombre = x.nombre })
                    .OrderBy(x => x.Nombre)
                    .ToList();

                marcas.Add(new { Id = "", Nombre = "Todos" });

                this.CmbMarca.DataSource = marcas;
                this.CmbMarca.DataValueField = "Id";
                this.CmbMarca.DataTextField = "Nombre";
                this.CmbMarca.DataBind();

                this.CmbMarca.SelectedValue = "";

                var tipos = db.VehiculoTipo
                    .Where(x => x.activo == true)
                    .Select(x => new { Id = x.idTipo.ToString(), Nombre = x.nombre })
                    .OrderBy(x => x.Nombre)
                    .ToList();

                tipos.Add(new { Id = "", Nombre = "Todos" });

                this.CmbTipo.DataSource = tipos;
                this.CmbTipo.DataValueField = "Id";
                this.CmbTipo.DataTextField = "Nombre";
                this.CmbTipo.DataBind();

                this.CmbTipo.SelectedValue = "";
            }
        }
}