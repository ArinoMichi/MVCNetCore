using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        private IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
        {
            this.repo = repo;
        }


        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            this.repo.InsertDoctor(doctor.IdDoctor, doctor.Apellido, doctor.Especialidad, doctor.Salario, doctor.IdHospital);
            return RedirectToAction("Index");
        }

        public IActionResult DoctoresEspecialidad()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);

        }

        [HttpPost]
        public IActionResult DoctoresEspecialidad(string especialidad)
        {
            List<Doctor> doctores =
                this.repo.GetDoctoresEspecialidad(especialidad);
            if (doctores == null)
            {
                ViewData["MENSAJE"] = "No existen doctores con la especialidad " +
                    especialidad;
                return View();
            }
            else
            {
                return View(doctores);
            }
        }

        public IActionResult Delete(int iddoctor)
        {
            this.repo.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int iddoctor)
        {
            Doctor doc = this.repo.GetDoctorById(iddoctor);
            return View(doc);
        }
        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            this.repo.UpdateDoctor(doctor.IdDoctor, doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario);
            return RedirectToAction("Index");
        }
    }
}
