using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSql.Models;
using MvcNetCoreLinqToSql.Repositories;

namespace MvcNetCoreLinqToSql.Controllers
{
    public class EnfermosController : Controller
    {
        RepositoryEnfermos repo;

        public EnfermosController()
        {
            this.repo = new RepositoryEnfermos();
        }



        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Details(int idEnfermo)
        {
            Enfermo enfermo = this.repo.FindEnfermo(idEnfermo);
            return View(enfermo);
        }
        public IActionResult Delete(int id)
        {
            this.repo.DeleteEnfermo(id);
            return RedirectToAction("Index");
        }
    }
}
