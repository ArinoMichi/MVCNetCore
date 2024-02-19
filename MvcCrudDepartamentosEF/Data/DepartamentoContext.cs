using Microsoft.EntityFrameworkCore;
using MvcCrudDepartamentosEF.Models;

namespace MvcCrudDepartamentosEF.Data
{
    public class DepartamentoContext :DbContext
    {
        public DepartamentoContext(DbContextOptions<DepartamentoContext> options) : base(options) { }

        public DbSet<Departamento> Departamentos { get; set; }

    }
}
