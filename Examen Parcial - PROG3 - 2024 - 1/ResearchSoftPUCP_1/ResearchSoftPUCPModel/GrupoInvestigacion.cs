using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchSoftPUCPModel
{
    public class GrupoInvestigacion
    {
        private int _idGrupoInvestigacion;
        private BindingList<MiembroPUCP> _integrantes;
        private DepartamentoAcademico _departamentoAcademico;
        private string _nombre;
        private string _acronimo;
        private TipoInvestigacion _tipoInvestigacion;
        private bool _poseeLaboratorio;
        private bool _poseeEquipamientoEspecializado;
        private bool _poseeAmbienteTrabajo;
        private DateTime _fechaFundacion;
        private string _descripcion;
        private double _presupuestoAnualDesignado;
        private byte[] _foto;
        public int IdGrupoInvestigacion { get => _idGrupoInvestigacion; set => _idGrupoInvestigacion = value; }
        public BindingList<MiembroPUCP> Integrantes { get => _integrantes; set => _integrantes = value; }
        public DepartamentoAcademico DepartamentoAcademico { get => _departamentoAcademico; set => _departamentoAcademico = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Acronimo { get => _acronimo; set => _acronimo = value; }
        public TipoInvestigacion TipoInvestigacion { get => _tipoInvestigacion; set => _tipoInvestigacion = value; }
        public bool PoseeLaboratorio { get => _poseeLaboratorio; set => _poseeLaboratorio = value; }
        public bool PoseeEquipamientoEspecializado { get => _poseeEquipamientoEspecializado; set => _poseeEquipamientoEspecializado = value; }
        public bool PoseeAmbienteTrabajo { get => _poseeAmbienteTrabajo; set => _poseeAmbienteTrabajo = value; }
        public DateTime FechaFundacion { get => _fechaFundacion; set => _fechaFundacion = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public double PresupuestoAnualDesignado { get => _presupuestoAnualDesignado; set => _presupuestoAnualDesignado = value; }
        public byte[] Foto { get => _foto; set => _foto = value; }
    }
}
