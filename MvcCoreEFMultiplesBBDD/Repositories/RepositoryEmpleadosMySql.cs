
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using MySqlConnector;

namespace MvcCoreEFMultiplesBBDD.Repositories

#region VIEWS
//create view V_EMPLEADOS as 
//select ifnull(EMP.EMP_NO,0) as EMP_NO,
//EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, DEPT.DNOMBRE, DEPT.LOC,DEPT.DEPT_NO
//FROM EMP
//INNER JOIN DEPT
//ON EMP.DEPT_NO = DEPT.DEPT_NO;
#endregion

#region PROCEDURES
    //DELIMITER //
    //CREATE PROCEDURE SP_ALL_EMPLEADOS()
    //BEGIN
    //    SELECT* FROM V_EMPLEADOS;
    //END //
    //DELIMITER ;



    //DELIMITER //
    //CREATE PROCEDURE SP_DETAILS_EMPLEADO(IN empleadoId INT)
    //BEGIN
    //    SELECT*
    //    FROM V_EMPLEADOS
    //    WHERE EMP_NO = empleadoId;
    //END //
    //DELIMITER ;
#endregion

{
    public class RepositoryEmpleadosMySql : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosMySql(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<Empleado> FindEmpleadosAsync(int EmpNo)
        {
            var empleadoIdParam = new MySqlParameter("@empleadoId", EmpNo);

            // Ejecuta la consulta y extrae los resultados
            var consulta = await context.Empleados
                .FromSqlRaw("CALL SP_DETAILS_EMPLEADO(@empleadoId)", empleadoIdParam)
                .ToListAsync();

            // Extrae el primer resultado (o null si no hay resultados)
            Empleado emp = consulta.FirstOrDefault();

            return emp;
        }



        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            var empleados = await context.Empleados
                            .FromSqlRaw("CALL SP_ALL_EMPLEADOS()")
                            .ToListAsync();
            return empleados;
        }
    }
}
