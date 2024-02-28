using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Extensions;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        int numero = 1;
        public IActionResult Index()
        {
            this.numero++;
            ViewData["NUMERO"] = this.numero;
            return View();
        }

        public IActionResult SessionSimple(string accion) 
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almcacenados correctamente";

                }else if (accion.ToLower() == "mostrar")
                {
                    ViewData["USUARIO"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }
        public IActionResult SessionMascota(string accion)
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Flounder";
                    mascota.Raza = "Pez";
                    mascota.Edad = 11;
                    //PARA ALMACENAR EL OBJETO EN SESSION DEBEMOS
                    //CONVERTIRLO A byte[]
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Datos almacenados correctamente";

                }else if(accion.ToLower() == "mostrar")
                {
                    //DEBEMOS RECUPERAR LOS byte[] DE SESSION
                    //QUE REPRESENTAN EL OBJETO MASCOTA
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;

                }
            }
            return View();
        }

        public IActionResult SessionCollection(string accion)
        {
            if(accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota{Nombre="Pumba", Raza="Jabali", Edad=14},
                        new Mascota{Nombre="Rafiki", Raza="Mono", Edad=18},
                        new Mascota{Nombre="Olaf", Raza="Cosa", Edad= 8},
                        new Mascota{Nombre="Nala", Raza="Leona", Edad=12}
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Coleccion Almacenada";
                }
                else if (accion.ToLower() =="mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>) HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
        }


        public IActionResult SessionMascotaJson(string accion)
        {
            if(accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota 
                    { 
                        Nombre= "Abu", Raza="Monito", Edad = 18
                    };

                    //SERIALIZAMOS EL OBJETO MEDIANTE JSON
                    string jsonMascota = HelperJsonSession.SerializableObject<Mascota>(mascota);
                    //UTILIZAMOS LOS METODOS STRING DE SESSION
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"] = "Mascota JSON almacenada";

                }else if(accion.ToLower() == "mostrar")
                {
                    //EXTRAEMOS EL STRING QUE REPRESENTA LA MASCOTA
                    string jsonMascota =
                        HttpContext.Session.GetString("MASCOTA");
                    Mascota mascota = HelperJsonSession.DeserializeObject<Mascota>(jsonMascota);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaObject(string accion)
        {
            if(accion != null)
            {
                if (accion.ToLower()== "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Simba",
                        Raza = "Bebe Leon",
                        Edad = 1
                    };

                    HttpContext.Session.SetObject("MASCOTAOBJECT", mascota);
                    ViewData["MENSAJE"] = "Mascota OBJETO almacenada";

                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota =
                        HttpContext.Session.GetObject<Mascota>("MASCOTAOBJECT");
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionCollectionObject(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
            {
                new Mascota {Nombre = "Rey", Raza="Leon", Edad= 14},
                new Mascota {Nombre = "Homer", Raza="Simpson", Edad= 18},
                new Mascota {Nombre = "Lilo", Raza="Stich", Edad= 8},
                new Mascota {Nombre = "Ned", Raza="Flanders", Edad= 12}
            };
                    HttpContext.Session.SetObject("MASCOTASLIST", mascotas);
                    ViewData["MENSAJE"] = "Mascotas almacenadas";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    List<Mascota> mascotas = HttpContext.Session.GetObject<List<Mascota>>("MASCOTASLIST");
                    return View(mascotas);
                }
            }
            return View();
        }
    }
}
