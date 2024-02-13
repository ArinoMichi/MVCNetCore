using MvcNetCoreLinqToSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcNetCoreLinqToSql.Repositories
{
    public class RepositoryEmpleados
    {

        private DataTable tablaEmpleados;
        private DataTable tablaDepartamentos;

        public RepositoryEmpleados()
        {
                string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = sa; Password =MCSD2023";
           
                string sqlEmp = "select * from EMP";
                string sqlDept = "select * from DEPT";
                SqlDataAdapter adEmp = new SqlDataAdapter(sqlEmp, connectionString);
                SqlDataAdapter adDept = new SqlDataAdapter(sqlDept, connectionString);

                // INSTANCIAMOS NUESTRO DATATABLE
                this.tablaEmpleados = new DataTable();
                this.tablaDepartamentos = new DataTable();

                // TRAEMOS LOS DATOS
                adEmp.Fill(tablaEmpleados);
                adDept.Fill(tablaDepartamentos);
            
        }

        //METODO PARA FILTRAR EMPLEADOS POR SU OFICIO
        public ResumenEmpleados GetEmpleadosOficio(string oficio)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<string>("OFICIO") == oficio
                           select datos;
            //ME GUSTARIA QUE LOS DATOS ESTEN ORDENADOS POR SALARIO
            consulta = consulta.OrderBy(x => x.Field<int>("SALARIO"));
            int personas = consulta.Count();
            int maximo = consulta.Max(z => z.Field<int>("SALARIO"));
            double media = consulta.Average(x => x.Field<int>("SALARIO"));

            List<Empleado> empleados = new List<Empleado>();
            foreach(var row in consulta)
            {
                Empleado emp = new Empleado
                {
                    IdEmpleado = row.Field<int>("EMP_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Oficio = row.Field<string>("OFICIO"),
                    Salario = row.Field<int>("SALARIO"),
                    IdDepartamento = row.Field<int>("DEPT_NO")
                };
                empleados.Add(emp);
            }
            ResumenEmpleados resumen = new ResumenEmpleados
            {
                Personas = personas,
                MaximoSalario = maximo,
                MediaSalarial = media,
                Empleados = empleados
            };
            return resumen;
        }

        public ResumenEmpleados GetEmpleadosDepartamento(int idDept)
        {
            ResumenEmpleados resumen = null;
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<int>("DEPT_NO") == idDept
                           select datos;
            if (consulta.Count() == 0)
            {

            }
            else
            {
                //ME GUSTARIA QUE LOS DATOS ESTEN ORDENADOS POR SALARIO
                consulta = consulta.OrderBy(x => x.Field<int>("SALARIO"));
                int personas = consulta.Count();
                int maximo = consulta.Max(z => z.Field<int>("SALARIO"));
                double media = consulta.Average(x => x.Field<int>("SALARIO"));

                List<Empleado> empleados = new List<Empleado>();
                foreach (var row in consulta)
                {
                    Empleado emp = new Empleado
                    {
                        IdEmpleado = row.Field<int>("EMP_NO"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Oficio = row.Field<string>("OFICIO"),
                        Salario = row.Field<int>("SALARIO"),
                        IdDepartamento = row.Field<int>("DEPT_NO")
                    };
                    empleados.Add(emp);
                }
                resumen = new ResumenEmpleados
                {
                    Personas = personas,
                    MaximoSalario = maximo,
                    MediaSalarial = media,
                    Empleados = empleados
                };
            }
            return resumen;
        }


        //METODO PARA RECUPERAR LOS OFICIOS DE LOS EMPLEADOS
        public List<string> GetOficios()
        {
            var consulta = (from datos in this.tablaEmpleados.AsEnumerable()
                            select datos.Field<string>("OFICIO")).Distinct();
            List<string> oficios = new List<string>();
            foreach(string ofi in consulta)
            {
                oficios.Add(ofi);
            }

            return oficios;
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.tablaDepartamentos.AsEnumerable()
                           select datos;
            List<Departamento> departamentos = new List<Departamento>();
            foreach(var row in consulta)
            {
                Departamento dept = new Departamento
                {
                    IdDepartamento = row.Field<int>("DEPT_NO"),
                    Nombre = row.Field<string>("DNOMBRE"),
                    Localidad = row.Field<string>("LOC")
                };
                departamentos.Add(dept);
            }
            return departamentos;
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


        public List<Empleado> GetEmpleadosOficioSalario(string oficio, int salario)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<string>("OFICIO") == oficio.ToUpper()
                           && datos.Field<int>("SALARIO") >= salario
                           select datos;
            List<Empleado> empleados = null;
            if (consulta.Count() == 0)
            {

            }
            else
            {   
                empleados = new List<Empleado>();
                //EL CODIGO PARA LA LISTA DE EMPLEADOS
                foreach (var row in consulta)
                {
                    //SINTAXIS PARA INSTANCIAR UN OBJETO Y RELLENAR SUS
                    //PROPIEDADES A LA VEZ
                    Empleado empleado = new Empleado
                    {
                        IdEmpleado = row.Field<int>("EMP_NO"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Oficio = row.Field<string>("OFICIO"),
                        Salario = row.Field<int>("SALARIO"),
                        IdDepartamento = row.Field<int>("DEPT_NO")
                    };
                    empleados.Add(empleado);
                }
            }
            
            return empleados;
            }
        }
    }

