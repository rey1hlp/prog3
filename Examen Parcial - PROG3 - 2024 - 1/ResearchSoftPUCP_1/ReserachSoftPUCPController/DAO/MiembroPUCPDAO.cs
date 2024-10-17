using ResearchSoftPUCPModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserachSoftPUCPController.DAO
{
    public interface MiembroPUCPDAO
    {
        int? insertar(MiembroPUCP miembro);
        int? modificar(MiembroPUCP miembro);
        int? eliminar(int id);

        BindingList<MiembroPUCP> listarTodosMiembros(string nombreCodigo);
        BindingList<MiembroPUCP> listarPorIdGrupoInvestigacion(int id_grupo);

        MiembroPUCP listarPorIdMiembro(int id);
    }
}
