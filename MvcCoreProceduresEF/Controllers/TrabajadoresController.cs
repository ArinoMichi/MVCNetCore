using Microsoft.AspNetCore.Mvc;
using MvcCoreProceduresEF.Models;
using MvcCoreProceduresEF.Repositories;

namespace MvcCoreProceduresEF.Controllers
{
    public class TrabajadoresController : Controller
    {
        private RepositoryTrabajadores repo;

        public TrabajadoresController(RepositoryTrabajadores repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Trabajador> trabajadores = await this.repo.GetTrabajadoresAsync();
            return View(trabajadores);
        }

        public async Task<IActionResult> TrabajadoresOficio()
        {
            List<string> oficios = await this.repo.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrabajadoresOficio(string oficio)
        {
            List<string> oficios = await this.repo.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;

            TrabajadoresModel model = await this.repo.GetTrabajadoresOficioAsync(oficio);
            return View(model);
        }
    }
}
