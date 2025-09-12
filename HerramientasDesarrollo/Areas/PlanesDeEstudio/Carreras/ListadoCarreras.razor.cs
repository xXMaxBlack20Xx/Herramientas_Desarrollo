using Entidades.DTO.PlanesDeEstudio.Carreras;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.AspNetCore.Components;

namespace HerramientasDesarrollo.Areas.PlanesDeEstudio.Carreras
{
    partial class ListadoCarreras : ComponentBase
    {
        [Inject] public Servicios.IRepositorios.PlanesDeEstudio.ICarreraServicios CarreraServicios { get; set; } = default!;

        private string criterioBusqueda = string.Empty;        // <-- asegúrate de tener este campo
        private List<E_Carrera> carrerasTodas = new();


        // Variables requeridasd insertar a la base de datos
        private bool mostrarModalCrear = false;
        private bool guardando = false;
        private string? errorGuardar = string.Empty;

        private CarreraDTO nuevaCarreraDTO = new()
        {
            EstadoCarrera = true
        };

        protected IEnumerable<E_Carrera> LstCarreras =>
            string.IsNullOrWhiteSpace(criterioBusqueda)
                ? carrerasTodas
                : carrerasTodas.Where(c =>
                    (c.ClaveCarrera ?? "").Contains(criterioBusqueda, StringComparison.OrdinalIgnoreCase) ||
                    (c.NombreCarrera ?? "").Contains(criterioBusqueda, StringComparison.OrdinalIgnoreCase) ||
                    (c.AliasCarrera ?? "").Contains(criterioBusqueda, StringComparison.OrdinalIgnoreCase));

        protected override async Task OnInitializedAsync()
        {
            carrerasTodas = (await CarreraServicios.ListarCarreras())?.ToList() ?? new();
        }

        protected async Task BorrarCarrera(int id)
        {
            var res = await CarreraServicios.BorrarCarrera(id);
            if (res?.Resultado == true)
            {
                carrerasTodas = (await CarreraServicios.ListarCarreras())?.ToList() ?? new();
                StateHasChanged();
            }
            else
            {
                Console.WriteLine(string.Join(" | ", res?.Mensajes ?? []));
            }
        }

        // Ahora los Modales de como se va a comportar
        protected void AbrirCrearCarrera()
        {
            nuevaCarreraDTO = new CarreraDTO { EstadoCarrera = true };
            errorGuardar = null;
            mostrarModalCrear = true;
        }

        protected void CerrarModalCrear()
        {
            mostrarModalCrear = false;
        }

        // El metodo de insert
        protected async Task<ResultadoAcciones> GuardarNuevaCarreraAsync()
        {
            errorGuardar = null;

            // Validaciones
            if(string.IsNullOrWhiteSpace(nuevaCarreraDTO.ClaveCarrera) ||
                string.IsNullOrWhiteSpace(nuevaCarreraDTO.NombreCarrera))
            {
                errorGuardar = "La clave y el nombre son obligatorios.";
                return new ResultadoAcciones();
            }

            // Validaciones de duplicado (Lado cliente)
            var coincidencias = await CarreraServicios.ListarCarreras(nuevaCarreraDTO.ClaveCarrera);

            if(coincidencias?.Any(c => string.Equals(c.ClaveCarrera, nuevaCarreraDTO.ClaveCarrera, 
                StringComparison.OrdinalIgnoreCase)) == true)
            {
                errorGuardar = $"Ya existe una carrera con la clave '{nuevaCarreraDTO.ClaveCarrera}'.";
                return new ResultadoAcciones();
            }

            guardando = true;

            try
            {
                var res = await CarreraServicios.InsertarCarrera(nuevaCarreraDTO);

                if (res?.Resultado == true)
                {
                    carrerasTodas = (await CarreraServicios.ListarCarreras())?.ToList() ?? new();
                    mostrarModalCrear = false;
                    StateHasChanged();
                }
                else
                {
                    errorGuardar = $"No fue posible guardar la carrera...";
                }
            }
            catch (Exception ex)
            {
                errorGuardar = $"Error inesperado: {ex.Message}";
            }
            finally
            {
                guardando = false;
            }
            return new ResultadoAcciones();
        }
    }
}