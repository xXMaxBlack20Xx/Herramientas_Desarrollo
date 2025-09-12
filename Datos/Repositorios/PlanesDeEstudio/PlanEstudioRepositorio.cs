
using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Datos.Repositorios.PlanesDeEstudio
{
    public class PlanEstudioRepositorio : IPlanDeEstudioRepositorio
    {
        private readonly D_ContextDB _context;

        public PlanEstudioRepositorio(D_ContextDB context) 
        {
            _context = context;
        }

        public async Task<ResultadoAcciones> InsertarPlanEstudio(E_PlanEstudio planDeEstudio)
        {
            var resultado = new ResultadoAcciones();

            try
            {
                await _context.PlanEstudios.AddAsync(planDeEstudio);
                await _context.SaveChangesAsync();

                resultado.Resultado = true;
                resultado.Mensajes = ["Plan de estudio insertado correctamente."];
            }
            catch (Exception ex)
            {
                resultado.Resultado = false;
                resultado.Mensajes = [$"Error al insertar plan de estudio: {ex.Message}"];
            }

            return resultado;
        }
    }
}