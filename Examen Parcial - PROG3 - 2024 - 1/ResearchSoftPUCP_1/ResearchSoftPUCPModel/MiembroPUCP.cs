using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchSoftPUCPModel
{
    public class MiembroPUCP
    {
        private int _idMiembroPUCP;
        private string _codigoPUCP;
        private string _nombre;
        private string _apellidoPaterno;
        public int IdMiembroPUCP { get => _idMiembroPUCP; set => _idMiembroPUCP = value; }
        public string CodigoPUCP { get => _codigoPUCP; set => _codigoPUCP = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string ApellidoPaterno { get => _apellidoPaterno; set => _apellidoPaterno = value; }

        public string NombreCompleto
        {
            get { return _nombre + " " + _apellidoPaterno;}
        }
        public string Tipo
        {
            get { 
                if (this is Profesor)
                {
                    return "Profesor";
                }
                else if (this is Estudiante)
                {
                    return "Estudiante";
                }
                return "Desconocido";
            }
        }
        public string DedicacionCRAEST
        {
            get
            {
                if (this is Profesor profesor)
                {
                    return profesor.Dedicacion;
                }
                else if (this is Estudiante estudiante)
                {
                    return estudiante.CRAEST.ToString("N2");
                }
                return "Desconocido";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is MiembroPUCP otroMiembro)
            {
                return this.IdMiembroPUCP == otroMiembro.IdMiembroPUCP;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return CodigoPUCP.GetHashCode();
        }
    }
}
