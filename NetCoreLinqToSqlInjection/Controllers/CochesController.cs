using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class CochesController : Controller
    {
        private ICoche car;

        public CochesController(ICoche coche)
        {
            this.car = coche;
        }

        public IActionResult Index()
        {
            return View(this.car);
        }

        [HttpPost]
        public IActionResult Index(string action)
        {
            if(action.ToLower()== "acelerar")
            {
                this.car.Acelerar();
            }
            else 
            { 
                this.car.Frenar();
            }
            return View(this.car);
        }
    }
}
