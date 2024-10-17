using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchSoftPUCPDBManager
{
    public abstract class DAOImplBase
    {
        private string nombre_tabla;
        private MySqlConnection conexion;
        private MySqlTransaction transaccion;
        private MySqlCommand comando;
        private MySqlDataReader lector;
        private bool mostrarSentenciaSQL;
        private bool retornarLlavePrimaria;
        private bool usarTransaccion;

        protected string Nombre_tabla { get => nombre_tabla; set => nombre_tabla = value; }
        protected MySqlConnection Conexion { get => conexion; set => conexion = value; }
        protected MySqlTransaction Transaccion { get => transaccion; set => transaccion = value; }
        protected MySqlCommand Comando { get => comando; set => comando = value; }
        protected MySqlDataReader Lector { get => lector; set => lector = value; }
        protected bool MostrarSentenciaSQL { get => mostrarSentenciaSQL; set => mostrarSentenciaSQL = value; }
        protected bool RetornarLlavePrimaria { get => retornarLlavePrimaria; set => retornarLlavePrimaria = value; }
        protected bool UsarTransaccion { get => usarTransaccion; set => usarTransaccion = value; }

        protected DAOImplBase(string nombre_tabla)
        {
            Nombre_tabla = nombre_tabla;
            Conexion = null;
            Transaccion = null;
            Comando = null;
            Lector = null;
            MostrarSentenciaSQL = true;
            RetornarLlavePrimaria = false;
            UsarTransaccion = true;
        }

        protected void abrirConexion()
        {
            this.Conexion = DBManager.Instance.Conexion;
            this.Conexion.Open();
        }

        protected void cerrarConexion()
        {
            if (this.Conexion != null)
            {
                this.Conexion.Close();
            }
        }

        protected void iniciarTransaccion()
        {
            this.abrirConexion();
            this.Transaccion = this.Conexion.BeginTransaction();
        }

        protected void comitarTransaccion()
        {
            this.Transaccion.Commit();
            this.Transaccion = null;
        }

        protected void rollbackTransaccion()
        {
            if (this.Transaccion != null)
            {
                this.Transaccion.Rollback();
            }
            this.Transaccion = null;
        }

        protected void colocarSQLenComando(string sql)
        {
            this.Comando.Connection = this.Conexion;
            this.Comando.CommandText = sql;
            this.Comando.CommandType = System.Data.CommandType.Text;
        }
        protected void ejecutarConsultaEnBD(string sql)
        {
            this.Lector = this.Comando.ExecuteReader();
        }

        private int ejecuta_DML(Tipo_Operacion tipo_Operacion)
        {
            int resultado = 0;
            try
            {
                if (this.usarTransaccion)
                {
                    this.iniciarTransaccion();
                }
                this.Comando = new MySqlCommand();
                string sql = "";
                switch (tipo_Operacion)
                {
                    case Tipo_Operacion.INSERTAR:
                        sql = this.generarSQLParaInsercion();
                        break;
                    case Tipo_Operacion.MODIFICAR:
                        sql = this.generarSQLParaModificacion();
                        break;
                    case Tipo_Operacion.ELIMINAR:
                        sql = this.generarSQLParaEliminacion();
                        break;
                }
                this.colocarSQLenComando(sql);
                switch (tipo_Operacion)
                {
                    case Tipo_Operacion.INSERTAR:
                        this.incluirValorParametrosParaInsercion();
                        break;
                    case Tipo_Operacion.MODIFICAR:
                        this.incluirValorParametrosParaModificacion();
                        break;
                    case Tipo_Operacion.ELIMINAR:
                        this.incluirValorParametrosParaEliminacion();
                        break;
                }
                this.Comando.ExecuteNonQuery();
                if (this.retornarLlavePrimaria)
                {
                    int id = this.retornarUltimoAutoGenerado();
                    resultado = id;
                }
                if (this.usarTransaccion)
                {
                    this.comitarTransaccion();
                }
            }
            catch (Exception ex)
            {
                if (this.usarTransaccion)
                {
                    this.rollbackTransaccion();
                }
                throw new Exception(ex.Message);
            }
            finally
            {
                if (this.usarTransaccion)
                {
                    this.cerrarConexion();
                }
            }
            return resultado;
        }
        protected int insertar()
        {
            return this.ejecuta_DML(Tipo_Operacion.INSERTAR);
        }

        protected int modificar()
        {
            return this.ejecuta_DML(Tipo_Operacion.MODIFICAR);
        }

        protected int eliminar()
        {
            return this.ejecuta_DML(Tipo_Operacion.ELIMINAR);
        }

        protected abstract string generarSQLParaInsercion();

        protected abstract string generarSQLParaModificacion();

        protected abstract string generarSQLParaEliminacion();

        protected abstract void incluirValorParametrosParaEliminacion();
        protected abstract void incluirValorParametrosParaModificacion();
        protected abstract void incluirValorParametrosParaInsercion();

        private int retornarUltimoAutoGenerado()
        {
            this.Comando.CommandText = "SELECT LAST_INSERT_ID();";
            return Convert.ToInt32(this.Comando.ExecuteScalar());
        }

        protected BindingList<Object> listarTodos(int? limite)
        {
            BindingList<Object> lista = new BindingList<Object>();
            this.Comando = new MySqlCommand();
            try
            {
                this.abrirConexion();
                string sql = this.generarSQLParaListarTodos(limite);
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
            return lista;
        }

        protected virtual string obtenerPredicadoParaListarTodos()
        {
            return ""; //por defecto no se incluye ningún predicado, pero esta función puede redefinirse en las clases derivadas
        }

        protected virtual void onListarTodosDespuesDeColocarComandoSQL()
        {
            //
        }

        protected abstract string generarSQLParaListarTodos(int? limite);

        protected abstract string obtenerProyeccionParaSelect();
        protected abstract void agregarObjetoALaLista(BindingList<object> lista, MySqlDataReader lector);

        public void obtenerPorId()
        {
            this.Comando = new MySqlCommand();
            try
            {
                this.abrirConexion();
                string sql = this.generarSQLParaListarPorId();
                this.incluirValorParametrosParaObtenerPorId();
                this.colocarSQLenComando(sql);
                this.ejecutarConsultaEnBD(sql);
                if (this.Lector.Read())
                    this.instanciarObjetoDelResultSet(this.Lector);
                else
                    this.limpiarObjetoDelResultSet();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.cerrarConexion();
            }
        }

        protected abstract string generarSQLParaListarPorId();

        protected abstract void instanciarObjetoDelResultSet(MySqlDataReader lector);

        protected abstract void limpiarObjetoDelResultSet();

        protected abstract void incluirValorParametrosParaObtenerPorId();

    }
}
