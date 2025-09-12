/*
    Aqui usaremos Data anottacion para poder hacer un Data Transfer Object en el cual nos servira para los formularios 
    y validar reglas adicionales y mapearlas con el AutoMapper
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.DTO.PlanesDeEstudio.PlanEstudios
{
    public class PlanEstudioDTO
    {
        public int IdPlanEstudio { get; set; }

        [Required(ErrorMessage = "Debe capturar el plan de estudios.")]
        [RegularExpression(@"\d{4}-[124]$", ErrorMessage = "El formato debe ser AAAA-D, Donde AAAA es un año y D es 1, 2, o 4.")]
        public string PlanEstudio { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe capturar el total de creditos.")]
        [Range(1, int.MaxValue, ErrorMessage = "El total de creditos debe ser mayor a 0.")]
        public int TotalCreditos { get; set; }

        [Required(ErrorMessage = "Debe capturar los creditos optativos.")]
        [Range(0, int.MaxValue, ErrorMessage = "Los creditos optativos deben ser 0 o mayor.")]
        public int CreditosOptativos { get; set; }

        [Required(ErrorMessage = "Debe capturar los creditos obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Los creditos obligatorio deben ser mayor a 0.")]
        public int CreditoObligatorios { get; set; }

        [Required(ErrorMessage = "Debe capturar el perfil de ingreso.")]
        public string PerfilDeIngreso { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe capturar el perfil de egreso.")]
        public string CampoOcupacional { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe capturar los comentarios")]
        public string Comentarios { get; set; } = string.Empty;

        [Required]
        public bool EstadoPlanEstudio { get; set; }

        // Propiedad de navegacion
        // Llave Foranea Muchos PlanEstudios pertenecen a una carrera
        [ForeignKey("IdCarrera")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecciona una carrera")]
        public int IdCarrera { get; set; }

        // Llave foranea para Nivel Academico
        //[Range(1, int.MaxValue, ErrorMessage = "Selecciona un nivel academico")]
        //public int IdNivelAcademico { get; set; } = 0;
    }
}
