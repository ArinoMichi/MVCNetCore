using Microsoft.AspNetCore.Mvc;

namespace PrimerMvcNetCore.Controllers
{
    public class PruebaController : Controller
    {
        public IActionResult PrimeraPagina()
        {
            return View();
        }
    }
}
