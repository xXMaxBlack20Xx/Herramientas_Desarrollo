/*using Entidades.DTO.PlanesDeEstudio.Carreras;
using Microsoft.AspNetCore.Components;
using Servicios.IRepositorios.PlanesDeEstudio;

namespace HerramientasDesarrollo.Areas.PlanesDeEstudio.Carreras
{
    public partial class CrearCarrera : ComponentBase
    {
        [Inject] public ICarreraServicios CarreraServicios { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Inject] public ILogger<CrearCarrera> Logger { get; set; } = default!;

        protected CarreraDTO nuevaCarreraDTO = new() { EstadoCarrera = true };
        protected bool guardando = false;
        protected string? errorGuardar;

        protected async Task GuardarNuevaCarreraAsync()
        {
            errorGuardar = null;

            Logger.LogInformation("Click Guardar → DTO: {@dto}", nuevaCarreraDTO);

            if (string.IsNullOrWhiteSpace(nuevaCarreraDTO.ClaveCarrera) ||
                string.IsNullOrWhiteSpace(nuevaCarreraDTO.NombreCarrera))
            {
                errorGuardar = "La clave y el nombre son obligatorios.";
                return;
            }

            guardando = true;
            try
            {
                var res = await CarreraServicios.InsertarCarrera(nuevaCarreraDTO);
                if (res?.Resultado == true)
                {
                    var msg = string.Join(" | ", res?.Mensajes ?? ["No fue posible guardar la carrera."]);
                    errorGuardar = msg;
                    Logger.LogWarning("InsertarCarrera FAIL: {msg}", msg);
                }
                else
                {
                    errorGuardar = string.Join(" | ", res?.Mensajes ?? ["No fue posible guardar la carrera."]);
                }
            }
            catch (Exception ex)
            {
                errorGuardar = $"Error inesperado: {ex.Message}";
                Logger.LogError(ex, "Excepción en GuardarNuevaCarreraAsync");
            }
            finally
            {
                guardando = false;
                StateHasChanged();
            }
        }
    }
}
*/