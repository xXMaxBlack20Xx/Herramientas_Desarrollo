/*
    Aqui basicamente ira el CRUD de nuestra aplicacion el cual tambien contara con la validacion de EF Core
*/

using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios.PlanesDeEstudio
{
    public class CarreraRepositorio(D_ContextDB contextDB) : ICarreraRepositorios
    {
        private readonly D_ContextDB _contextDB = contextDB;

        // Insertar
        public async Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones validacion = await ValidarCarrera(carrera);

            try
            {
                if (validacion.Resultado)
                {
                    await _contextDB.Carreras.AddAsync(carrera);
                    await _contextDB.SaveChangesAsync();
                }
                return validacion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string mensaje = "Ocurrio un error inesperado...";
                validacion.Mensajes.Add(mensaje);
                validacion.Resultado = false;
                return validacion;
            }
        }

        // Borrar
        public async Task<ResultadoAcciones> BorrarCarrera(int idCarrera)
        {
            var carreraBD = await _contextDB.Carreras.FindAsync(idCarrera);
            if (carreraBD == null)
            {
                ResultadoAcciones resultado = new ResultadoAcciones();
                resultado.Resultado = false;
                resultado.Mensajes.Add("No se encontro la carrera");
                throw new KeyNotFoundException($"Carrera con ID {idCarrera} no encontrada.");
            }

            _contextDB.Carreras.Remove(carreraBD);
            await _contextDB.SaveChangesAsync();
            return new ResultadoAcciones();
        }

        // Modificar
        public async Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones validacion = await ValidarCarrera(carrera);
            try
            {
                if (validacion.Resultado)
                {
                    var existente = await BuscarCarrera(carrera.IdCarrera);
                    if (existente == null)
                    {
                        validacion.Mensajes.Add($" - La carrera con ID {carrera.IdCarrera} no encontrada");
                        validacion.Resultado = false;
                        return validacion;
                    }
                    _contextDB.Carreras.Update(carrera);
                    await _contextDB.SaveChangesAsync();
                }
                return validacion;
            }
            catch (Exception)
            {
                string mensaje = "Ocurrio un error inesperado...";
                validacion.Mensajes.Add(mensaje);
                validacion.Resultado = false;
                return validacion;
            }
        }

        // Listar
        public async Task<IEnumerable<E_Carrera>> ListarCarreras()
        {
            return await _contextDB.Carreras.ToListAsync();
        }

        // Listar con Criterio de Busqueda
        public async Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null)
        {
            var query = _contextDB.Carreras.AsQueryable();

            if (!string.IsNullOrWhiteSpace(criterioBusqueda))
            {
                query = query.Where(c =>
                    c.ClaveCarrera.Contains(criterioBusqueda) ||
                    c.NombreCarrera.Contains(criterioBusqueda) ||
                    c.AliasCarrera.Contains(criterioBusqueda)
                );
            }
            return await query.ToListAsync();
        }

        // Buscar
        public  async Task<E_Carrera?> BuscarCarrera(int idCarrera)
        {
            return await _contextDB.Carreras.FindAsync(idCarrera);
        }

        // Metodos par validaciones
        public async Task<ResultadoAcciones> ValidarCarrera(E_Carrera carrera)
        {
            ResultadoAcciones resultado = new ResultadoAcciones();

            #region
            if (string.IsNullOrEmpty(carrera.ClaveCarrera))
            {
                resultado.Mensajes.Add("La clave de la carrera es obligatoria");
                resultado.Resultado = false;
            }
            else
            {
                if (carrera.ClaveCarrera.Length > 3)
                {
                    resultado.Mensajes.Add("La clave de la carrera no debe exceder los 10 caracteres");
                    resultado.Resultado = false;
                }

                if (await ExisteClaveCarrera(carrera.ClaveCarrera))
                {
                    resultado.Mensajes.Add("La clave de la carrera ya existe");
                    resultado.Resultado = false;
                }

            }
            #endregion

            #region Validacion Nombre
            if (string.IsNullOrWhiteSpace(carrera.NombreCarrera))
            {
                resultado.Mensajes.Add("El nombre de la carrera es obligatorio");
                resultado.Resultado = false;
            }
            else
            {
                if (carrera.NombreCarrera.Length > 100)
                {
                    resultado.Mensajes.Add("El nombre de la carrera no debe exceder los 50 caracteres");
                    resultado.Resultado = false;
                }
                if (await ExisteNombreCarrera(carrera.NombreCarrera))
                {
                    resultado.Mensajes.Add("El nombre de la carrera ya existe");
                    resultado.Resultado = false;
                }
            }
            #endregion

            #region Validacion Alias
            if (string.IsNullOrWhiteSpace(carrera.AliasCarrera))
            {
                resultado.Mensajes.Add("El alias de la carrera es obligatorio");
                resultado.Resultado = false;
            }
            else
            {
                if (carrera.AliasCarrera.Length > 100)
                {
                    resultado.Mensajes.Add("El alias de la carrera no debe exceder los 50 caracteres");
                    resultado.Resultado = false;
                }
                if (await ExisteAliasCarrera(carrera.AliasCarrera))
                {
                    resultado.Mensajes.Add("El alias de la carrera ya existe");
                    resultado.Resultado = false;
                }
            }
            #endregion

            return resultado;
        }

        //=====================================================================//

        public async Task<bool> ExisteClaveCarrera(string claveCarrera)
        {
            try
            {
                return await _contextDB.Carreras.AnyAsync(c => c.ClaveCarrera == claveCarrera);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExisteNombreCarrera(string nombreCarrera)
        {
            try
            {
                return await _contextDB.Carreras.AnyAsync(c => c.NombreCarrera == nombreCarrera);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExisteAliasCarrera(string aliasCarrera)
        {
            try
            {
                return await _contextDB.Carreras.AnyAsync(c => c.NombreCarrera == aliasCarrera);
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Verificar que la  interfaz si esta realizada correctamente
        public Task<IEnumerable<E_Carrera>> ListarCarrerasPorPlanEstudio(int value)
        {
            throw new NotImplementedException();
        }
    }
}
