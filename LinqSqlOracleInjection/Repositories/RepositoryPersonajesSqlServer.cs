using LinqSqlOracleInjection.Models;
using System.Data;
using System.Data.SqlClient;

namespace LinqSqlOracleInjection.Repositories
{
    public class RepositoryPersonajesSqlServer : IRepositoryPersonajes
    {

        private DataTable tablaPersonajes;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryPersonajesSqlServer()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;

            this.tablaPersonajes = new DataTable();
            string sql = "SELECT * FROM PERSONAJES";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, this.cn);
            adapter.Fill(tablaPersonajes);
        }


        public List<Personaje> GetPersonajes()
        {
            var consulta = from datos in this.tablaPersonajes.AsEnumerable()
                           select datos;
            List<Personaje> personajes = new List<Personaje>();
            foreach(var row in consulta)
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
            string sql = "INSERT INTO PERSONAJES VALUES(@ID, @NOMBRE,@IMAGEN)";
            this.com.Parameters.AddWithValue("@ID", personaje.Id);
            this.com.Parameters.AddWithValue("@NOMBRE", personaje.Nombre);
            this.com.Parameters.AddWithValue("@IMAGEN", personaje.Imagen);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
