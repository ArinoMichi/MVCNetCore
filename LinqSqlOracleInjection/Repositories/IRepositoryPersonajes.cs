using LinqSqlOracleInjection.Models;

namespace LinqSqlOracleInjection.Repositories
{
    public interface IRepositoryPersonajes
    {
        List<Personaje> GetPersonajes();

        void InsertPersonaje(Personaje personaje);

        Personaje FindPersonajeById(int id);

        void DeletePersonaje(int id);
        void UpdatePersonaje(Personaje personaje);
        
    }
}
