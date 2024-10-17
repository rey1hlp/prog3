using ResearchSoftPUCPModel;
using ReserachSoftPUCPController.DAO;
using ReserachSoftPUCPController.MySQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
    public partial class GestionarGruposInvestigacion : System.Web.UI.Page
    {
        private DepartamentoAcademicoDAO departamentoAcademicoDAO;
        private BindingList<DepartamentoAcademico> listaDepartamentos;
        private MiembroPUCP miembroSeleccionado;
        private MiembroPUCP miembroEliminar;
        private MiembroPUCPDAO miembroDAO;
        private BindingList<MiembroPUCP> listaMiembrosPorGrupo;
        private BindingList<MiembroPUCP> listaMiembros;
        private GrupoInvestigacionDAO grupoInvestigacionDAO;
        private GrupoInvestigacion GrupoInvestigacion;
        private integrante_grupo_investigacionDAO integrante_Grupo_InvestigacionDAO;
        private byte[] foto;

        public GestionarGruposInvestigacion()
        {
            departamentoAcademicoDAO = new DepartamentoAcademicoMySQL();
            listaDepartamentos = new BindingList<DepartamentoAcademico>();
            miembroDAO = new MiembroPUCPMySQL();
            listaMiembrosPorGrupo = new BindingList<MiembroPUCP>();
            listaMiembros = new BindingList<MiembroPUCP>();
            grupoInvestigacionDAO=new GrupoInvestigacionMySQL();
            GrupoInvestigacion = new GrupoInvestigacion();
            integrante_Grupo_InvestigacionDAO = new integrante_grupo_investigacionMySQL();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["foto"] != null)
                foto = (byte[])Session["foto"];
            //Cambiar el título a 'Datos del Grupo de Investigación' cuando haya ingresado via el botón 'Mostrar Datos'
            lblTitulo.Text = "Registrar Grupo de Investigación";

            if (!IsPostBack)
            {
                cargarDepartamentos();
                Session["ListaMiembros"] = listaMiembrosPorGrupo;
            }
        }

        public void mostrarDatos()
        {
            lblTitulo.Text = "Datos del Grupo de Investigación";
            //Completar con la asignación
            //Por ejemplo:

            //dtpFechaFundacion.Value = grupoInvestigacion.FechaFundacion.ToString("yyyy-MM-dd");
            //string base64String = Convert.ToBase64String(grupoInvestigacion.Foto);
            //string imageUrl = "data:image/jpeg;base64," + base64String;
            //imgFotoGrupo.ImageUrl = imageUrl;

            txtNombre.Enabled = false;
            txtAcronimo.Enabled = false;
            ddlDepartamento.Enabled = false;
            btnGuardar.Enabled = false;
            btnSubirFotoGrupo.Visible = false;
            fileUploadFotoGrupo.Visible = false;
            rbBasica.Disabled = true;
            rbAplicada.Disabled = true;
            cbLaboratorio.Disabled = true;
            cbEquipamiento.Disabled = true;
            cbAmbienteTrabajo.Disabled = true;
            dtpFechaFundacion.Enabled = false;
            txtPresupuestoAnual.Enabled = false;
            txtDescripcion.Disabled = true;
            lbBuscarIntegrante.Visible = false;
            lbAgregarIntegrante.Visible = false;
            gvIntegrantes.Columns[4].Visible = false;
        }

        protected void btnSubirFotoGrupo_Click(object sender, EventArgs e)
        {
            //Verificar si se seleccionó un archivo
            if (fileUploadFotoGrupo.HasFile)
            {
                // Obtener la extensión del archivo
                string extension = System.IO.Path.GetExtension(fileUploadFotoGrupo.FileName);
                // Verificar si el archivo es una imagen
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png" || extension.ToLower() == ".gif")
                {
                    // Guardar la imagen en el servidor
                    string filename = Guid.NewGuid().ToString() + extension;
                    string filePath = Server.MapPath("~/Uploads/") + filename;
                    fileUploadFotoGrupo.SaveAs(Server.MapPath("~/Uploads/") + filename);
                    // Mostrar la imagen en la página
                    imgFotoGrupo.ImageUrl = "~/Uploads/" + filename;
                    imgFotoGrupo.Visible = true;
                    // Guardamos la referencia en una variable de sesión llamada foto
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    Session["foto"] = br.ReadBytes((int)fs.Length);
                    fs.Close();
                }
                else
                {
                    // Mostrar un mensaje de error si el archivo no es una imagen
                    Response.Write("Por favor, selecciona un archivo de imagen válido.");
                }
            }
            else
            {
                // Mostrar un mensaje de error si no se seleccionó ningún archivo
                Response.Write("Por favor, selecciona un archivo de imagen.");
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void lbBuscarIntegrante_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showModalForm() };";
            ScriptManager.RegisterStartupScript(this, GetType(), "", script, true);
        }

        protected void gvIntegrantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIntegrantes.PageIndex = e.NewPageIndex;
            gvIntegrantes.DataBind();
        }

        protected void gvMiembrosPUCP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMiembrosPUCP.PageIndex = e.NewPageIndex;
            gvMiembrosPUCP.DataBind();
        }

        // ///////////////////////////////////////////////////////////////
        protected void cargarDepartamentos()
        {

            // Obtenemos la lista de departamentos
            listaDepartamentos = departamentoAcademicoDAO.listarTodosDepas();
            ddlDepartamento.DataSource = listaDepartamentos;
            ddlDepartamento.DataTextField = "nombre";  // El nombre del departamento que se mostrará
            ddlDepartamento.DataValueField = "IdDepartamentoAcademico";
            ddlDepartamento.DataBind();
        }

        protected void lbBuscarMiembroPUCPModal_Click(object sender, EventArgs e)
        {

            string criterio = txtNombreCodigoPUCP.Text;
            listaMiembros = miembroDAO.listarTodosMiembros(criterio);
            gvMiembrosPUCP.DataSource = listaMiembros;
            gvMiembrosPUCP.DataBind();
        }
        protected void lbSeleccionar_click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lb.NamingContainer;

            string codigoPUCP = row.Cells[1].Text;

            listaMiembros = miembroDAO.listarTodosMiembros(codigoPUCP);
            foreach (MiembroPUCP i in listaMiembros)
            {
                Session["miembroPUCPSeleccionado"] = i;
            }//para buscar el miembro que se seleccionó

            ScriptManager.RegisterStartupScript(this, GetType(), "", "__doPostBack('', '');", true);
        }

        protected void lbAgregarIntegrante_Click(object sender, EventArgs e)
        {
            miembroSeleccionado = (MiembroPUCP)Session["miembroPUCPSeleccionado"];
            if (miembroSeleccionado != null)
            {
                listaMiembrosPorGrupo = (BindingList<MiembroPUCP>)Session["ListaMiembros"];
                listaMiembrosPorGrupo.Add(miembroSeleccionado);
                Session["ListaMiembros"] = listaMiembrosPorGrupo;
                gvIntegrantes.DataSource = listaMiembrosPorGrupo;
                gvIntegrantes.DataBind();
            }
        }
        protected void lbEliminar_click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lb.NamingContainer;
            string codigoPUCP = row.Cells[1].Text;

            listaMiembros = miembroDAO.listarTodosMiembros(codigoPUCP);
            foreach (MiembroPUCP i in listaMiembros)
            {
                miembroEliminar = i;
            }
            //Para tener un miembro para eliminar

            if (miembroEliminar != null)
            {
                listaMiembrosPorGrupo = (BindingList<MiembroPUCP>)Session["ListaMiembros"];
                listaMiembrosPorGrupo.Remove(miembroEliminar);
                Session["ListaMiembros"] = listaMiembrosPorGrupo;
                gvIntegrantes.DataSource = listaMiembrosPorGrupo;
                gvIntegrantes.DataBind();
            }

        }
        protected void btnGuardar_Click(object sender, EventArgs e) {
            GrupoInvestigacion.Nombre = txtNombre.Text;
            GrupoInvestigacion.Acronimo = txtAcronimo.Text;

            int asd = int.Parse(ddlDepartamento.SelectedValue);
            DepartamentoAcademico depa = new DepartamentoAcademico();
            depa.IdDepartamentoAcademico = asd;
            GrupoInvestigacion.DepartamentoAcademico = depa;
            DateTime fechaFundacion;
            if(DateTime.TryParseExact(dtpFechaFundacion.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFundacion))
            {
                GrupoInvestigacion.FechaFundacion = fechaFundacion;
            }
            Double presupuestoAnual;
            if(double.TryParse(txtPresupuestoAnual.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out presupuestoAnual))
            {
                GrupoInvestigacion.PresupuestoAnualDesignado = presupuestoAnual;
            }
            if (rbBasica.Checked) GrupoInvestigacion.TipoInvestigacion = TipoInvestigacion.BASICA;
            else if (rbAplicada.Checked) GrupoInvestigacion.TipoInvestigacion = TipoInvestigacion.APLICADA;

            GrupoInvestigacion.PoseeLaboratorio = cbLaboratorio.Checked;
            GrupoInvestigacion.PoseeEquipamientoEspecializado = cbEquipamiento.Checked;
            GrupoInvestigacion.PoseeAmbienteTrabajo = cbAmbienteTrabajo.Checked;

            GrupoInvestigacion.Descripcion = txtDescripcion.InnerText;
            GrupoInvestigacion.Foto = (byte[])Session["foto"];

            int? id=grupoInvestigacionDAO.insertar(GrupoInvestigacion);
            foreach (var a in ((BindingList<MiembroPUCP>)Session["ListaMiembros"]))
            {
                if (a is MiembroPUCP mie) integrante_Grupo_InvestigacionDAO.insertar(mie.IdMiembroPUCP,id);
            }
            Response.Redirect("ListarGruposInvestigacion.aspx");
        }

    }
}