using Microsoft.EntityFrameworkCore;
using PracticaCubos.Models;

namespace PracticaCubos.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<Compra> Compras { get; set; }

    }
}
