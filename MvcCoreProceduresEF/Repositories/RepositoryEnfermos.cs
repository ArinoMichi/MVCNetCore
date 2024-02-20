using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;
using System.Data.Common;


#region PROCEDIMIENTOS ALMACENADOS

    //CREATE PROCEDURE SP_TODOS_ENFERMOS
    //AS
    //	SELECT * FROM ENFERMO
    //GO

    //CREATE PROCEDURE SP_FIND_ENFERMO
    //(@INSCRIPCION INT)
    //AS
    //	SELECT * FROM ENFERMO 
    //	WHERE INSCRIPCION = @INSCRIPCION
    //GO

    //CREATE PROCEDURE SP_DELETE_ENFERMO
    //(@INSCRIPCION INT)
    //AS
    //    DELETE FROM ENFERMO
    //	WHERE INSCRIPCION = @INSCRIPCION
    //GO

    //--------CREATE----------

    //CREATE PROCEDURE SP_INSERT_ENFERMO
    //(@APELLIDO VARCHAR(20), @DIRECCION VARCHAR(40), @FECHA_NAC DATETIME, @GENERO CHAR)
    //AS
    //    DECLARE @INSCRIPCION INT
    //	SELECT @INSCRIPCION = MAX(INSCRIPCION) +1

    //                            FROM ENFERMO
	
    //	INSERT INTO ENFERMO VALUES(@INSCRIPCION, @APELLIDO, @DIRECCION, @FECHA_NAC, @GENERO, NULL)
    //GO

#endregion




namespace MvcCoreProceduresEF.Repositories
{
    public class RepositoryEnfermos
    {
        private EnfermosContext context;

        public RepositoryEnfermos(EnfermosContext context)
        {
            this.context = context;
        }

        public List<Enfermo> GetEnfermos()
        {
            using(DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_TODOS_ENFERMOS";
                com.CommandText = sql;
                com.CommandType = System.Data.CommandType.StoredProcedure;

                //ABRIMOS LA CONEXION A TRAVES DLE COMANDO
                com.Connection.Open();
                //EJECUTAMOS NUESTRO READER
                DbDataReader reader = com.ExecuteReader();
                List<Enfermo> enfermos = new List<Enfermo>();
                while (reader.Read())
                {
                    Enfermo enfermo = new Enfermo
                    {
                        Inscripcion = int.Parse(reader["INSCRIPCION"].ToString()),
                        Apellido = reader["APELLIDO"].ToString(),
                        Direccion = reader["DIRECCION"].ToString(),
                        FechaNacimiento = DateTime.Parse(reader["FECHA_NAC"].ToString()),
                        Genero = reader["S"].ToString()
                    };
                    enfermos.Add(enfermo);
                }
                return enfermos;
                reader.Close();
                com.Connection.Close();
            }
        }

        public Enfermo FindEnfermo(int inscripcion)
        {
            //PARA LLAMAR A PROCEDIMIENTOS CON PARAMETROS
            //LA LLAMADA SE REALIZA INCLUYENDO LOS PARAMETROS
            //Y TAMBIEN EL NOMBRE DEL PROCEDURE:
            //SP_NOMBREPROCEDIMIENTOS @param1, @param2
            string sql = "SP_FIND_ENFERMO @inscripcion";
            //PARA DECLARAR PARAMETROS SE UTILIZA LA CLASE SqlParameter
            //DEBEMOS TENER CUIDADO CON EL NAMESPACE
            //EL NAMESPACE ES Microsoft.Data
            SqlParameter pamInscripcion = new SqlParameter("@inscripcion", inscripcion);
            //AL SER UN PROCESIMIENTO SELECT, PUEDO UTILIZAR
            //EL METODO FromSqlRaw PARA EXTRAER LOS DATOS
            //SI MI CONSULTA COINCIDE CON UN MODE, PUEDO UTILIZAR
            //LINQ PARA MAPEAR LOS DATOS.

            //CUANDO TENEMOS UN PROCEDURE SELECT, LAS PETICIONES SE
            //DIVIDEN EN DOS. NO PUEDO HACER LINQ Y DESPUES UN foreach
            //DEBEMOS EXTRAER LOS DATOS EN DOS ACCIONES
            //var consulta = this.context.
            var consulta = this.context.Enfermos.FromSqlRaw(sql,pamInscripcion);
            //EXTRAER LAS ENTIDADES DE LAS CONSULTA(EJECUTAR)
            //PARA EJECUTAR, NECESITAMOS AsEnumerable()
            Enfermo enfermo = consulta.AsEnumerable().FirstOrDefault();
            return enfermo;
        }

        public void DeleteEnfermo (int inscripcion)
        {
            string sql = "SP_DELETE_ENFERMO @inscripcion";
            SqlParameter pamInscripcion = new SqlParameter("@inscripcion", inscripcion);
            //EJECUTAR CONSULTAS DE ACCION SE REALIZA MEDIANTE
            //EL METODO ExecuteSqlRaw() QUE SE ACCEDE DESDE
            //DataBase DENTRO DEL DbContext
            this.context.Database.ExecuteSqlRaw(sql, pamInscripcion);
        }

        public void InsertEnfermo(Enfermo enfermo)
        {
            string sql = "SP_INSERT_ENFERMO @apellido, @direccion, @fecha_nac,@genero";
            SqlParameter pamApellido = new SqlParameter("@apellido", enfermo.Apellido);
            SqlParameter pamDireccion = new SqlParameter("@direccion", enfermo.Direccion);
            SqlParameter pamFechaNac = new SqlParameter("@fecha_nac", enfermo.FechaNacimiento);
            SqlParameter pamGenero = new SqlParameter("@genero", enfermo.Genero);

            this.context.Database.ExecuteSqlRaw(sql,pamApellido, pamDireccion, pamFechaNac, pamGenero);
        }
    }
}
