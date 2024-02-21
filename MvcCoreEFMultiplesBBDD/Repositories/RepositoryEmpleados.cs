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


namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task<Empleado> FindEmpleadosAsync(int EmpNo)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.EmpNo == EmpNo
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
