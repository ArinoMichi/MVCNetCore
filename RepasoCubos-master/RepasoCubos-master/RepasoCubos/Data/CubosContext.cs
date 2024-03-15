using Microsoft.EntityFrameworkCore;
using RepasoCubos.Models;

namespace RepasoCubos.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<VistaCubos> VistaCubos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ModelPaginacionCubos> ModelCubosPaginados { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar ModelPaginacionCubos como una entidad sin clave primaria
            modelBuilder.Entity<ModelPaginacionCubos>().HasNoKey();
        }
    }
}
