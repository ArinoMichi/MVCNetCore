﻿using NetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresOracle : IRepositoryDoctores
    {
        private DataTable tablaDoctores;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = cn;
            string sql = "SELECT * FROM DOCTOR";
            OracleDataAdapter ad = new OracleDataAdapter(sql, this.cn);
            this.tablaDoctores = new DataTable();
            ad.Fill(this.tablaDoctores);
        }

        public List<Doctor> GetDoctores()
        {
            //LA VENTAJA DE LINQ ES QUE SE ABSTRAE DEL ORIGEN DE DATOS
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach(var row in consulta)
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

        public void InsertDoctor(int id, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "INSERT INTO DOCTOR VALUES (:idhospital,:iddoctor," +
                ":apellido,:especialidad,:salario)";
            //ORACLE TIENE EN CUENTA EL ORDEN DE LOS PARAMETROS NO EL NOMBRE
            OracleParameter paramHospitalCod = new OracleParameter(":idhospital", idHospital);
            this.com.Parameters.Add(paramHospitalCod);
            OracleParameter paramDoctorNo = new OracleParameter(":iddoctor", id);
            this.com.Parameters.Add(paramDoctorNo);
            OracleParameter paramApellido = new OracleParameter(":apellido", apellido);
            this.com.Parameters.Add(paramApellido);
            OracleParameter paramEspecialidad = new OracleParameter(":especialidad", especialidad);
            this.com.Parameters.Add(paramEspecialidad);
            OracleParameter paramSalario = new OracleParameter(":salario", salario);
            this.com.Parameters.Add(paramSalario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
