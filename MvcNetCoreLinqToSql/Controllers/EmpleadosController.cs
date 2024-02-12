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
    }
}
