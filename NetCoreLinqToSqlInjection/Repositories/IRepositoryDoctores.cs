using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        void InsertDoctor(int id, string apellido, string especialidad,
            int salario, int idHospital);
        List<Doctor> GetDoctoresEspecialidad(string especialidad);

        void DeleteDoctor(int id);

        void UpdateDoctor(int idDoc, int idHos, string apellido, string especialidad, int salario);

        Doctor GetDoctorById(int id);
    }
}
