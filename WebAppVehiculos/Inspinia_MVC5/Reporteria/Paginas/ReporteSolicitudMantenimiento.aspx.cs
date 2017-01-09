using Inspinia_MVC5.Tags;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inspinia_MVC5.Reporteria.Paginas
{
    public partial class ReporteSolicitudMantenimiento : System.Web.UI.Page
    {
        private Contexto db = new Contexto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

            var choferes = db.Usuario
                .Where(x => x.Activo == true && x.EsChofer == true)
                .Select(x => new { Id = x.IdUsuario.ToString(), Nombre = x.Nombres + " " + x.Apellidos })
                .OrderBy(x => x.Nombre)
                .ToList();

            choferes.Add(new { Id = "", Nombre = "Todos" });

            this.cmbChofer.DataSource = choferes;
            this.cmbChofer.DataValueField = "Id";
            this.cmbChofer.DataTextField = "Nombre";
            this.cmbChofer.DataBind();

            this.cmbChofer.SelectedValue = "";

            var roles = db.Vehiculo
                .Where(x => x.activo == true)
                .Select(x => new { Id = x.idVehiculo.ToString(), Nombre = x.placaLetras+x.placaNumeros + " - " + x.VehiculoMarca.nombre + " - " + x.VehiculoTipo.nombre })
                .OrderBy(x => x.Nombre)
                .ToList();

            roles.Add(new { Id = "", Nombre = "Todos" });

            this.CmbVehiculo.DataSource = roles;
            this.CmbVehiculo.DataValueField = "Id";
            this.CmbVehiculo.DataTextField = "Nombre";
            this.CmbVehiculo.DataBind();

            this.CmbVehiculo.SelectedValue = "";

            this.CmbTipoMantenimiento.Items.Add(new ListItem("Preventivo", "Preventivo"));
            this.CmbTipoMantenimiento.Items.Add(new ListItem("Correctivo", "Correctivo"));
            this.CmbTipoMantenimiento.Items.Add(new ListItem("Todos", ""));
            this.CmbTipoMantenimiento.SelectedValue = "";

            this.CmbAprobado.Items.Add(new ListItem("SI", "True"));
            this.CmbAprobado.Items.Add(new ListItem("NO", "False"));
            this.CmbAprobado.Items.Add(new ListItem("TODOS", ""));
            this.CmbAprobado.SelectedValue = "";
        }
    }
}