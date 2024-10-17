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
    public class integrante_grupo_investigacionMySQL : DAOImplBase,integrante_grupo_investigacionDAO
    {
        private int? idMiembro;
        private int? idGrupo;
        private string sql_filtro;
        public integrante_grupo_investigacionMySQL() : base("integrante_grupo_investigacion")
        {
            idMiembro = null;
            idGrupo = null;
            sql_filtro = null;
        }


        //INSERTAR
        public int? insertar(int idMiembro, int? idGrupo)
        {
            this.idMiembro = idMiembro;
            this.idGrupo = idGrupo;
            this.RetornarLlavePrimaria = true;
            return this.insertar();
        }
        protected override string generarSQLParaInsercion()
        {
            return "CALL INSERTAR_INTEGRANTE_GRUPO_INVESTIGACION(@_id_integrante_grupo_investigacion,@_fid_grupo_investigacion,@_fid_miembro_pucp)";
        }
        protected override void incluirValorParametrosParaInsercion()
        {
            this.Comando.Parameters.Add(new MySqlParameter("@_id_integrante_grupo_investigacion", MySqlDbType.Int32));
            this.Comando.Parameters["@_id_integrante_grupo_investigacion"].Direction = ParameterDirection.Output; // Indica que es un parámetro de salida

            // Parámetros de entrada
            this.Comando.Parameters.AddWithValue("@_fid_grupo_investigacion", this.idGrupo);
            this.Comando.Parameters.AddWithValue("@_fid_miembro_pucp", this.idMiembro);
            
        }
        //////////////////////////////////////////////////////////
        ///
        protected override void agregarObjetoALaLista(BindingList<object> lista, MySqlDataReader lector)
        {
            throw new NotImplementedException();
        }

        protected override string generarSQLParaEliminacion()
        {
            throw new NotImplementedException();
        }

        protected override string generarSQLParaListarPorId()
        {
            throw new NotImplementedException();
        }

        protected override string generarSQLParaListarTodos(int? limite)
        {
            throw new NotImplementedException();
        }

        protected override string generarSQLParaModificacion()
        {
            throw new NotImplementedException();
        }

        protected override void incluirValorParametrosParaEliminacion()
        {
            throw new NotImplementedException();
        }
        protected override void incluirValorParametrosParaModificacion()
        {
            throw new NotImplementedException();
        }

        protected override void incluirValorParametrosParaObtenerPorId()
        {
            throw new NotImplementedException();
        }

        protected override void instanciarObjetoDelResultSet(MySqlDataReader lector)
        {
            throw new NotImplementedException();
        }

        protected override void limpiarObjetoDelResultSet()
        {
            throw new NotImplementedException();
        }

        protected override string obtenerProyeccionParaSelect()
        {
            throw new NotImplementedException();
        }
    }
}
