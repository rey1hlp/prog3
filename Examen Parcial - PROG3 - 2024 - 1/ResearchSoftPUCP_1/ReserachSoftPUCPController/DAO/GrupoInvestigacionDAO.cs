using ResearchSoftPUCPModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserachSoftPUCPController.DAO
{
    public interface GrupoInvestigacionDAO
    {
        int? insertar(GrupoInvestigacion grupo);
        int? modificar(GrupoInvestigacion grupo);
        int? eliminar(int id);

        BindingList<GrupoInvestigacion> listarPorNombreAcronimo(string acronimo);
        GrupoInvestigacion obtenerPorID(int id);
    }
}
