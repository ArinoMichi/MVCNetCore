using Microsoft.AspNetCore.Mvc;
using MvcCoreProceduresEF.Models;
using MvcCoreProceduresEF.Repositories;

namespace MvcCoreProceduresEF.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repo;

        public EnfermosController(RepositoryEnfermos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Details(int id)
        {
            Enfermo enfermo = this.repo.FindEnfermo(id);
            return View(enfermo);
        }

        public IActionResult Delete(int id)
        {
            this.repo.DeleteEnfermo(id);
            return RedirectToAction("Index");
        }

        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert(Enfermo enfermo)
        {
            this.repo.InsertEnfermo(enfermo);
            return RedirectToAction("Index");
        }
    }
}
