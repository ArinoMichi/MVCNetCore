using Microsoft.AspNetCore.Mvc;
using PrimerMvcNetCore.Models;

namespace PrimerMvcNetCore.Controllers
{
    public class MatematicasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SumarNumerosGet(int numero1, int numero2)
        {
            ViewBag.Numero1 = numero1;
            ViewBag.Numero2 = numero2;
            ViewBag.Suma = numero1 + numero2;
            return View();
        }
        public IActionResult SumarNumerosPost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SumarNumerosPost(int numero1, int numero2)
        {
            @ViewBag.Data ="La suma de "+ numero1 +" + "+ numero2 +" es: " + (numero1+numero2);
            return View();
        }
        
        public IActionResult ConjeturaCollatz()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConjeturaCollatz(int numero)
        {
            //DECLARAMOS UN MODEL PARA ENVIAR A LA VISTA
            List<int> numeros = new List<int>();
            while (numero != 1)
            {
                if (numero %2==0)
                {
                    numero = numero / 2;
                }
                else
                {
                    numero = numero * 3 + 1;
                }
                numeros.Add(numero);
            }
            //DEVOLVEMOS EL MODEL A LA VISTA
            return View(numeros);
        }

        public IActionResult TablaMultiplicarSimple()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TablaMultiplicarSimple(int numero)
        {
            var resultados = new List<int>();
            for (int i = 1; i <= 11; i++)
            {
                resultados.Add(numero * i);
            }

            return View(resultados);
        }

        public IActionResult TablaMultiplicarModel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TablaMultiplicarModel(int numero)
        {
            var resultados = new List<FilaTablaMultiplicar>();

            for(int i = 1; i<=10; i++)
            {
                FilaTablaMultiplicar fila = new FilaTablaMultiplicar();
                fila.Operacion = i + " x " + numero;
                fila.Resultado = i * numero;
                resultados.Add(fila);
            }
            return View(resultados);
        }
    }
}
