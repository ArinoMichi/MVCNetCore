using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSql.Models;
using MvcNetCoreLinqToSql.Repositories;

namespace MvcNetCoreLinqToSql.Controllers
{
    public class EmpleadosController : Controller
    {
        RepositoryEmpleados repo;

        public EmpleadosController()
        {
            this.repo = new RepositoryEmpleados();
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int id) 
        {
            Empleado emp = this.repo.FindEmpleado(id);
            return View(emp);
        }

        public IActionResult BuscadorEmpleados() 
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        [HttpPost]
        public IActionResult BuscadorEmpleados(string oficio, int salario)
        {
            List<Empleado> empleados = this.repo.GetEmpleadosOficioSalario(oficio, salario);
            if (empleados == null)
            {
                ViewData["MENSAJE"] = "No existen registros con oficio " + oficio + " ni salario mayor a " + salario;
                return View();
            }
            else 
            {
                return View(empleados);
            }
            
        }

        public IActionResult DatosEmpleados()
        {
            ViewData["OFICIOS"] = this.repo.GetOficios();
            return View();
        }

        [HttpPost]
        public IActionResult DatosEmpleados(string oficio)
        {
            ViewData["OFICIOS"] = this.repo.GetOficios();
            ResumenEmpleados resumen = this.repo.GetEmpleadosOficio(oficio);
            return View(resumen);
        }

        public IActionResult BuscadorEmpleadosDepartamento()
        {
            ViewData["DEPARTAMENTOS"] = this.repo.GetDepartamentos();
            return View();
        }
        [HttpPost]
        public IActionResult BuscadorEmpleadosDepartamento(int idDepartamento)
        {
            ViewData["DEPARTAMENTOS"] = this.repo.GetDepartamentos();
            ResumenEmpleados resumen = this.repo.GetEmpleadosDepartamento(idDepartamento);
            if (resumen != null)
            {
                return View(resumen);
            }
            else
            {
                ViewData["MENSAJE"] = "No tiene empleados";
                return View();
            }
            
        }


    }
}
