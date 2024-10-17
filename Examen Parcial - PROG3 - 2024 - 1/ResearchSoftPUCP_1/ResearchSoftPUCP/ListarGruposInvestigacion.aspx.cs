using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchSoftPUCP
{
    /*
     * Colocar datos:
     * --------------------------------------
     * Código PUCP: 
     * Nombre Completo: 
     */
    public partial class ListarGruposInvestigacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarGruposInvestigacion.aspx"); ;
        }

        protected void gvGruposInvestigacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGruposInvestigacion.PageIndex = e.NewPageIndex;
            gvGruposInvestigacion.DataBind();
        }
    }
}