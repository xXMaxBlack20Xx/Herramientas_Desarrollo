/*
    Aqui haremos las configuracion de E_PlanDeEstudio  con Fluent API y EF Core
*/

using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio
{
    public class PlanEstudioConfiguration : IEntityTypeConfiguration<E_PlanEstudio>
    {
        public void Configure(EntityTypeBuilder<E_PlanEstudio> builder)
        {
            // Configuracion para el esquema CEF de la base de datos
            builder.ToTable("PlanesEstudio", "CEF");

            // Llave primaria y autoincremento
            builder.HasKey(pe => pe.IdPlanEstudio);
            builder.Property(pe => pe.IdPlanEstudio).ValueGeneratedOnAdd();

            // Configurar propiedades requeridas y longitudes
            builder.Property(pe => pe.PlanEstudio).IsRequired().HasMaxLength(6);
            builder.Property(pe => pe.PlanEstudio).IsUnicode().HasAnnotation("Relational:Name", "UK_PlanEstudio");

            builder.Property(pe => pe.FechaCreacion).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.Property(pe => pe.TotalCreditos).IsRequired();

            builder.Property(p => p.CreditosOptativos).IsRequired().HasMaxLength(10);

            builder.Property(p => p.CreditosObligatorios).IsRequired().HasMaxLength(10);

            builder.Property(p => p.PerfilDeIngreso).IsRequired().HasMaxLength(2048);

            builder.Property(p => p.PerfelDeEgreso).IsRequired().HasMaxLength(2048);

            builder.Property(p => p.CampoOcupacional).IsRequired().HasMaxLength(2048);

            builder.Property(p => p.Comentarios).IsRequired().HasMaxLength(2048);

            builder.Property(pe => pe.EstadoPlanEstudio).IsRequired().HasDefaultValue(true);
        }
    }
}
