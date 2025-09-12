/*
    Aqui usaremos Data anottacion para poder hacer un Data Transfer Object en el cual nos servira para los formularios 
    y validar reglas adicionales y mapearlas con el AutoMapper
*/

using System.ComponentModel.DataAnnotations;

namespace Entidades.DTO.PlanesDeEstudio.Carreras
{
    public class CarreraDTO
    {
        public int IdCarrera { get; set; }

        [Required(ErrorMessage = "Debe Capturar la Clave de la Carrra")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "La clave de la carrera debe contener exactamente  3 digitos.")]
        public string ClaveCarrera { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe Capturar el nombre de la carrera")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-,.']+$", ErrorMessage = "El nombre de de la carrera solo puede contener letras, espacios y las siguientes caracteres especiales: - ,  ' ")]
        public string NombreCarrera { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe Capturar el alias de la carrera")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-,.']+$", ErrorMessage = "El alias de la carrera solo puede contener letras, espacios y las siguientes caracteres especiales: - ,  ' ")]
        public string AliasCarrera { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe de especificar el coodinador")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se permiten numeros del 0 al 9.")]
        public string IdCoordinador { get; set; } = string.Empty;

        public bool EstadoCarrera { get; set; }
    }
}
