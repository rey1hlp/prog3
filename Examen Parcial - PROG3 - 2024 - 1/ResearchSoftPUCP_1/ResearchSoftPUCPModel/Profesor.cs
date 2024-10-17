using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchSoftPUCPModel
{
    public class Profesor : MiembroPUCP
    {
        private string _dedicacion;
        private bool _activo;
        public string Dedicacion { get => _dedicacion; set => _dedicacion = value; }
        public bool Activo { get => _activo; set => _activo = value; }
    }
}
