/*
    En esta parte haremos la interfaz de repositorio carrera
*/

using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Datos.IRepositorios.PlanesDeEstudio
{
    public interface ICarreraRepositorios
    {
        // Aqui lo que se esta utilizando es la entidad E_Carrera no el modelo de DTO
        public Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera);

        public Task<ResultadoAcciones> BorrarCarrera(int idCarrera);

        public Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera);

        public Task<E_Carrera?> BuscarCarrera(int idCarrera);

        public Task<IEnumerable<E_Carrera>> ListarCarreras();

        //Este es un metodo sobrecargado que permite buscar carreras por un criterio de 
        public Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null);

        public Task<bool> ExisteClaveCarrera(string clave);

        public Task<bool> ExisteNombreCarrera(string nombre);

        public Task<bool> ExisteAliasCarrera(string alias);

        public Task<IEnumerable<E_Carrera>> ListarCarrerasPorPlanEstudio(int value);
    }
}