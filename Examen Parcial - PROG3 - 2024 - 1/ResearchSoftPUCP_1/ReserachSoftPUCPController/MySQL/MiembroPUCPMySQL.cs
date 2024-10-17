using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using ResearchSoftPUCPDBManager;
using ResearchSoftPUCPModel;
using ReserachSoftPUCPController.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ReserachSoftPUCPController.MySQL
{
    public class MiembroPUCPMySQL : DAOImplBase, MiembroPUCPDAO
    {
        private MiembroPUCP miembro;
        private string sql_filtro;
        private string nombreCodigo;

        public MiembroPUCPMySQL() : base("miembro_pucp")
        {
            miembro = null;
            sql_filtro = null;
        }


        //INSERTAR
        public int? insertar(MiembroPUCP miembro)
        {
            this.miembro = miembro;
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
        public int? modificar(MiembroPUCP miembro)
        {
            this.miembro = miembro;
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
        public int? eliminar(int miembro)
        {
            this.miembro = new MiembroPUCP();
            this.miembro.IdMiembroPUCP = miembro;
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
            int id= lector.GetInt32("id_miembro_pucp");
            char tipo = lector.GetChar("fid_tipo_miembro_pucp");
            string codigo = lector.GetString("codigo_pucp");
            string nombre = lector.GetString("nombre");
            string apaterno = lector.GetString("apellido_paterno");
            string dedicacion="";
            Decimal craest=0;
            if (tipo == 'P')
            {
                this.miembro = new Profesor();
                dedicacion = lector.GetString("dedicacion");
            }
            else
            {
                this.miembro = new Estudiante();
                craest = lector.GetDecimal("CRAEST");
            }
            miembro.IdMiembroPUCP = id;
            miembro.CodigoPUCP = codigo;
            miembro.Nombre = nombre;
            miembro.ApellidoPaterno = apaterno;

            if (miembro is Estudiante estudiante) estudiante.CRAEST = (double) craest;
            else if (miembro is Profesor profe) profe.Dedicacion = dedicacion;
        }

        public BindingList<MiembroPUCP> listarTodosMiembros(string nombreCodigo)
        {
            BindingList<MiembroPUCP> lista = new BindingList<MiembroPUCP>();
            this.nombreCodigo = nombreCodigo;
            foreach (var a in base.listarTodos(null))
                if (a is MiembroPUCP miembro) lista.Add(miembro);
            return lista;
        }

        protected override void agregarObjetoALaLista(BindingList<object> lista, MySqlDataReader lector)
        {
            this.instanciarObjetoDelResultSet(this.Lector);
            lista.Add(this.miembro);
        }

        public BindingList<MiembroPUCP> listarPorIdGrupoInvestigacion(int id_grupo)
        {
            BindingList<Object> lista = new BindingList<Object>();
            this.Comando = new MySqlCommand();
            try
            {
                this.abrirConexion();
                string sql = "CALL LISTAR_INTEGRANTES_X_ID_GRUPO_INVESTIGACION("+id_grupo+")";
                this.colocarSQLenComando(sql);
                this.onListarTodosDespuesDeColocarComandoSQL();
                this.ejecutarConsultaEnBD(sql);
                while (this.Lector.Read())
                {
                    agregarObjetoALaLista(lista, this.Lector);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.cerrarConexion();
            }
            BindingList<MiembroPUCP> listaMiem = new BindingList<MiembroPUCP>();
            foreach (var a in lista)
                if (a is MiembroPUCP miembro) listaMiem.Add(miembro);
            return listaMiem;
        }

        public MiembroPUCP listarPorIdMiembro(int id)
        {
            this.miembro = new MiembroPUCP();
            this.miembro.IdMiembroPUCP = id;
            base.obtenerPorId();
            return this.miembro;
        }

        protected override string obtenerProyeccionParaSelect()
        {
            return "id_miembro_pucp,fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno,dedicacion,CRAEST";
        }

        protected override string obtenerPredicadoParaListarTodos()
        {
            if (this.sql_filtro != null)
                return this.sql_filtro;
            return "";
        }

        protected override string generarSQLParaListarTodos(int? limite)
        {
            return "call LISTAR_MIEMBROS_PUCP_X_NOMBRE_CODIGOPUCP('"+ nombreCodigo + "')";
        }

        //public BindingList<MiembroPUCP> buscarDepa(string titulo)
        //{
        //    if (titulo != null && titulo != "")
        //        this.sql_filtro = " where titulo like \"%" + titulo + "%\"";
        //    BindingList<MiembroPUCP> lista = this.listarTodosMiembros(nombreCodigo);
        //    this.sql_filtro = null;
        //    return lista;
        //}


        protected override void incluirValorParametrosParaObtenerPorId()
        {
            this.Comando.Parameters.AddWithValue("@id_miembro_pucp", this.miembro.IdMiembroPUCP);
        }

        protected override string generarSQLParaListarPorId()
        {
            return "";
        }

        protected override void limpiarObjetoDelResultSet()
        {
            this.miembro = null;
        }
    }
}
