using MvcCoreEFMultiplesBBDD.Models;

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        Task<List<Empleado>> GetEmpleadosAsync();

        Task<Empleado> FindEmpleadosAsync(int EmpNo);
    }
}
