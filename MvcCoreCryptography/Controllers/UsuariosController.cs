using Microsoft.AspNetCore.Mvc;
using MvcCoreCryptography.Helpers;
using MvcCoreCryptography.Models;
using MvcCoreCryptography.Repositories;

namespace MvcCoreCryptography.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;
        private HelperPathProvider helperPathProvider;
        private HelperMails helperMails;

        public UsuariosController(RepositoryUsuarios repo, HelperMails helperMails, HelperPathProvider helperPathProvider)
        {
            this.repo = repo;
            this.helperMails = helperMails;
            this.helperPathProvider = helperPathProvider;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string email, string password, string imagen)
        {
            Usuario user = await this.repo.RegisterUserAsync(nombre, email,password,imagen);
            string serverUrl = this.helperPathProvider.MapUrlServerPath();
            //https://localhost:855/Usuarios/ActivateUser/TOKEN??
            serverUrl = serverUrl + "/Usuarios/ActivateUser/?token=" + user.TokenMail;
            string mensaje = "<h3>Usuario registrado</h3>";
            mensaje += "<p> Debe activar su cuenta con nosotros pulsando el siguiente enlace </p>";
            mensaje += "<p>" + serverUrl + "</p>";
            mensaje += "<a href='" + serverUrl + "'>" + serverUrl + "</a>";
            mensaje += "<p>Muchas gracias</p>";
            await this.helperMails.SendMailAsync(email, "Registro Usuario", mensaje);

            ViewData["MENSAJE"] = "Usuario registrado correctamente. Hemos enviado un mail a " + email + " para activar su cuenta";

            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            Usuario user = await this.repo.LogInUserAsync(email, password);
            if(user == null)
            {
                ViewData["MENSAJE"] = "Credenciales Incorrectas";
                return View();
            }
            else
            {
                return View(user);
            }
        }
        public async Task<IActionResult> ActivateUser(string token) 
        {
            await this.repo.ActivateUserAsync(token);
            ViewData["MENSAJE"] = "Cuenta activada correctamente";
            return View();
        }

    }
}
