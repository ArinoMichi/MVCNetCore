using Microsoft.EntityFrameworkCore;
using MvcCoreEnfermosEF.Models;

namespace MvcCoreEnfermosEF.Data
{
    public class EnfermoContext : DbContext
    {
        public EnfermoContext(DbContextOptions<EnfermoContext> options) : base(options) { }

        public DbSet<Enfermo> Enfermos { get; set; }
    }
}
