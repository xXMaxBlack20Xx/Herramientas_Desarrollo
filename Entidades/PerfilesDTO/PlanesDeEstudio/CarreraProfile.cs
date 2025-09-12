/*
 * Perfil de AutoMapper para Planes de Estudio.
 *
 * - Esta clase hereda de AutoMapper.Profile y su CONSTRUCTOR debe llamarse exactamente igual que la clase
 *   (PlanEstudiosProfile). En ese constructor se configuran las reglas de mapeo.
 *
 * - CreateMap<E_PlanEstudio, PlanEstudioDTO>() registra el mapeo entre la ENTIDAD de dominio
 *   (usada por EF Core para la base de datos) y el DTO (usado en la capa de presentación/servicios).
 *
 * - ReverseMap() habilita el mapeo en ambos sentidos (Entidad ↔ DTO), evitando declarar dos mapas separados.
 *
 * - Si alguna propiedad difiere en nombre o formato, ajusta con ForMember(...).
 *
 * - Recuerda registrar este perfil en DI (ejemplo):
 *     builder.Services.AddAutoMapper(typeof(PlanEstudiosProfile).Assembly);
 */

using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.Carreras;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.PerfilesDTO.PlanesDeEstudio
{
    public class CarreraProfile : Profile
    {
        public CarreraProfile() 
        {
            CreateMap<CarreraDTO, E_Carrera>().ReverseMap();
        }
    }
}
