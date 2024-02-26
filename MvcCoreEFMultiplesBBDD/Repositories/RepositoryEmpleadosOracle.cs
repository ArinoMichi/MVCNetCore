using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region VIEWS
//create or replace view v_empleados as
//  select nvl(EMP.EMP_NO, 0) as EMP_NO,
//  EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO
//  , DEPT.DNOMBRE, DEPT.LOC, DEPT.DEPT_NO
//  from EMP
//  inner join DEPT
//  on EMP.DEPT_NO=DEPT.DEPT_NO;
#endregion

#region STORED_PROCEDURES

//create or replace procedure SP_ALL_EMPLEADOS
//(p_cursor_empleados out sys_refcursor)
//as
//begin
//  open p_cursor_empleados for
//  select * from v_empleados;
//end;



//create or replace procedure SP_DETAILS_EMPLEADO
//(p_cursor_empleados out sys_refcursor,
//p_idempleado EMP.EMP_NO%TYPE)
//as
//begin
//  open p_cursor_empleados for
//  select * from V_EMPLEADOS
//  where EMP_NO=p_idempleado;
//end;
#endregion


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
            string sql = "begin ";
            sql += "SP_DETAILS_EMPLEADO (:p_cursor_empleados, :p_idempleado);";
            sql += "end;";

            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = System.Data.ParameterDirection.Output;
            pamCursor.OracleDbType= OracleDbType.RefCursor;
            OracleParameter pamId = new OracleParameter("p_idempleado", EmpNo);
            var consulta = this.context.Empleados.FromSqlRaw(sql,pamCursor,pamId);
            Empleado emp =  consulta.AsEnumerable().FirstOrDefault();
            return emp;
        }
    }
}
