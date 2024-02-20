using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;
using System.Data.Common;
using System.Data;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace MvcCoreProceduresEF.Repositories
{
    #region STORED PROCEDURES
    //CREATE PROCEDURE SP_TODOS_DOCTORES
    //AS
    //   SELECT* FROM DOCTOR
    //GO

    //CREATE PROCEDURE SP_INCREMENTAR_SALARIO_DOCTOR
    //(@INCREMENTO INT, @ESPECIALIDAD VARCHAR(40))
    //AS
    //    UPDATE DOCTOR SET SALARIO = SALARIO + @INCREMENTO
    //                    WHERE ESPECIALIDAD = @ESPECIALIDAD
    //GO


    //CREATE OR ALTER PROCEDURE SP_DOCTORES_ESPECIALIDAD
    //(@ESPECIALIDAD NVARCHAR(50))
    //AS

    //    SELECT*
    //    FROM DOCTOR
    //    WHERE ESPECIALIDAD = @ESPECIALIDAD
    //GO



    //CREATE OR ALTER PROCEDURE SP_ESPECIALIDADES_DOCTORES
    //AS

    //    SELECT DISTINCT ESPECIALIDAD
    //    FROM DOCTOR
    //GO
    #endregion


    public class RepositoryDoctores
    {
        private EnfermosContext context;

        public RepositoryDoctores(EnfermosContext context)
        {
            this.context = context;
        }

        public List<Doctor> GetDoctores()
        {
            string sql = "SP_TODOS_DOCTORES";
            var consulta = this.context.Doctores.FromSqlRaw(sql);
            List<Doctor> doctores = consulta.AsEnumerable().ToList();
            return doctores;
        }

        public void IncrementarSalarioDoctor(string especialidad, int incremento)
        {
            string sql = "EXEC SP_INCREMENTAR_SALARIO_DOCTOR @INCREMENTO, @ESPECIALIDAD";
            this.context.Database.ExecuteSqlRaw(sql,
                new SqlParameter("@INCREMENTO", incremento),
                new SqlParameter("@ESPECIALIDAD", especialidad));
        }

        public List<Doctor> GetDoctoresByEspecialidad(string especialidad)
        {
            string sql = "EXEC SP_DOCTORES_ESPECIALIDAD @ESPECIALIDAD";
            var consulta = this.context.Doctores.FromSqlRaw(sql,
                new SqlParameter("@ESPECIALIDAD", especialidad));
            List<Doctor> doctores = consulta.AsEnumerable().ToList();
            return doctores;
        }

        public List<string> GetEspecialidades()
        {
            using (DbCommand com =
                this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_ESPECIALIDADES_DOCTORES";
                com.CommandText = sql;
                com.CommandType = CommandType.StoredProcedure;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();
                List<string> especialidades = new List<string>();
                while (reader.Read())
                {
                    especialidades.Add(reader["ESPECIALIDAD"].ToString());
                }
                reader.Close();
                com.Connection.Close();
                return especialidades;
            }
        }

    }
}
