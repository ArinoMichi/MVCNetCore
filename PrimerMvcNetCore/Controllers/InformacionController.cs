using Microsoft.AspNetCore.Mvc;
using PrimerMvcNetCore.Models;

namespace PrimerMvcNetCore.Controllers
{
    public class InformacionController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
       
        //SIEMPRE DEBEMOS TENER UN METODO GET
        public IActionResult VistaControladorPost()
        {
            return View();
        }
        [HttpPost]  
        public IActionResult VistaControladorPost(Persona persona, string aficiones)
        {
            ViewData["DATOS"] = "Nombre: " + persona.Nombre
                + " ,Email: " + persona.Email
                + " ,Edad: " + persona.Edad
                + " ,Aficiones: " + aficiones;
            return View();
        }



        public IActionResult ControladorVista()
        {
            //VAMOS A ENVIAR INFORMACION SIMPLE A NUESTRA VISTA
            Persona persona = new Persona();
            persona.Nombre = "Persona model";
            persona.Edad = 77;
            persona.Email = "model@gmail.com";

            ViewData["NOMBRE"] = "Alumno";
            ViewData["EDAD"] = 24;
            ViewBag.DiaSemana = "Lunes";
            return View(persona);
        }

        //VAMOS A RECIBIR DOS PARAMETROS
        public IActionResult VistaControladorGet(string app, System.Nullable<int> version)
        {

            //AHORA LA VERSION ES OPCIONAL
            if (app is null || version is null)
            {
                return View();
            }
            else
            {
                //DIBUJAMOS EN LA VISTA LOS PARAMETROS RECIBIDOS
                ViewData["DATOS"] = "App: " + app.ToUpper() + ", version: " + version;
                return View();
            }
        }


    }
}
