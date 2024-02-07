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


        public IActionResult Index()
        {
            List<Hospital> hospitales = this.repo.GetHospitales();
            return View(hospitales);

        }
    }
}
