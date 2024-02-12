using MvcCoreAdoNet.Models;
using System;
using System.Collections.Generic;  // Agrega este espacio de nombres
using System.Data;
using System.Data.SqlClient;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryHospital
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryHospital()
        {
            // Asegúrate de tener la cadena de conexión correcta y actualizada
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Hospital> GetHospitales()
        {
            try
            {
                string sql = "SELECT * FROM HOSPITAL";
                this.com.CommandType = CommandType.Text;
                this.com.CommandText = sql;
                this.cn.Open();
                this.reader = this.com.ExecuteReader();
                List<Hospital> hospitalesList = new List<Hospital>();
                while (this.reader.Read())
                {
                    Hospital hospital = new Hospital();
                    hospital.IdHospital = Convert.ToInt32(this.reader["HOSPITAL_COD"]);
                    hospital.Nombre = this.reader["NOMBRE"].ToString();
                    hospital.Direccion = this.reader["DIRECCION"].ToString();
                    hospital.Telefono = this.reader["TELEFONO"].ToString();
                    hospital.Camas = Convert.ToInt32(this.reader["NUM_CAMA"]);
                    hospitalesList.Add(hospital);
                }
                this.reader.Close();
                return hospitalesList;
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes imprimir el error o lanzar una excepción personalizada
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            finally
            {
                this.cn.Close();
            }
        }

        public Hospital FindHospitalById(int idhospital)
        {
            string sql = "select * from HOSPITAL where HOSPITAL_COD=@IDHOSPITAL";
            SqlParameter pamIdHospital = new SqlParameter("@IDHOSPITAL", idhospital);
            this.com.Parameters.Add(pamIdHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();
            Hospital hospital = new Hospital();
            hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            hospital.Nombre = this.reader["NOMBRE"].ToString();
            hospital.Direccion = this.reader["DIRECCION"].ToString();
            hospital.Telefono = this.reader["TELEFONO"].ToString();
            hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());
            this.reader.Close();
            this.com.Parameters.Clear();
            this.cn.Close();
            return hospital;

        }

        public int InsertHospital( Hospital hospital )
        {
            string sql = "insert into HOSPITAL values(@idhospital, @nombre," +
                "@direccion,@telefono,@camas)";
            SqlParameter pamId = new SqlParameter("@idhospital", hospital.IdHospital);
            SqlParameter pamNombre = new SqlParameter("@nombre", hospital.Nombre);
            SqlParameter pamDireccion = new SqlParameter("@direccion", hospital.Direccion);
            SqlParameter pamTelefono = new SqlParameter("@telefono", hospital.Direccion);
            SqlParameter pamCamas = new SqlParameter("@camas", hospital.Camas);
            this.com.Parameters.Add(pamId );
            this.com.Parameters.Add(pamNombre );
            this.com.Parameters.Add(pamDireccion );
            this.com.Parameters.Add(pamTelefono );
            this.com.Parameters.Add(pamCamas );
            this.com.CommandType= CommandType.Text;
            this.com.CommandText= sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return af;
        }

        public int UpdateHospital(Hospital hospital)
        {
            string sql = "UPDATE HOSPITAL SET NOMBRE=@nombre, DIRECCION=@direccion" +
                ", TELEFONO=@telefono, NUM_CAMA=@camas " +
                "WHERE HOSPITAL_COD=@idhospital";
            //CREAMOS Y AÑADIMOS LOS PARAMETROS
            //VAMOS A REALIZARLO TOOD EN UNO, AÑADIR Y CREAR EL PARAMETRO
            this.com.Parameters.AddWithValue("@idhospital", hospital.IdHospital);
            this.com.Parameters.AddWithValue("@nombre", hospital.Nombre);
            this.com.Parameters.AddWithValue("direccion", hospital.Direccion);
            this.com.Parameters.AddWithValue("@telefono", hospital.Telefono);
            this.com.Parameters.AddWithValue("@camas", hospital.Camas);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return af;
        }
        public void DeleteHospital(int idhospital)
        {
            string sql = "DELETE FROM HOSPITAL WHERE HOSPITAL_COD = @IDHOSPITAL";
            this.com.Parameters.AddWithValue("@IDHOSPITAL", idhospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int result = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
