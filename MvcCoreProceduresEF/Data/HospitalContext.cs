using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Models;

namespace MvcCoreProceduresEF.Data
{
    public class HospitalContext :DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }
        public DbSet<Enfermo> Enfermos { get; set; }

        public DbSet<Doctor> Doctores { get; set; }

        public DbSet<ViewEmpleado> ViewEmpleados { get; set; }

        public DbSet<Trabajador> Trabajadores { get; set; }

    }
}
