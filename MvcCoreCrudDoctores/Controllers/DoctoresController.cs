using Microsoft.AspNetCore.Mvc;
using MvcCoreCrudDoctores.Models;
using MvcCoreCrudDoctores.Repositories;

namespace MvcCoreCrudDoctores.Controllers
{
    
    public class DoctoresController : Controller
    {
        RepositoryDoctores repo;
        
        public DoctoresController()
        {
            this.repo = new RepositoryDoctores();
        }

        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores = await this.repo.GetDoctoresAsync();
            return View(doctores);
        }

        public async Task<IActionResult> Details(int id)
        {
            Doctor doctor = await this.repo.FindDoctorByIdAsync(id);
            return View(doctor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            await this.repo.InsertDoctorAsync(doctor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Doctor doctor = await this.repo.FindDoctorByIdAsync(id);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(doctor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }

    }
}
