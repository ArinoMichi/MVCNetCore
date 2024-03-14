using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadPersonalizada.Filters;

namespace NetCoreSeguridadPersonalizada.Controllers
{
    public class UsuariosController : Controller
    {
        [AuthorizeUsers]
        public IActionResult Perfil()
        {
            return View();
        }

    }
}
