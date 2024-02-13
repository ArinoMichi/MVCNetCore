using MvcNetCoreLinqToSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcNetCoreLinqToSql.Repositories
{
    public class RepositoryEnfermos
    {
        private DataTable tablaEnfermos;

        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryEnfermos()
        {
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = sa; Password =MCSD2023";
            string sql = "select * from ENFERMO";
            //CONSULTA CON LINQ
            SqlDataAdapter adEn = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();
            adEn.Fill(this.tablaEnfermos);
            //CONSULTA CON ADO NET
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
        }

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           select datos;
            List<Enfermo> enfermos = new List<Enfermo>();

            foreach(var row in consulta)
            {
                Enfermo enfermo = new Enfermo
                {
                    Inscipcion = row.Field<int>("INSCRIPCION"),
                    Apellido= row.Field<string>("APELLIDO"),
                    Direccion= row.Field<string>("DIRECCION") ,
                    FechaNacimiento= row.Field<DateTime>("FECHA_NAC") ,
                    Sexo= row.Field<string>("S"),
                    NSS= row.Field<int>("NSS")
                };
                enfermos.Add(enfermo);
            }
            return enfermos;
        }

        public Enfermo FindEnfermo(int id)
        {
            System.Console.WriteLine(id);
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<int>("INSCRIPCION") == id
                           select datos;
            var row = consulta.FirstOrDefault();
            Enfermo enf = new Enfermo
            {
                Inscipcion = row.Field<int>("INSCRIPCION"),
                Apellido = row.Field<string>("APELLIDO"),
                Direccion = row.Field<string>("DIRECCION"),
                FechaNacimiento = row.Field<DateTime>("FECHA_NAC"),
                Sexo = row.Field<string>("S"),
                NSS = row.Field<int>("NSS")
            };
            return enf;
        }

        public void DeleteEnfermo(int id)
        {
            string sql = "DELETE FROM ENFERMO WHERE INSCRIPCION = @id";
            this.com.Parameters.AddWithValue("@id", id);

            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }


    }
}
