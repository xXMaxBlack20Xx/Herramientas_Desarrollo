

using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.Carreras;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Negocios.Repositorios.PlanesDeEstudio;
using Servicios.IRepositorios.PlanesDeEstudio;

namespace Servicios.Repositorios.PlanesDeEstudio
{
    public class CarreraServicios(CarreraNegocios carreraNegocios, IMapper mapper) : ICarreraServicios
    {
        private readonly CarreraNegocios _carreraNegocios = carreraNegocios;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultadoAcciones> InsertarCarrera(CarreraDTO carreraDTO)
        {
            E_Carrera carrera = _mapper.Map<E_Carrera>(carreraDTO);
            return await _carreraNegocios.InsertarCarrera(carrera);
        }

        public async Task<ResultadoAcciones> BorrarCarrera(int idCarrera)
        {
            if (idCarrera == 0)
            {
                return new ResultadoAcciones
                {
                    Mensajes = { "El identificador de la carrera no es valido." },
                    Resultado = false
                };
            }
            return await _carreraNegocios.BorrarCarrera(idCarrera);
        }

        public async Task<ResultadoAcciones> ModificarCarrera(CarreraDTO carreraDTO)
        {
            E_Carrera carrera = _mapper.Map<E_Carrera>(carreraDTO);
            return await _carreraNegocios.ModificarCarrera(carrera);
        }

        public async Task<ResultadoAcciones<T>> ObtenerCarrera<T>(int idCarrera) where T : class
        {
            try
            {
                var carrera = await _carreraNegocios.ObtenerCarreraPorId(idCarrera);

                if (!carrera.Resultado || carrera.Entidad == null)
                {
                    return new ResultadoAcciones<T>
                    {
                        Mensajes = carrera.Mensajes,
                        Resultado = false,
                        Entidad = default(T)
                    };
                }

                //Mapear la entidad al DTO
                var dtoMapeado = _mapper.Map<T>(carrera.Entidad);

                return new ResultadoAcciones<T>
                {
                    Mensajes = carrera.Mensajes,
                    Entidad = dtoMapeado,
                    Resultado = true,
                };
            }
            catch (Exception ex)
            {
                return new ResultadoAcciones<T>
                {
                    Mensajes = ["Error inespirado al procesar la solicitud. " + ex.Message + " - " + (ex.InnerException?.Message ?? "")],
                    Resultado = false,
                    Entidad = default(T)
                };
            }
        }

        public async Task<IEnumerable<E_Carrera>> ListarCarreras()
        {
            return await _carreraNegocios.ListarCarreras();
        }

        public async Task<IEnumerable<E_Carrera>> ListarCarreras(string criterioBusqueda)
        {
            return await _carreraNegocios.ListarCarreras(criterioBusqueda);
        }

        // Aqui va un metodo que no tenemos 
        public async Task<List<CarreraDTO>> ObtenerCarrerasPorPlanEstudioAsync(int idPlanEstudio)
        {
            var carreras = await _carreraNegocios.ListarCarrerasPorPlanEstudio(idPlanEstudio);
            return _mapper.Map<List<CarreraDTO>>(carreras);
        }

    }
}