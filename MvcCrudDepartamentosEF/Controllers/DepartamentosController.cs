using Microsoft.AspNetCore.Mvc;
using MvcCrudDepartamentosEF.Models;
using MvcCrudDepartamentosEF.Repositories;

namespace MvcCrudDepartamentosEF.Controllers
{
    public class DepartamentosController : Controller
    {
        private RepositoryDepartamentos repo;

        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            return View(departamentos);
        }

        public IActionResult Details(int id) 
        {
            Departamento dept = this.repo.FindDepartamento(id);
            return View(dept);
        }
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Departamento dept)
        {
            this.repo.InsertDepartamento(dept);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Departamento dept = this.repo.FindDepartamento(id);
            return View(dept);
        }
        [HttpPost]
        public IActionResult Edit(Departamento dept)
        {
            this.repo.Update(dept);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            this.repo.DeleteDepartamento(id);
            return RedirectToAction("Index");
        }
    }
}
