using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Models;

namespace MvcCorePaginacionRegistros.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options) { }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<VistaDepartamento> VistaDepartamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
