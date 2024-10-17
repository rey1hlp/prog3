using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchSoftPUCPModel
{
    public class DepartamentoAcademico
    {
        private int _idDepartamentoAcademico;
        private string _nombre;
        private bool _activo;
        public int IdDepartamentoAcademico { get => _idDepartamentoAcademico; set => _idDepartamentoAcademico = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public bool Activo { get => _activo; set => _activo = value; }
    }
}