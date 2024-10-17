using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResearchSoftPUCPDBManager
{
    public class DBManager
    {
        private static string ARCHIVO_CONFIGURACION = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "base_de_datos.json");

        private MySqlConnection conexion;
        private string base_de_datos;
        private string nombre_del_host;
        private string puerto;
        private string usuario;
        private string contraseña;
        private static DBManager dbManager = null;

        private DBManager()
        {
            //para que no se pueda crear instancias fuera de la clase
        }

        public static DBManager Instance
        { //Instance es una propiedad, funciona como si fuese una "atributo público"
          //¿Cómo se utiliza?  dbManager = DBManager.Instance; <- se ejecuta el get
            get
            {
                if (dbManager == null)
                    createInstance();
                return dbManager;
            }
        }

        private static void createInstance()
        {
            if (dbManager == null)
                dbManager = new DBManager();
        }

        public MySqlConnection Conexion
        {
            get
            {
                this.leer_archivo_configuracion();
                string cadena_de_conexion = this.obtener_cadena_de_conexion();
                this.conexion = new MySqlConnection(cadena_de_conexion);
                return this.conexion;
            }
        }

        public class MySQL_json
        {
            public string base_de_datos { get; set; }
            public string nombre_de_host { get; set; }
            public string puerto { get; set; }
            public string usuario { get; set; }
            public string contraseña { get; set; }
        }

        private void leer_archivo_configuracion()
        { // para poder usar el soporte a JSON, se debe agregar la referencia
          // System.Text.Json
          // además, se debe incluir la referencia System.Memory
            string cadena_json = File.ReadAllText(ARCHIVO_CONFIGURACION); // para usar el File se realizó using System.IO;
            //incluir using System.Text.Json
            MySQL_json mySQL_Json = JsonSerializer.Deserialize<MySQL_json>(cadena_json);
            this.nombre_del_host = mySQL_Json.nombre_de_host;
            this.usuario = mySQL_Json.usuario;
            this.contraseña = mySQL_Json.contraseña;
            this.base_de_datos = mySQL_Json.base_de_datos;
            this.puerto = mySQL_Json.puerto;
        }

        private string obtener_cadena_de_conexion()
        {
            string cadena_de_conexion = "server=" + this.nombre_del_host + ";";
            cadena_de_conexion += "user=" + this.usuario + ";";
            cadena_de_conexion += "password=" + this.contraseña + ";";
            cadena_de_conexion += "port=" + this.puerto + ";";
            cadena_de_conexion += "database=" + this.base_de_datos + ";";
            return cadena_de_conexion;
        }
    }
}
