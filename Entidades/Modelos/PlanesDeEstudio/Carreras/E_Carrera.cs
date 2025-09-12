/* 
    Aqui vamos a estar definiendo la tabla de Carreras y PlanesEstudio 
    es una propiedad de navegacion que establese una relacion de 
    1:N (Osea de uno a muchos)
*/

namespace Entidades.Modelos.PlanesDeEstudio.Carreras
{
    public class E_Carrera
    {
        public int IdCarrera { get; set; }

        // Si ClaveCarrera es STRING (ej. "ING-SIS-23"), NO debe ser ValueGeneratedOnAdd en la config.
        // Si quieres que la DB la genere (IDENTITY), cámbiala a int. Aquí asumo string:
        public string ClaveCarrera { get; set; } = string.Empty;

        public string NombreCarrera { get; set;} = string.Empty;

        public string AliasCarrera { get; set; } = string.Empty;

        //public int? IdCoordinador { get; set; }

        public bool EstadoCarrera { get; set; } = true;

        // Navegación (plural)
        public ICollection<E_PlanEstudio> PlanEstudios { get; set; } = new List<E_PlanEstudio>();
    }
}
