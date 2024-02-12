using MvcCoreAdoNet.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryDoctores
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryDoctores()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
        }

        public List<Doctor> GetDoctores()
        {
            string sql = "SELECT * FROM DOCTOR";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Doctor> doctores = new List<Doctor>();
            while (this.reader.Read())
            {
                Doctor doctor = new Doctor();
                doctor.IdHospital = Convert.ToInt32(this.reader["HOSPITAL_COD"]);
                doctor.IdDoctor = Convert.ToInt32(this.reader["DOCTOR_NO"]);
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = Convert.ToInt32(this.reader["SALARIO"]);
                doctores.Add(doctor);
            }
            this.cn.Close();
            this.reader.Close();
            return doctores;
        }

        public List<Doctor> FindDoctorPorEspecialidad(string especialidad)
        {
            string sql = "SELECT * FROM DOCTOR WHERE ESPECIALIDAD = @especialidad";
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            List<Doctor> doctores = new List<Doctor>();

            while (this.reader.Read())
            {
                Doctor doctor = new Doctor();
                doctor.IdHospital = Convert.ToInt32(this.reader["HOSPITAL_COD"]);
                doctor.IdDoctor = Convert.ToInt32(this.reader["DOCTOR_NO"]);
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = Convert.ToInt32(this.reader["SALARIO"]);
                doctores.Add(doctor);
            }

            this.reader.Close();
            this.com.Parameters.Clear();
            this.cn.Close();

            return doctores;
        }

        public List<string> GetEspecialidades()
        {
            string sql = "SELECT DISTINCT ESPECIALIDAD FROM DOCTOR";
            this.com.CommandText = sql;
            this.com.CommandType = CommandType.Text;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<string> especialidades = new List<string>();
            while (this.reader.Read())
            {
                especialidades.Add(this.reader["ESPECIALIDAD"].ToString());
            }
            this.reader.Close();
            this.com.Parameters.Clear();
            this.cn.Close();
            return especialidades;
        }

    }
}
