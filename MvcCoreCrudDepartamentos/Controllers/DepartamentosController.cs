using Microsoft.AspNetCore.Mvc;
using MvcCoreCrudDepartamentos.Models;
using MvcCoreCrudDepartamentos.Repositories;

namespace MvcCoreCrudDepartamentos.Controllers
{
    public class DepartamentosController : Controller
    {
        RepositoryDepartamentos repo;

        public DepartamentosController()
        {
            this.repo = new RepositoryDepartamentos();
        }

        //EN UN CONTROLLER JAMAS SE CAMBIAN LOS NOMBRES DE LOS METODOS
        //PARA INCLUIR ASYNC
        public async Task<IActionResult> Index()
        {
            List<Departamento> departamentos = await this.repo.GetDepartamentosAsync();
            return View(departamentos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Departamento departamento =
                await this.repo.FindDepartamentoAsync(id);
            return View(departamento);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string localidad)
        {
            await this.repo.InsertDepartamentoAsync(nombre, localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Departamento dept = await this.repo.FindDepartamentoAsync(id);
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string nombre, string localidad)
        {
            await this.repo.UpdateDepartamentoAsync(id, nombre, localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int iddepartamento)
        {
            await this.repo.DeleteDepartamentoAsync(iddepartamento);
            return RedirectToAction("Index");
        }
    }

}
