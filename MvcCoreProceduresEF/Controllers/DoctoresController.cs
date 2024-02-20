using Microsoft.AspNetCore.Mvc;
using MvcCoreProceduresEF.Models;
using MvcCoreProceduresEF.Repositories;

namespace MvcCoreProceduresEF.Controllers
{
    public class DoctoresController : Controller
    {

        private RepositoryDoctores repo;
        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            ViewData["ESPECIALIDADES"] = this.repo.GetEspecialidades();
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        [HttpPost]
        public IActionResult Index(string especialidad, int incremento)
        {
            ViewData["ESPECIALIDADES"] = this.repo.GetEspecialidades();
            this.repo.IncrementarSalarioDoctor(especialidad, incremento);
            List<Doctor> doctores = this.repo.GetDoctoresByEspecialidad(especialidad);
            return View(doctores);
        }
    }
}
