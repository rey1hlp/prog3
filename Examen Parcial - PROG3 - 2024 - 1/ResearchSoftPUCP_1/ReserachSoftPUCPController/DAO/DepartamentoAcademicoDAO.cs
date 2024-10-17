using ResearchSoftPUCPModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserachSoftPUCPController.DAO
{
    public interface DepartamentoAcademicoDAO
    {
        int? insertar(DepartamentoAcademico depa);
        int? modificar(DepartamentoAcademico depa);
        int? eliminar(int id);

        BindingList<DepartamentoAcademico> listarTodosDepas();
        DepartamentoAcademico listarPorIdDepa(int id);
    }
}
