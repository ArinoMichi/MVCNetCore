using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreLinqToSql.Controllers
{
    public class EmpleadosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
