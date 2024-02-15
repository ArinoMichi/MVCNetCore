using LinqSqlOracleInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LinqSqlOracleInjection.Repositories
{
    public class RepositoryPersonajesOracle : IRepositoryPersonajes
    {
        private DataTable tablaDoctores;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryPersonajesOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True; User Id=SYSTEM; Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = cn;

            string sql = "SELECT * FROM PERSONAJES";
            OracleDataAdapter adapter = new OracleDataAdapter(sql, this.cn);
            this.tablaDoctores = new DataTable();
            adapter.Fill(this.tablaDoctores);
        }

        public void DeletePersonaje(int id)
        {
            throw new NotImplementedException();
        }

        public Personaje FindPersonajeById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Personaje> GetPersonajes()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Personaje> personajes = new List<Personaje>();
            foreach (var row in consulta)
            {
                Personaje personaje = new Personaje
                {
                    Id = row.Field<int>("IDPERSONAJE"),
                    Nombre = row.Field<string>("PERSONAJE"),
                    Imagen = row.Field<string>("IMAGEN")
                };
                personajes.Add(personaje);
            }
            return personajes;
        }

        public void InsertPersonaje(Personaje personaje)
        {
            string sql = "INSERT INTO PERSONAJES VALUES(:ID,:NOMBRE,:IMAGEN)";
            this.com.Parameters.Add(new OracleParameter(":ID", personaje.Id));
            this.com.Parameters.Add(new OracleParameter(":NOMBRE", personaje.Nombre));
            this.com.Parameters.Add(new OracleParameter(":IMAGEN", personaje.Imagen));

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void UpdatePersonaje(Personaje personaje)
        {
            throw new NotImplementedException();
        }
    }
}
