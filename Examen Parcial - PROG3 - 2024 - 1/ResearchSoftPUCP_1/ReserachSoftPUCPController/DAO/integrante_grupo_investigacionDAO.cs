using ResearchSoftPUCPModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserachSoftPUCPController.DAO
{
    public interface integrante_grupo_investigacionDAO
    {
        int? insertar(int idMiembro, int? idGrupo);
    }
}
