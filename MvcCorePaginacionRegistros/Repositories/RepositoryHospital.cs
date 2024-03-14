using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Data;
using MvcCorePaginacionRegistros.Models;

#region PROCEDURES AND VIEWS
//CREATE VIEW V_GRUPO_EMPLEADOS
//AS
//	SELECT CAST(ROW_NUMBER() OVER(ORDER BY EMP_NO)AS INT) AS POSICION,
//    ISNULL(EMP_NO, 0) AS EMP_NO, APELLIDO, OFICIO, DIR, FECHA_ALT, SALARIO, COMISION, DEPT_NO
//	FROM EMP
//GO

//CREATE PROCEDURE SP_GRUPO_EMPLEADOS
//(@POSICION INT)
//AS
//    SELECT EMP_NO, APELLIDO, OFICIO, DIR, FECHA_ALT, SALARIO, COMISION, DEPT_NO
//	FROM V_GRUPO_EMPLEADOS
//	WHERE POSICION>= @POSICION AND POSICION< (@POSICION+3)
//GO





//CREATE PROCEDURE SP_GRUPO_EMPLEADOS_OFICIO
//(@POSICION INT, @OFICIO NVARCHAR(50))
//AS
//	select * from(
//	SELECT CAST(
//	ROW_NUMBER() OVER(ORDER BY APELLIDO)AS INT) AS POSICION
//    , EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO
//	FROM EMP
//	WHERE OFICIO = @OFICIO) as QUERY

//    WHERE QUERY.POSICION >= "POSICION" AND QUERY.POSICION < (@POSICION + 2)
//GO


//ALTER PROCEDURE SP_DEPARTAMENTOS_EMPLEADOS_OUT
//(@POSICION INT, @DEPARTAMENTO INT, @REGISTROS INT OUT)
//AS
//    SELECT @REGISTROS = COUNT(EMP_NO) FROM EMP
//	WHERE DEPT_NO = @DEPARTAMENTO

//	select * from(
//	SELECT CAST(
//	ROW_NUMBER() OVER(ORDER BY APELLIDO)AS INT) AS POSICION
//    , EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO
//	FROM EMP
//	WHERE DEPT_NO = @DEPARTAMENTO) as QUERY

//    WHERE QUERY.POSICION = @POSICION
//GO


#endregion



namespace MvcCorePaginacionRegistros.Repositories
{

    public class RepositoryHospital
    {
        private HospitalContext context;

        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            return await this.context.Departamentos.ToListAsync();
        }

        public async Task<List<Empleado>> GetEmpleadosDepartamentoAsync
            (int idDepartamento)
        {
            var empleados = this.context.Empleados
                .Where(x => x.IdDepartamento == idDepartamento);
            if (empleados.Count() == 0)
            {
                return null;
            }
            else
            {
                return await empleados.ToListAsync();
            }
        }

        public async Task<int> GetNumeroRegistrosVistaDepartamentos()
        {
            return await this.context.VistaDepartamentos.CountAsync();
        }

        public async Task<VistaDepartamento>
            GetVistaDepartamentoAsync(int posicion)
        {
            VistaDepartamento vista = await
                this.context.VistaDepartamentos
                .Where(z => z.Posicion == posicion).FirstOrDefaultAsync();
            return vista;
        }

        public async Task<List<VistaDepartamento>>
            GetGrupoVistaDepartamentoAsync(int posicion)
        {
            //SELECT* FROM V_DEPARTAMENTOS_INDIVIDUAL
            //WHERE POSICION >= 1 AND POSICION< (1 +2)
            var consulta = from datos in this.context.VistaDepartamentos
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 2)
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task<List<Departamento>>
            GetGrupoDepartamentosAsync(int posicion)
        {
            string sql = "SP_GRUPO_DEPARTAMENTOS @posicion";
            SqlParameter pamPosicion =
                new SqlParameter("@posicion", posicion);
            var consulta =
                this.context.Departamentos.FromSqlRaw(sql, pamPosicion);
            return await consulta.ToListAsync();
        }

        public async Task<int> GetNumeroEmpleadosAsync()
        {
            return await this.context.Empleados.CountAsync();
        }


        public async Task<List<Empleado>>
            GetGrupoEmpleadosAsync(int posicion)
        {
            string sql = "SP_GRUPO_EMPLEADOS @posicion";
            SqlParameter pamPosicion =
                new SqlParameter("@posicion", posicion);
            var consulta =
                this.context.Empleados.FromSqlRaw(sql, pamPosicion);
            return await consulta.ToListAsync();
        }

        public async Task<int> GetNumeroEmpleadosOficioAsync(string oficio)
        {
            return await this.context.Empleados
                .Where(z => z.Oficio == oficio).CountAsync();
        }

        public async Task<List<Empleado>> GetGrupoEmpleadosOficioAsync(int posicion, string oficio)
        {
            string sql = "SP_GRUPO_EMPLEADOS_OFICIO @posicion, @oficio";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamOficio = new SqlParameter("@oficio", oficio);
            var consulta = this.context.Empleados.FromSqlRaw(sql, pamPosicion, pamOficio);
            return await consulta.ToListAsync();
        }

        //EL CONTROLLER NOS VA A DAR UNA POSICION Y UN OFICIO
        //DEBEMOS DEVOLVER LOS EMPLEADOS Y EL NUMERO DE REGISTROS
        public async Task<ModelPaginacionEmpleados> GetGrupoEmpleadosOficioOutAsync
            (int posicion, string oficio)
        {
            string sql = "SP_GRUPO_EMPLEADOS_OFICIO_OUT @posicion, @oficio,@registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamOficio = new SqlParameter("@oficio", oficio);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);
            pamRegistros.Direction = System.Data.ParameterDirection.Output;
            var consulta = 
                this.context.Empleados.FromSqlRaw(sql,pamPosicion, pamOficio, pamRegistros);
            //PRIMERO DEBEMOS EJECUTAR LA CONSULTA PARA PODER RECUPERAR 
            //LOS PARAMETROS DE SALIDA
            List<Empleado> empleados = await consulta.ToListAsync();
            int registros = (int)pamRegistros.Value;

            return new ModelPaginacionEmpleados
            {
                NumeroRegistros = registros,
                Empleados = empleados
            };
        }

        public async Task<ModelDepartamentosDetalles> GetDetallesDepartamentoEmpleadosAsync(int posicion, int numDept)
        {
            Departamento dept = await this.context.Departamentos
                .Where(z => z.IdDepartamento == numDept)
                .FirstOrDefaultAsync();

            string sql = "SP_DEPARTAMENTOS_EMPLEADOS_OUT @posicion, @dept, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamOficio = new SqlParameter("@dept", numDept);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);
            pamRegistros.Direction = System.Data.ParameterDirection.Output;

            var consulta = this.context.Empleados
                .FromSqlRaw(sql, pamPosicion, pamOficio, pamRegistros)
                .AsEnumerable();

            // Obtenemos solo el primer empleado después de la llamada a AsEnumerable()
            Empleado emp = consulta.FirstOrDefault();
            int registros = (int)pamRegistros.Value;

            return new ModelDepartamentosDetalles
            {
                Departamento = dept,
                Empleado = emp,
                Registros = registros
            };
        }



    }

}

