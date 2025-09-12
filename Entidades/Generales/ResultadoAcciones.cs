/*
    Esta clase se utiliza para devolver el resultado de una accion en los repositorios
*/

namespace Entidades.Generales
{
    public class ResultadoAcciones
    {
        public List<string> Mensajes { get; set; } = [];

        public bool Resultado { get; set; } = true;
    }

    public class ResultadoAcciones<T> : ResultadoAcciones
    {
        public T? Data { get; set; }

        public required object? Entidad { get; set; }
    }
}
