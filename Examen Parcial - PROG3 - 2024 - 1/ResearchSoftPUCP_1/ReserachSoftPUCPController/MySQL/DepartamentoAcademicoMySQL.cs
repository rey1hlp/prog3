using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReserachSoftPUCPController.DAO;
using ResearchSoftPUCPModel;
using MySql.Data.MySqlClient;
using ResearchSoftPUCPDBManager;

namespace ReserachSoftPUCPController.MySQL
{
    public class DepartamentoAcademicoMySQL : DAOImplBase, DepartamentoAcademicoDAO
    {
        private DepartamentoAcademico depa;
        private string sql_filtro;
        public DepartamentoAcademicoMySQL() : base("departamento_academico")
        {
            depa = null;
            sql_filtro = null;
        }


        //INSERTAR
        public int? insertar(DepartamentoAcademico depa)
        {
            this.depa = depa;
            return this.insertar();
        }
        protected override string generarSQLParaInsercion()
        {
            return "";
        }
        protected override void incluirValorParametrosParaInsercion()
        {
            //no hay un procedimiento
        }

        //MODIFICAR
        public int? modificar(DepartamentoAcademico depa)
        {
            this.depa = depa;
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
        public int? eliminar(int depa)
        {
            this.depa = new DepartamentoAcademico();
            this.depa.IdDepartamentoAcademico = depa;
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
            this.depa = new DepartamentoAcademico();
            depa.IdDepartamentoAcademico = lector.GetInt32("id_departamento_academico");
            depa.Nombre = lector.GetString("nombre");
        }

        public BindingList<DepartamentoAcademico> listarTodosDepas()
        {
            BindingList<DepartamentoAcademico> lista = new BindingList<DepartamentoAcademico>();
            foreach (var a in base.listarTodos(null)) {
                if (a is DepartamentoAcademico departamento) { 
                    lista.Add(departamento);
                }
            }
            return lista;
        }

        protected override void agregarObjetoALaLista(BindingList<object> lista, MySqlDataReader lector)
        {
            this.instanciarObjetoDelResultSet(this.Lector);
            lista.Add(this.depa);
        }

        public DepartamentoAcademico listarPorIdDepa(int id)
        {
            this.depa = new DepartamentoAcademico();
            this.depa.IdDepartamentoAcademico = id;
            base.obtenerPorId();
            return this.depa;
        }

        protected override string obtenerProyeccionParaSelect()
        {
            return "id_departamento_academico, nombre";
        }

        protected override string obtenerPredicadoParaListarTodos()
        {
            if (this.sql_filtro != null)
                return this.sql_filtro;
            return "";
        }

        protected override string generarSQLParaListarTodos(int? limite)
        {
            return "call LISTAR_DEPARTAMENTOS_ACADEMICOS_TODOS()";
        }

        public BindingList<DepartamentoAcademico> buscarDepa(string titulo)
        {
            if (titulo != null && titulo != "")
                this.sql_filtro = " where titulo like \"%" + titulo + "%\"";
            BindingList<DepartamentoAcademico> lista = this.listarTodosDepas();
            this.sql_filtro = null;
            return lista;
        }


        protected override void incluirValorParametrosParaObtenerPorId()
        {
            this.Comando.Parameters.AddWithValue("@id_departamento_academico", this.depa.IdDepartamentoAcademico);
        }

        protected override string generarSQLParaListarPorId()
        {
            return "";
        }

        protected override void limpiarObjetoDelResultSet()
        {
            this.depa = null;
        }
    }
}
