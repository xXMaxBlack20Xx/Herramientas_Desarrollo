

using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Datos.IRepositorios.PlanesDeEstudio
{
    public interface IPlanDeEstudioRepositorio
    {
        public Task<ResultadoAcciones> InsertarPlanEstudio(E_PlanEstudio planDeEstudio);
    }
}
