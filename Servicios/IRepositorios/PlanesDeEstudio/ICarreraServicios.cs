

using Entidades.DTO.PlanesDeEstudio.Carreras;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Servicios.IRepositorios.PlanesDeEstudio
{
    public interface ICarreraServicios
    {
        Task<ResultadoAcciones> InsertarCarrera(CarreraDTO carreraDTO);

        Task<ResultadoAcciones> BorrarCarrera(int idCarrera);

        Task<ResultadoAcciones> ModificarCarrera(CarreraDTO carreraDTO);

        Task<ResultadoAcciones<T>> ObtenerCarrera<T>(int idCarrera) where T : class;

        Task<IEnumerable<E_Carrera>> ListarCarreras();

        Task<IEnumerable<E_Carrera>> ListarCarreras(string criterioBusqueda);

        Task<List<CarreraDTO>> ObtenerCarrerasPorPlanEstudioAsync(int idPlanEstudio);
    }
}