using Microsoft.AspNetCore.Http.HttpResults;
using MvcCoreCrudDoctores.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;

namespace MvcCoreCrudDoctores.Repositories
{

#region PROCEDURES
    //Insertar Doctor
    //CREATE PROCEDURE SP_INSERTDOCTOR
    //  (@IDHOSPITAL INT, @APELLIDO NVARCHAR(50), @ESPECIALIDAD NVARCHAR(50), @SALARIO INT)
    //AS
    //    DECLARE @NUEVOID INT
    //    SELECT @NUEVOID = MAX(DOCTOR_NO) + 1 FROM DOCTOR
    //    INSERT INTO DOCTOR VALUES(@IDHOSPITAL, @NUEVOID, @APELLIDO, @ESPECIALIDAD, @SALARIO)
    //GO
#endregion

    public class RepositoryDoctores
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryDoctores()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string sql = "SELECT * FROM DOCTOR";
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();

            List<Doctor> doctores = new List<Doctor>();
            while (await this.reader.ReadAsync()) 
            {
                Doctor doctor = new Doctor();
                doctor.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctores.Add(doctor);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return doctores;
        }


        public async Task<Doctor> FindDoctorByIdAsync(int id)
        {
            string sql = "SELECT * FROM DOCTOR WHERE DOCTOR_NO=@id";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            Doctor doctor = null;
            if(await this.reader.ReadAsync())
            {
                doctor = new Doctor();
                doctor.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doctor.IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doctor.Apellido = this.reader["APELLIDO"].ToString();
                doctor.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(this.reader["SALARIO"].ToString());

            }
            else
            {
                //no encuentra el doctor
            }

            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return doctor;
        }

        public async Task InsertDoctorAsync(Doctor doc)
        {
            this.com.Parameters.AddWithValue("@IDHOSPITAL", doc.IdHospital);
            this.com.Parameters.AddWithValue("@APELLIDO", doc.Apellido);
            this.com.Parameters.AddWithValue("@ESPECIALIDAD", doc.Especialidad);
            this.com.Parameters.AddWithValue("@SALARIO", doc.Salario);

            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERTDOCTOR";
            await this.cn.OpenAsync();
            int af = await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateDoctorAsync(Doctor doc)
        {
            string sql = "UPDATE DOCTOR SET HOSPITAL_COD=@HOSPITAL_COD, APELLIDO=@APELLIDO, " +
                          "ESPECIALIDAD=@ESPECIALIDAD, SALARIO=@SALARIO WHERE DOCTOR_NO=@DOCTOR_NO";
            this.com.Parameters.AddWithValue("@HOSPITAL_COD", doc.IdHospital);
            this.com.Parameters.AddWithValue("@APELLIDO", doc.Apellido);
            this.com.Parameters.AddWithValue("@ESPECIALIDAD", doc.Especialidad);
            this.com.Parameters.AddWithValue("@SALARIO", doc.Salario);
            this.com.Parameters.AddWithValue("@DOCTOR_NO", doc.IdDoctor);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            int af = await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            string sql = "DELETE FROM DOCTOR WHERE DOCTOR_NO = @id";
            this.com.Parameters.AddWithValue("@id", id);

            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            int af = await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }


    }
}
