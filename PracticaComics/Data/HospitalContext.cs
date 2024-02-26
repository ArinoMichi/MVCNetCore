using Microsoft.EntityFrameworkCore;
using PracticaComics.Models;

namespace PracticaComics.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Comic> Comics { get; set; }
    }
}
