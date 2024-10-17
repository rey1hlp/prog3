using MySql.Data.MySqlClient;
using ResearchSoftPUCPDBManager;
using ResearchSoftPUCPModel;
using ReserachSoftPUCPController.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserachSoftPUCPController.MySQL
{
    public class GrupoInvestigacionMySQL : DAOImplBase, GrupoInvestigacionDAO
    {
        private GrupoInvestigacion grupo;
        private string nombre_acronimo;
        private string sql_filtro;
        public GrupoInvestigacionMySQL() : base("grupo_investigacion")
        {
            grupo = null;
            this.RetornarLlavePrimaria = true;
            sql_filtro = null;
        }


        //INSERTAR
        public int? insertar(GrupoInvestigacion grupo)
        {
            this.grupo = grupo;
            return this.insertar();
        }
        protected override string generarSQLParaInsercion()
        {
            return "CALL INSERTAR_GRUPO_INVESTIGACION(@_id_grupo_investigacion,@_fid_departamento_academico,@_nombre,@_acronimo,@_tipo_investigacion,@_fecha_fundacion,@_presupuesto_anual_designado,@_posee_laboratorio,@_posee_equipamiento_especializado,@_posee_ambiente_trabajo,@_descripcion,@_foto)";
        }
        protected override void incluirValorParametrosParaInsercion()
        {
            this.Comando.Parameters.Add(new MySqlParameter("@_id_grupo_investigacion", MySqlDbType.Int32));
            this.Comando.Parameters["@_id_grupo_investigacion"].Direction = ParameterDirection.Output; // Indica que es un parámetro de salida

            // Parámetros de entrada
            this.Comando.Parameters.AddWithValue("@_fid_departamento_academico", this.grupo.DepartamentoAcademico.IdDepartamentoAcademico);
            this.Comando.Parameters.AddWithValue("@_nombre", this.grupo.Nombre);
            this.Comando.Parameters.AddWithValue("@_acronimo", this.grupo.Acronimo);
            this.Comando.Parameters.AddWithValue("@_tipo_investigacion", this.grupo.TipoInvestigacion.ToString());
            this.Comando.Parameters.AddWithValue("@_fecha_fundacion", this.grupo.FechaFundacion);
            this.Comando.Parameters.AddWithValue("@_presupuesto_anual_designado", this.grupo.PresupuestoAnualDesignado);
            this.Comando.Parameters.AddWithValue("@_posee_laboratorio", this.grupo.PoseeLaboratorio);
            this.Comando.Parameters.AddWithValue("@_posee_equipamiento_especializado", this.grupo.PoseeEquipamientoEspecializado);
            this.Comando.Parameters.AddWithValue("@_posee_ambiente_trabajo", this.grupo.PoseeAmbienteTrabajo);
            this.Comando.Parameters.AddWithValue("@_descripcion", this.grupo.Descripcion);
            this.Comando.Parameters.AddWithValue("@_foto", this.grupo.Foto);
        }

        //MODIFICAR
        public int? modificar(GrupoInvestigacion grupo)
        {
            this.grupo = grupo;
            return this.modificar();
        }
        protected override string generarSQLParaModificacion()
        {
            return "";
        }
        protected override void incluirValorParametrosParaModificacion()
        {
            //no hay un procedimiento
        }

        //ELIMINAR
        public int? eliminar(int grupo)
        {
            this.grupo = new GrupoInvestigacion();
            this.grupo.IdGrupoInvestigacion = grupo;
            return this.eliminar();
        }
        protected override string generarSQLParaEliminacion()
        {
            return "";
        }
        protected override void incluirValorParametrosParaEliminacion()
        {
            //no hay un procedimiento
        }

        // LISTAR
        protected override void instanciarObjetoDelResultSet(MySqlDataReader lector)
        {
            this.grupo = new GrupoInvestigacion();
            grupo.IdGrupoInvestigacion = lector.GetInt32("id_grupo_investigacion");
            grupo.Acronimo = lector.GetString("acronimo");
            grupo.Nombre = lector.GetString("nombre");
        }

        public BindingList<GrupoInvestigacion> listarPorNombreAcronimo(string nombre_acronimo)
        {
            BindingList<GrupoInvestigacion> lista = new BindingList<GrupoInvestigacion>();
            this.nombre_acronimo = nombre_acronimo;
            foreach (var a in base.listarTodos(null))
            {
                if (a is GrupoInvestigacion grupo)
                {
                    lista.Add(grupo);
                }
            }
            return lista;
        }

        protected override void agregarObjetoALaLista(BindingList<object> lista, MySqlDataReader lector)
        {
            this.instanciarObjetoDelResultSet(this.Lector);
            lista.Add(this.grupo);
        }

        public GrupoInvestigacion obtenerPorID(int id)
        {
            this.grupo = new GrupoInvestigacion();
            this.grupo.IdGrupoInvestigacion = id;
            base.obtenerPorId();
            return this.grupo;
        }

        protected override string obtenerProyeccionParaSelect()
        {
            return "id_grupo_investigacion, acronimo, nombre";
        }

        protected override string obtenerPredicadoParaListarTodos()
        {
            if (this.sql_filtro != null)
                return this.sql_filtro;
            return "";
        }

        protected override string generarSQLParaListarTodos(int? limite)
        {
            return "call LISTAR_GRUPOS_INVESTIGACION_X_NOMBRE_ACRONIMO('"+this.nombre_acronimo+"')";
        }

        public BindingList<GrupoInvestigacion> buscarGrupo(string titulo)
        {
            if (titulo != null && titulo != "")
                this.sql_filtro = " where titulo like \"%" + titulo + "%\"";
            BindingList<GrupoInvestigacion> lista = this.listarPorNombreAcronimo(this.nombre_acronimo);
            this.sql_filtro = null;
            return lista;
        }


        protected override void incluirValorParametrosParaObtenerPorId()
        {
            this.Comando.Parameters.AddWithValue("@id_grupo_investigacion", this.grupo.IdGrupoInvestigacion);
        }

        protected override string generarSQLParaListarPorId()
        {
            return "SELECT id_grupo_investigacion, acronimo, nombre FROM grupo_investigacion WHERE id_grupo_investigacion=" + grupo.IdGrupoInvestigacion;
        }

        protected override void limpiarObjetoDelResultSet()
        {
            this.grupo = null;
        }
    }
}
