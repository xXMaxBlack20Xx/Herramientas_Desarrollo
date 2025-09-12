/*
 * Capa de Negocio para Carreras.
 *
 * ¿Qué hace?
 * - Orquesta los casos de uso de Carreras (CRUD): Insertar, Modificar, Borrar, Obtener por Id y Listar.
 * - Valida datos de entrada con reglas básicas (longitudes, requeridos) y reglas de unicidad consultando la BD.
 * - Interactúa con la capa de Datos a través de ICarreraRepositorios (inyección de dependencias).
 * - Maneja errores con try/catch y devuelve un contrato uniforme mediante ResultadoAcciones / ResultadoAcciones<T>.
 * - Centraliza los mensajes para la UI en la propiedad Mensajes, sin exponer detalles de EF ni de la BD.
 *
 * Patrón de flujo:
 *   1) Validar entrada  →  2) (si pasa) llamar repositorio  →  3) Construir resultado (éxito o mensajes de error).
 *
 * Nota:
 * - Esta clase trabaja con entidades (E_Carrera) y retorna objetos ResultadoAcciones para que la capa de presentación
 *   pueda mostrar mensajes consistentes al usuario.
*/

using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Negocios.Repositorios.PlanesDeEstudio
{
    public class CarreraNegocios(ICarreraRepositorios carreraReposiorios)
    {
        private readonly ICarreraRepositorios _carreraRepositorios = carreraReposiorios;

        public async Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera)
        {
            if (carrera == null)
            {
                return new ResultadoAcciones
                {
                    Mensajes = { "La carrera no tiene los datos necesarios para agregarla al sistema" },
                    Resultado = false,
                };
            }

            var resultadoValidacion = await ValidarCarrera(carrera);

            if (!resultadoValidacion.Resultado)
            {
                return resultadoValidacion;
            }

            try
            {
                var dataRes = await _carreraRepositorios.InsertarCarrera(carrera);
                return dataRes;
            }
            catch (Exception ex)
            {
                return new ResultadoAcciones
                {
                    Mensajes = { "Ocurrio un error inesperado al insertar al carrera: ", ex.Message + "." },
                    Resultado = false
                };
            }
        }

        public async Task<ResultadoAcciones> BorrarCarrera(int idCarrera)
        {
            if (idCarrera <= 0)
            {
                return new ResultadoAcciones
                {
                    Mensajes = ["El identificardor de ka carrera no es valido."],
                    Resultado = false,
                };
            }

            try
            {
                await _carreraRepositorios.BorrarCarrera(idCarrera);
                return new ResultadoAcciones { Resultado = true };
            }
            catch (KeyNotFoundException ex)
            {
                return new ResultadoAcciones
                {
                    Mensajes = { ex.Message + "." },
                    Resultado = false,
                };
            }
        }

        public async Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera)
        {
            if (carrera == null || carrera.IdCarrera <= 0)
            {
                return new ResultadoAcciones
                {
                    Mensajes = { "Los datos de la carrera son invalidos." },
                    Resultado = false,
                };
            }

            var resultadoValidacion = await ValidarCarrera((E_Carrera)carrera, esModificacion: true);

            if (!resultadoValidacion.Resultado)
            {
                return resultadoValidacion;
            }

            try
            {
                await _carreraRepositorios.ModificarCarrera(carrera);
                return resultadoValidacion;
            }
            catch (KeyNotFoundException ex)
            {
                return new ResultadoAcciones
                {
                    Mensajes = { "Ocurrio un error inesperado al modificar la carrera", ex.Message + "." },
                    Resultado = false,
                };
            }

            catch (Exception ex)
            {
                return new ResultadoAcciones
                {
                    Mensajes = { "Ocurrio un error inesperado al modificar la carrera", ex.Message + "." },
                    Resultado = false,

                };
            }
        }

        public async Task<ResultadoAcciones<E_Carrera>> ObtenerCarreraPorId(int idCarrera)
        {
            if (idCarrera <= 0)
            {
                return new ResultadoAcciones<E_Carrera>
                {
                    Mensajes = { "El identificador de la carrera no es válido." },
                    Resultado = true,
                    Entidad = null
                };
            }

            try
            {
                var entidad = await _carreraRepositorios.BuscarCarrera(idCarrera);
                if (entidad is null)
                {
                    return new ResultadoAcciones<E_Carrera>
                    {
                        Resultado = false,
                        Mensajes = { $"No se encontró la carrera con ID {idCarrera}." },
                        Entidad = null
                    };
                }

                return new ResultadoAcciones<E_Carrera>
                {
                    Resultado = true,
                    Data = entidad,
                    Entidad = entidad
                };
            }
            catch (Exception ex)
            {
                return new ResultadoAcciones<E_Carrera>
                {
                    Resultado = false,
                    Mensajes = { "Ocurrió un error inesperado al obtener la carrera.", ex.Message },
                    Entidad = null
                };
            }
        }


        public async Task<IEnumerable<E_Carrera>> ListarCarreras()
        {
            try
            {
                return await _carreraRepositorios.ListarCarreras();
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null)
        {
            try
            {
                var criterio = string.IsNullOrWhiteSpace(criterioBusqueda)
                    ? null
                    : criterioBusqueda.Trim();
                return await _carreraRepositorios.ListarCarreras(criterio);
            }
            catch (Exception)
            {
                return [];
            }

        }

        public async Task<IEnumerable<E_Carrera>> ListarCarrerasPorPlanEstudio(int? idPlanEstudio = null)
        {
            try
            {
                if (idPlanEstudio is null || idPlanEstudio <= 0)
                {
                    return await _carreraRepositorios.ListarCarreras();
                }

                return await _carreraRepositorios.ListarCarrerasPorPlanEstudio(idPlanEstudio.Value);
            }
            catch
            {
                return [];
            }
        }

        public async Task<ResultadoAcciones> ValidarCarrera(E_Carrera carrera, bool esModificacion = false)
        {
            ResultadoAcciones resultado = new();

            // Reglas básicas (sin tocar BD)
            ValidarClaveCarrera(resultado, carrera.ClaveCarrera);
            ValidarNombreCarrera(resultado, carrera.NombreCarrera);
            ValidarAliasCarrera(resultado, carrera.AliasCarrera);

            // Si ya falló por reglas básicas, no pegues a la BD
            if (resultado.Mensajes.Count > 0)
            {
                resultado.Resultado = false;
                return resultado;
            }

            // Reglas de unicidad (consultan BD)
            if (esModificacion)
            {
                await ValidarUnicidadClaveCarrera(resultado, carrera.ClaveCarrera, carrera.IdCarrera);
                await ValidarUnicidadNombreCarrera(resultado, carrera.NombreCarrera, carrera.IdCarrera);
                await ValidarUnicidadAliasCarrera(resultado, carrera.AliasCarrera, carrera.IdCarrera);
            }
            else
            {
                await ValidarUnicidadClaveCarrera(resultado, carrera.ClaveCarrera);
                await ValidarUnicidadNombreCarrera(resultado, carrera.NombreCarrera);
                await ValidarUnicidadAliasCarrera(resultado, carrera.AliasCarrera);
            }

            resultado.Resultado = resultado.Mensajes.Count == 0;
            return resultado;
        }

        // =================== Helpers de reglas básicas (sin BD) ===================

        private static void ValidarClaveCarrera(ResultadoAcciones resultado, string? claveCarrera)
        {
            if (string.IsNullOrWhiteSpace(claveCarrera))
            {
                resultado.Mensajes.Add("La clave de la carrera es obligatoria.");
                return;
            }

            var clave = claveCarrera.Trim();

            if (clave.Length > 3)
            {
                resultado.Mensajes.Add("La clave de la carrera no debe exceder los 3 caracteres.");
            }
        }

        private static void ValidarNombreCarrera(ResultadoAcciones resultado, string? nombreCarrera)
        {
            if (string.IsNullOrWhiteSpace(nombreCarrera))
            {
                resultado.Mensajes.Add("El nombre de la carrera es obligatorio.");
                return;
            }

            var nombre = nombreCarrera.Trim();
            if (nombre.Length > 50)
                resultado.Mensajes.Add("El nombre de la carrera no debe exceder los 50 caracteres.");
        }

        private static void ValidarAliasCarrera(ResultadoAcciones resultado, string? aliasCarrera)
        {
            if (string.IsNullOrWhiteSpace(aliasCarrera))
            {
                resultado.Mensajes.Add("El alias de la carrera es obligatorio.");
                return;
            }

            var alias = aliasCarrera.Trim();
            if (alias.Length > 50)
                resultado.Mensajes.Add("El alias de la carrera no debe exceder los 50 caracteres.");
        }

        // =================== Helpers de unicidad (con BD) ===================
        // Regla: si es modificación y el valor existente pertenece al MISMO Id, no es duplicado.

        private async Task ValidarUnicidadClaveCarrera(ResultadoAcciones resultado, string? claveCarrera, int? idExcluido = null)
        {
            if (string.IsNullOrWhiteSpace(claveCarrera)) return;

            var clave = claveCarrera.Trim();
            bool existe = await _carreraRepositorios.ExisteClaveCarrera(clave);
            if (!existe) return;

            if (idExcluido.HasValue)
            {
                var actual = await _carreraRepositorios.BuscarCarrera(idExcluido.Value);
                if (actual != null && string.Equals(actual.ClaveCarrera?.Trim(), clave, StringComparison.OrdinalIgnoreCase))
                    return; // es el mismo registro, no marcar duplicado
            }

            resultado.Mensajes.Add("La clave de la carrera ya existe.");
        }

        private async Task ValidarUnicidadNombreCarrera(ResultadoAcciones resultado, string? nombreCarrera, int? idExcluido = null)
        {
            if (string.IsNullOrWhiteSpace(nombreCarrera)) return;

            var nombre = nombreCarrera.Trim();
            bool existe = await _carreraRepositorios.ExisteNombreCarrera(nombre);
            if (!existe) return;

            if (idExcluido.HasValue)
            {
                var actual = await _carreraRepositorios.BuscarCarrera(idExcluido.Value);
                if (actual != null && string.Equals(actual.NombreCarrera?.Trim(), nombre, StringComparison.OrdinalIgnoreCase))
                    return;
            }

            resultado.Mensajes.Add("El nombre de la carrera ya existe.");
        }

        private async Task ValidarUnicidadAliasCarrera(ResultadoAcciones resultado, string? aliasCarrera, int? idExcluido = null)
        {
            if (string.IsNullOrWhiteSpace(aliasCarrera)) return;

            var alias = aliasCarrera.Trim();
            bool existe = await _carreraRepositorios.ExisteAliasCarrera(alias);
            if (!existe) return;

            if (idExcluido.HasValue)
            {
                var actual = await _carreraRepositorios.BuscarCarrera(idExcluido.Value);
                if (actual != null && string.Equals(actual.AliasCarrera?.Trim(), alias, StringComparison.OrdinalIgnoreCase))
                    return;
            }

            resultado.Mensajes.Add("El alias de la carrera ya existe.");
        }

    }
}
