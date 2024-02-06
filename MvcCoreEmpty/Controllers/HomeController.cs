using Microsoft.AspNetCore.Mvc;
using MvcCoreEmpty.Models;

namespace MvcCoreEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contenido()
        {
            return View();
        }

        public IActionResult VistaPersona()
        {
            Persona persona = new Persona();
            persona.Nombre = "Alumno Core";
            persona.Email = "alumnocore@gmail.com";
            persona.Edad = 23;
            return View(persona);
        }
    }
}
