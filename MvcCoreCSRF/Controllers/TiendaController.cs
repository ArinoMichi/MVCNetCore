using Microsoft.AspNetCore.Mvc;

namespace MvcCoreCSRF.Controllers
{
    public class TiendaController : Controller
    {
        public IActionResult Productos()
        {
            //COMPROBAMOS SI EXISTE EL USUARIO PARA DEJARLE ENTRAR O NO
            if(HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("AccesoDenegado", "Managed");
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Productos(string direccion, string[] producto)
        {
            //COMPROBAMOS SI EXISTE EL USUARIO ANTES
            //DE REALIZAR LA COMPRA
            if (HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("AccesoDenegado", "Managed");
            }
            else
            {
                //PARA ENVIAR INFORMACION ENTRE CONTROLADORES
                //SE UTILIZA TempData["KEY"]
                TempData["DIRECCION"] = direccion;
                TempData["PRODUCTOS"] = producto;
                return RedirectToAction("PedidoFinal");
            }
        }

        public IActionResult PedidoFinal()
        {
            string[] productos = TempData["PRODUCTOS"] as string[];
            ViewData["DIRECCION"] = TempData["DIRECCION"] as string;
            return View(productos);
        }
    }
}
