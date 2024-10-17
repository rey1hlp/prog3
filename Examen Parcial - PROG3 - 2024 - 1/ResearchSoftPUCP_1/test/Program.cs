using ResearchSoftPUCPModel;
using ReserachSoftPUCPController.DAO;
using ReserachSoftPUCPController.MySQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    internal static class Program
    {
        static void Main()
        {
            //DepartamentoAcademicoDAO depa = new DepartamentoAcademicoMySQL();

            //BindingList<DepartamentoAcademico> departamentos = depa.listarTodosDepas();

            //foreach (DepartamentoAcademico a in departamentos)
            //{
            //    System.Console.WriteLine(a.Nombre);
            //}

            //MiembroPUCPDAO miembro = new MiembroPUCPMySQL();
            //BindingList<MiembroPUCP> miem = miembro.listarTodosMiembros("luis");

            //foreach (MiembroPUCP a in miem)
            //{
            //    Console.WriteLine(a.Tipo + " " + a.ApellidoPaterno);
            //}

            //GrupoInvestigacionDAO grupo = new GrupoInvestigacionMySQL();
            //GrupoInvestigacion a = new GrupoInvestigacion();
            //a.Nombre = "GAAAAA";
            //grupo.insertar(a);
            //BindingList<GrupoInvestigacion> aa = grupo.listarPorNombreAcronimo("GAAAA");
            //GrupoInvestigacion a = grupo.obtenerPorID(1);
            
            MiembroPUCPDAO dao = new MiembroPUCPMySQL();
            BindingList<MiembroPUCP> miem = dao.listarPorIdGrupoInvestigacion(1);

            string xd = "xd";
        }
    }
}
