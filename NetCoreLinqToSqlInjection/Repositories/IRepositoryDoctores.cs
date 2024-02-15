using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        void InsertDoctor(int id, string apellido, string especialidad,
            int salario, int idHospital);
        List<Doctor> GetDoctoresEspecialidad(string especialidad);
    }
}
