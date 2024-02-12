using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;
using MvcCoreAdoNet.Repositories;

namespace MvcCoreAdoNet.Controllers
{
    public class HospitalesController : Controller
    {
        RepositoryHospital repo;

        public HospitalesController()
        {
            this.repo = new RepositoryHospital();
        }


        public IActionResult Index(int? eliminar)
        {
            if (eliminar != null)
            {
                this.repo.DeleteHospital(eliminar.Value);
            }
            List<Hospital> hospitales = this.repo.GetHospitales();
            return View(hospitales);

        }
        public IActionResult Details(int idhospital) 
        {
            Hospital hospital = this.repo.FindHospitalById(idhospital);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Hospital hospital)
        {
            int resultado= this.repo.InsertHospital(hospital);
            ViewData["MENSAJE"] = "Filas afectadas: " + resultado;
            return View();
        }

        public IActionResult Edit(int idhospital)
        {
            Hospital hospital = this.repo.FindHospitalById(idhospital);
            return View(hospital);
        }

        [HttpPost]
        public IActionResult Edit(Hospital hospital) 
        {
            int resultado = this.repo.UpdateHospital(hospital);
            ViewData["MENSAJE"] = "Filas afectadas: " + resultado;
            return RedirectToAction("Index");
            //return View("Index");
        }

    }
}
