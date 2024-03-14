using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NetCoreSeguridadPersonalizada.Controllers
{
    public class ManagedController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if(username.ToLower()=="admin" && password.ToLower() == "admin")
            {
                //AUNQUE NOSOTROS NO LO VEAMOS, POR SEGURIDAD
                //SESSION SIEMPRE TRABAJA CON COOKIES.
                //DEBEMOS CREAR UNA IDENTIDAD PARA EL USUARIO
                //BASADA EN COOKIES DE AUTORIZACION
                //E INDICAR QUE NUESTRO USER TENDRA NAME Y ROLE
                ClaimsIdentity identity =
                    new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                                        ClaimTypes.Name, ClaimTypes.Role);
                //LOS CLAIMS INDICAN CARACTERISTICAS DEL USUARIO
                Claim claimUserName = new Claim(ClaimTypes.Name, username);
                Claim claimRole = new Claim(ClaimTypes.Role, "USUARIO");
                identity.AddClaim(claimUserName);
                identity.AddClaim(claimRole);
                //CREAMOS NUESTRO USER PRINCIPAL QUE SERA EL QUE
                //ESTARA DENTRO DE NUESTRA APP (SESSION)
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                //VALIDAMOS AL USUARIO EN EL SISTEMA
                await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       userPrincipal,
                       new AuthenticationProperties
                       {
                           ExpiresUtc = DateTime.Now.AddMinutes(15)
                       }
                    );
                //LLEVAMOS AL USUARIO A SU PERFIL
                return RedirectToAction("Perfil", "Usuarios");
            }
            else
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            } 
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //NOS LO LLEVAMOS A UNA ZONA NEUTRA
            return RedirectToAction("Index", "Home");
        }
    }
}
