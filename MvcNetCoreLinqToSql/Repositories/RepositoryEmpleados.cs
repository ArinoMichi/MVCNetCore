using MvcNetCoreLinqToSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcNetCoreLinqToSql.Repositories
{
    public class RepositoryEmpleados
    {

        private DataTable tablaEmpleados;

        public RepositoryEmpleados()
        {
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = sa; Password =MCSD2023";
            string sql = "select * from EMP";
            SqlDataAdapter adEmp = new SqlDataAdapter(connectionString,sql);
            //INSTANCIAMOS NUESTRO DATATABLE
            this.tablaEmpleados = new DataTable();
            //TRAEMOS LOS DATOS
            adEmp.Fill(tablaEmpleados);
        }

        public List<Empleado> GetEmpleados()
        {
            //LA CONSULTA LINQ SE ALMACENA EN VARIABLES DE TIPO var
            var consulta = from datos in this.tablaEmpleados.AsEnumerable() 
                           select datos;
            //LO QUE TENEMOS ALMACENADO EN CONSULTA ES UN CONJUNTO DE
            //OBJETOS DataRow QUE SON LOS OBJETOS QUE CONTIENE LA
            //CLASE DataTable
            //DEBEMOS CONVERTIR DICHOS OBJETOS DataRow EN EMPLEADOS
            List<Empleado> empleados = new List<Empleado>();
            //RECORREMOS CADA FILA DE LA consulta
            foreach (var row in consulta)
            {
                //PARA EXTRAER LOS DATOS DE UNA FILA DataRow
                // fila.Field<TIPO>("COLUMNA")
                Empleado emp = new Empleado();
                emp.IdEmpleado = row.Field<int>("EMP_NO");
                emp.Apellido = row.Field<string>("APELLIDO");
                emp.Oficio = row.Field<string>("OFICIO");
                emp.Salario = row.Field<int>("SALARIO");
                emp.IdDepartamento = row.Field<int>("DEPT_NO");
                empleados.Add(emp);
            }
            return empleados;

        }

        //METODO PARA BUSCAR UN EMPLEADO POR SU ID

        public Empleado FindEmpleado(int idEmpleado)
        {
            //EL ALIAS datos REPRESENTA CADA OBJETO DENTRO DEL CONJUNTO
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<int>("EMP_NO") == idEmpleado
                           select datos;
            //NOSOTROS SABEMOS QUE DEVUELVE SOLO UNA FILA, PERO
            //consulta SIEMPRE SERA UNA COLECCION
            //consulta CONTIENE UNA SERIE DE METODOS Lambda PARA INDICAR
            //CIERTAS FILAS O ACCIONES NECESARIAS
            //TENEMOS UN METODO LLAMADO First() QUE NOS DEVUELVE LA PRIMERA FILA

            var row = consulta.First();
            Empleado emp = new Empleado();
            emp.IdEmpleado = row.Field<int>("EMP_NO");
            emp.Apellido = row.Field<string>("APELLIDO");
            emp.Oficio = row.Field<string>("OFICIO");
            emp.Salario = row.Field<int>("SALARIO");
            emp.IdDepartamento = row.Field<int>("DEPT_NO");

            return emp;
        }
    }
}
