using Microsoft.AspNetCore.Mvc;
using PracticaCubos.Extensions;
using PracticaCubos.Models;
using PracticaCubos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticaCubos.Controllers
{
    public class CubosController : Controller
    {
        private readonly RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index(int? idCubo)
        {
            if (idCubo != null)
            {
                List<int> listaCompra = HttpContext.Session.GetObject<List<int>>("CARRITO") ?? new List<int>();
                listaCompra.Add(idCubo.Value);
                HttpContext.Session.SetObject("CARRITO", listaCompra);
                ViewData["MENSAJE"] = "Cubo: " + idCubo + " añadido al carrito!";
            }

            List<Cubo> cubos = await this.repo.GetCubosAsync();
            return View(cubos);
        }

        public async Task<IActionResult> Carrito()
        {
            List<int> idsCubos = HttpContext.Session.GetObject<List<int>>("CARRITO");

            if (idsCubos != null && idsCubos.Any())
            {
                List<Cubo> cubos = await this.repo.GetCubosSessionAsync(idsCubos);
                decimal precioTotal = cubos.Sum(c => c.Precio);

                var carritoModel = new Carrito
                {
                    Cubos = cubos,
                    PrecioTotal = precioTotal
                };

                return View(carritoModel);
            }

            return View(new Carrito());
        }

        public IActionResult Eliminar(int idCubo)
        {
            List<int> idsCubos = HttpContext.Session.GetObject<List<int>>("CARRITO");

            if (idsCubos != null)
            {
                // Eliminar el cubo del carrito por ID
                idsCubos.Remove(idCubo);
                HttpContext.Session.SetObject("CARRITO", idsCubos);
            }

            return RedirectToAction("Carrito");
        }

        public async Task<IActionResult> Comprar()
        {
            List<int> idsCubos = HttpContext.Session.GetObject<List<int>>("CARRITO");
            await this.repo.EjecutarCompra(idsCubos);
            return RedirectToAction("Index");
        }
    }
}
