using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchSoftPUCPModel
{
    public class Estudiante : MiembroPUCP
    {
        private double _CRAEST;
        private bool _activo;
        public double CRAEST { get => _CRAEST; set => _CRAEST = value; }
        public bool Activo { get => _activo; set => _activo = value; }
    }
}
