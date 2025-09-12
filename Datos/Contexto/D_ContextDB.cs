using Entidades.Configuraciones.PlanesDeEstudio;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;

namespace Datos.Contexto
{
    public class D_ContextDB : DbContext
    {
        public D_ContextDB(DbContextOptions<D_ContextDB> options) : base(options) { }

        // DbSets para las entidades

        public DbSet<E_Carrera> Carreras { get; set; }

        public DbSet<E_PlanEstudio> PlanEstudios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CarreraConfiguration());
            modelBuilder.ApplyConfiguration(new PlanEstudioConfiguration());
        }
    }
}
