using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VistaMascotas()
        {

            List<Mascota> mascotas = ObtenerListaMascotas();
            return View(mascotas);
        }
        private List<Mascota> ObtenerListaMascotas()
        {
            // Aquí puedes crear y retornar una lista de mascotas de ejemplo
            List<Mascota> mascotas = new List<Mascota>
        {
            new Mascota("Max", 3, "Labrador"),
            new Mascota("Luna", 2, "Golden Retriever"),
            new Mascota("Rocky", 4, "Bulldog")
        };

            return mascotas;
        }

    }
}
