using Microsoft.AspNetCore.Http.HttpResults;
using NetCoreLinqToSqlInjection.Models;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

#region PROCEDURES

//PROCEDIMIENTO PARA ELIMINAR DOCTOR
    //create procedure SP_DELETE_DOCTOR
    //(@iddoctor int)
    //as
    //    delete from DOCTOR where DOCTOR_NO = @iddoctor
    //go

//PROCEDIMIENTO PARA ACTUALIZAR DOCTOR

    //create procedure SP_UPDATE_DOCTOR
    //(@iddoctor int, @idhos int, @apellido varchar(50),@especialidad varchar(50), @salario int)
    //as
    //    update DOCTOR
    //    where DOCTOR_NO=@iddoctor
    //go

#endregion




namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer : IRepositoryDoctores
    {
        private DataTable tablaDoctores;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.tablaDoctores = new DataTable();
            string sql = "SELECT * FROM DOCTOR";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, this.cn);
            adapter.Fill(tablaDoctores);
        }

        public void DeleteDoctor(int id)
        {
            this.com.Parameters.AddWithValue("@iddoctor", id);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_DELETE_DOCTOR";

            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
        public void UpdateDoctor(int idDoc, int idHos, string apellido, string especialidad, int salario)
        {
            this.com.Parameters.AddWithValue("@iddoctor", idDoc);
            this.com.Parameters.AddWithValue("@idhos", idHos);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_UPDATE_DOCTOR";

            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }


        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD")
                };
                doctores.Add(doc);
            }
            return doctores;
        }


        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD").ToUpper() == especialidad.ToUpper()
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Doctor> doctores = new List<Doctor>();
                foreach (var row in consulta)
                {
                    Doctor doc = new Doctor
                    {
                        IdDoctor = row.Field<int>("DOCTOR_NO"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Especialidad = row.Field<string>("ESPECIALIDAD"),
                        Salario = row.Field<int>("SALARIO"),
                        IdHospital = row.Field<int>("HOSPITAL_COD")
                    };
                    doctores.Add(doc);
                }
                return doctores;
            }
        }

        public void InsertDoctor(int id, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "INSERT INTO DOCTOR VALUES(@IDHOSPITAL, @IDDOCTOR, " +
                "@APELLIDO, @ESPECIALIDAD, @SALARIO)";
            this.com.Parameters.AddWithValue("@IDHOSPITAL", idHospital);
            this.com.Parameters.AddWithValue("@IDDOCTOR", id);
            this.com.Parameters.AddWithValue("@APELLIDO", apellido);
            this.com.Parameters.AddWithValue("@ESPECIALIDAD", especialidad);
            this.com.Parameters.AddWithValue("@SALARIO", salario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }

        public Doctor GetDoctorById(int id)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == id
                           select datos;

            if (consulta.Any())
            {
                var row = consulta.First();
                Doctor doc = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD")
                };
                return doc;
            }
            else
            {
                return null;
            }
        }
    }
}
