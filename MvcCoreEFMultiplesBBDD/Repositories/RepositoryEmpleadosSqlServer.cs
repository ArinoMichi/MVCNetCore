using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
#region VISTAS
//CREATE OR ALTER VIEW v_empleados
//AS
//	SELECT EMP.EMP_NO, EMP.APELLIDO, EMP.OFICIO,
//    EMP.SALARIO, DEPT.DEPT_NO, DEPT.DNOMBRE, DEPT.LOC
//	FROM EMP
//	INNER JOIN DEPT
//	ON EMP.DEPT_NO = DEPT.DEPT_NO
//GO
#endregion
#region STORED PROCECURES 

//create procedure SP_ALL_EMPLEADOS
//as
//	select * from v_empleados
//go


//CREATE PROCEDURE SP_DETAILS_EMPLEADO
//(@IDEMPLEADO INT)
//AS
//	SELECT * FROM V_EMPLEADOS WHERE EMP_NO= @IDEMPLEADO
//GO
#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosSqlServer : IRepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleadosSqlServer(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string sql = "SP_ALL_EMPLEADOS";
            var consulta = this.context.Empleados.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }



        public async Task<Empleado> FindEmpleadosAsync(int EmpNo)
        {
            string sql = "SP_DETAILS_EMPLEADO @idEmpleado";
            SqlParameter pamId = new SqlParameter("@idEmpleado", EmpNo);

            var consulta = this.context.Empleados.FromSqlRaw(sql, pamId);
            Empleado emp = consulta.AsEnumerable().FirstOrDefault();
            return emp;

        }

    }
}
