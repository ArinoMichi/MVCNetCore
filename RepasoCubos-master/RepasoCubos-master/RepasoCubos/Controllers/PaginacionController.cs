using Microsoft.AspNetCore.Mvc;
using RepasoCubos.Models;
using RepasoCubos.Repositories;

namespace RepasoCubos.Controllers
{
    public class PaginacionController : Controller
    {
        private RepositoryCubos repo;

        public PaginacionController(RepositoryCubos repo)
        {
            this.repo = repo;
        }
        private async Task InitializeMarcasAsync()
        {
            List<string> marcas = await this.repo.ObtenerTodasLasMarcas();
            ViewBag.Marcas = marcas;
        }

        //PAGINAR DE 1 EN 1 CON ANTERIOR SIGUIENTE ULTIMO Y PRIMERO
        public async Task<IActionResult> PaginacionCubosAs(int? posicion)
        {
            if(posicion == null)
            {
                posicion = 1;
            }
            //PRIMERO = 1 
            //SIGUIENTE = 5 
            //ANTERIOR = 4 
            //ULTIMO = 5 
            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaCubos();
            int siguiente = posicion.Value + 1;
            //DEBEMOS COMPROBAR QUE NO PASAMOS DEL NUMERO DE REGISTROS 
            if(siguiente  > numeroRegistros)
            {
                siguiente = numeroRegistros;
            }
            int anterior = posicion.Value - 1;
            if(anterior < 1)
            {
                anterior = 1;
            }
            VistaCubos paginacionCubos = await this.repo.GetVistaCubosAsync(posicion.Value);
            ViewData["ULTIMO"] = numeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            return View(paginacionCubos);
        }

        //PAGINAR EN GRUPOS DE CUBOS CON LAMBDA DE LA VIEW
        public async Task<IActionResult> PaginarVistaCubos(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroPagina = 1;
            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaCubos();
            string html = "<div>";
            for(int i = 1;  i <= numeroRegistros; i+=5) //Ajustar aqui con la cantidad de cubos que queramos tanto aqui como en el repo
            {
                html += "<a href='PaginarVistaCubos?posicion="
                    + i + "'>Página " + numeroPagina + "</a> | ";
                numeroPagina += 1;
            }
            html += "</div>";
            ViewData["LINKS"] = html;
            List<VistaCubos> cubos = await this.repo.PaginarCubosEnGrupo(posicion.Value);
            return View(cubos);
        }

        public async Task<IActionResult> PaginarVistaCubosProcedure(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaCubos();
            ViewData["REGISTROS"] = numeroRegistros;
            List<Cubo> cubos = await this.repo.GetGrupoProcedureAsync(posicion.Value);
            return View(cubos);
        }
        public async Task<IActionResult> CubosMarca
            (int? posicion, string marca)
        {
            if (posicion == null)
            {
                posicion = 1;
                return View();
            }
            else
            {
                List<Cubo> cubos = await
                    this.repo.GetGrupoCubosMarcaAsync(posicion.Value, marca);
                int registros = await this.repo.GetNumeroCubosMarcaAsync(marca);
                ViewData["REGISTROS"] = registros;
                ViewData["MARCA"] = marca;
                return View(cubos);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CubosMarca
            (string marca)
        {
            //CUANDO BUSCAMOS, NORMALMENTE, EN QUE POSICION COMIENZA TODO? 
            List<Cubo> cubos = await
                this.repo.GetGrupoCubosMarcaAsync(1, marca);
            int registros = await this.repo.GetNumeroCubosMarcaAsync(marca);
            ViewData["REGISTROS"] = registros;
            ViewData["MARCA"] = marca;
            return View(cubos);
        }

        public async Task<IActionResult> CubosMarcaOut(int? posicion, string marca)
        {
            await InitializeMarcasAsync();

            if (posicion == null)
            {
                posicion = 1;
                return View();
            }
            else
            {
                ModelPaginacionCubos model = await this.repo.GetGrupoCubisMarcaOutAsync(posicion.Value, marca);
                ViewData["REGISTROS"] = model.NumeroRegistros;
                ViewData["MARCA"] = marca;
                return View(model.Cubos);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CubosMarcaOut(string marca)
        {
            await InitializeMarcasAsync();

            ModelPaginacionCubos model = await this.repo.GetGrupoCubisMarcaOutAsync(1, marca);
            ViewData["REGISTROS"] = model.NumeroRegistros;
            ViewData["MARCA"] = marca;
            return View(model.Cubos);
        }
        public async Task<List<string>> ObtenerTodasLasMarcas()
        {
            List<string> marcas = await this.repo.ObtenerTodasLasMarcas();
            return marcas;
        }

    }
}

