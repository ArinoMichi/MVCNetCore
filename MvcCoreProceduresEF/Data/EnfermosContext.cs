using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Models;

namespace MvcCoreProceduresEF.Data
{
    public class EnfermosContext :DbContext
    {
        public EnfermosContext(DbContextOptions<EnfermosContext> options) : base(options) { }
        public DbSet<Enfermo> Enfermos { get; set; }

    }
}
