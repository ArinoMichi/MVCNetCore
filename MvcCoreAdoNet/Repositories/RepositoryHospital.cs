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
    }
}
