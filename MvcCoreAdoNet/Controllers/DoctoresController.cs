using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;
using MvcCoreAdoNet.Repositories;

namespace MvcCoreAdoNet.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctores repo;

        public DoctoresController()
        {
            this.repo = new RepositoryDoctores();
        }
        public IActionResult Index()
        {
            List<Doctor> doctores= this.repo.GetDoctores();
            return View(doctores);
           
        }
        [HttpPost]
        public IActionResult Index(string? especialidad)
        {
            List<Doctor> doctores;
            if (especialidad != null)
            {
                doctores = this.repo.FindDoctorPorEspecialidad(especialidad);
            }
            else
            {
                doctores = this.repo.GetDoctores();
            }
            return View(doctores);
        }


        public IActionResult DoctoresEspecialidadOneModel()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            List<string> especialidades = this.repo.GetEspecialidades();
            ModelDoctores model = new ModelDoctores();
            model.Doctores = doctores;
            model.Especialidades = especialidades;
            return View(model);
        }

        [HttpPost]
        public IActionResult DoctoresEspecialidadOneModel(string especialidad)
        {
            List<Doctor> doctores =
                this.repo.FindDoctorPorEspecialidad(especialidad);
            List<string> especialidades = this.repo.GetEspecialidades();
            ModelDoctores model = new ModelDoctores();
            model.Doctores = doctores;
            model.Especialidades = especialidades;
            return View(model);
        }
    }
}
