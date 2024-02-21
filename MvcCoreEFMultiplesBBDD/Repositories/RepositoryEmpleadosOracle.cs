using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string sql = "begin ";
            sql += "SP_ALL_EMPLEADOS(:p_cursor_empleados);";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter(":p_cursor_empleados", null);
            pamCursor.Direction = System.Data.ParameterDirection.Output;
            //COMO ES UN TIPO DE ORACLE PROPIO(Cursor) DEBEMOS 
            //PONERLO DE FORMA EXPLICITA
            pamCursor.OracleDbType = OracleDbType.RefCursor;

            var consulta = this.context.Empleados.FromSqlRaw(sql,pamCursor);
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
