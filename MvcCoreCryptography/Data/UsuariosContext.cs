using Microsoft.EntityFrameworkCore;
using MvcCoreCryptography.Models;

namespace MvcCoreCryptography.Data
{
    public class UsuariosContext :DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
