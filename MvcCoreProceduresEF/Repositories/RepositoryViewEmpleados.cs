using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;
using System.Diagnostics.Metrics;

#region VISTAS
        //CREATE VIEW V_EMP_DEPT
        //AS
        //	SELECT 
        //	CAST(ISNULL(ROW_NUMBER() OVER(ORDER BY APELLIDO),0) AS INT)
        //	AS ID,
        //    EMP.APELLIDO, EMP.OFICIO,
        //    DEPT.DNOMBRE AS DEPARTAMENTO,
        //    DEPT.LOC AS LOCALIDAD
        //	FROM EMP
        //	INNER JOIN DEPT
        //	ON EMP.DEPT_NO = DEPT.DEPT_NO
        //GO
#endregion




namespace MvcCoreProceduresEF.Repositories
{
    public class RepositoryViewEmpleados
    {
        private HospitalContext context;

        public RepositoryViewEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        //REALIZAMOS LA PETICION A LA VISTA DE FORMA ASINCREONA
        //TENEMOD UN METODO DENTRO DE EF 
        //QUE NOS DEVUELVE LAS LISTAS DE var consulta DE FORMA ASINCRONA
        public async Task<List<ViewEmpleado>> GetEmpleadosAsync()
        {
            var consulta = from datos in this.context.ViewEmpleados
                           select datos;
            return await consulta.ToListAsync();
        }
    }
}
