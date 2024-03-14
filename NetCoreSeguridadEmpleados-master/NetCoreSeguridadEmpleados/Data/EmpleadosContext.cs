using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadEmpleados.Models;

namespace NetCoreSeguridadEmpleados.Data
{
    public class EmpleadosContext : DbContext
    {
        public EmpleadosContext
            (DbContextOptions<EmpleadosContext> options)
            : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
    }
}
