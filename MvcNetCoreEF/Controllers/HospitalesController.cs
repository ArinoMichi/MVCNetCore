using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEF.Repositories;
using MvcNetCoreEF.Models;

namespace MvcNetCoreEF.Controllers
{
    public class HospitalesController : Controller
    {
        RepositoryHospital repo;

        public HospitalesController(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Hospital> hospitales = this.repo.GetHospitales();
            return View(hospitales);
        }
        public IActionResult Details(int idHospital)
        {
            Hospital hospital = this.repo.FindHospital(idHospital);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Hospital hospital)
        {
            this.repo.InsertHospital(hospital);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int idHospital)
        {
            this.repo.DeleteHospita(idHospital);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Hospital hospital = this.repo.FindHospital(id);
            return View(hospital);
        }
        [HttpPost]
        public IActionResult Edit(Hospital hospital)
        {
            this.repo.UpdateHospital(hospital);
            return RedirectToAction("Index");
        }
    }
}
