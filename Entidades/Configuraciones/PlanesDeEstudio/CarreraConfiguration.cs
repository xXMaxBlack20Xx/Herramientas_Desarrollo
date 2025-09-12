/*
    Aqui haremos la configuracion de la entidad E_Carrera con Fluent API y EF Core
*/

using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio
{
    public class CarreraConfiguration : IEntityTypeConfiguration<E_Carrera>
    {
        public void Configure(EntityTypeBuilder<E_Carrera> builder)
        {
            // Configuracion para el esquema CEF de la base de datos
            builder.ToTable("Carreras", "CEF");

            // Llave primaria y AutoIncremento
            builder.HasKey(c => c.IdCarrera);
            builder.Property( c => c.ClaveCarrera).ValueGeneratedOnAdd();

            // Configurar propiedades requeridas y longitudes
            builder.Property( c => c.ClaveCarrera).IsRequired().HasMaxLength(3);
            builder.Property(c => c.ClaveCarrera).IsUnicode().HasAnnotation("Relational:Name", "UK_ClaveCarrera");

            builder.Property(c => c.NombreCarrera).IsRequired().HasMaxLength(50);
            builder.Property(c => c.NombreCarrera).IsUnicode().HasAnnotation("Relational:Name", "UK_NombreCarrera");

            builder.Property(c => c.AliasCarrera).IsRequired().HasMaxLength(50);
            builder.Property(c => c.AliasCarrera).IsUnicode().HasAnnotation("Relational:Name", "UK_AliasCarrera");

            builder.Property(c => c.EstadoCarrera).IsRequired().HasDefaultValue(true);
        }
    }
}