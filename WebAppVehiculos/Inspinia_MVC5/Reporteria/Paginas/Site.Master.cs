using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class SiteMaster : MasterPage
    {
        public String TituloPagina { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            TituloPagina = "SAV";
        }
    }
}