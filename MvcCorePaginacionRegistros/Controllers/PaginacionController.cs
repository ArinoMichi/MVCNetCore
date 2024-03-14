using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.Controllers
{
    public class PaginacionController : Controller
    {
        private RepositoryHospital repo;

        public PaginacionController(RepositoryHospital repo)
        {
            this.repo = repo;
        }
        
        public async Task<IActionResult> PaginarRegistroVistaDepartamento(int? posicion)
        {
            if(posicion == null)
            {
                //PONSEMOS LA POSICION EN EL PRIMER REGISTRO
                posicion = 1;
            }
            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaDepartamentos();

            int siguiente = posicion.Value + 1;
            //DBEMOS COMPROBAR QUE NO PASAMOS DEL NUMERO DE REGISTROS
            if(siguiente > numeroRegistros)
            {
                //EFECTO OPTICO
                siguiente = numeroRegistros;
            }
            int anterior = posicion.Value -1;
            if(anterior < 1)
            {
                anterior = 1;
            }
            VistaDepartamento vista= await
                this.repo.GetVistaDepartamentoAsync(posicion.Value);
            ViewData["ULTIMO"] = numeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;

            return View(vista);
        }
        public async Task<IActionResult>
            PaginarGrupoVistaDepartamento(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            //<a href='paginargrupo?posicion=1'>Pagina 1</a>
            //<a href='paginargrupo?posicion=3'>Pagina 2</a>
            //<a href='paginargrupo?posicion=5'>Pagina 3</a>
            //<a href='paginargrupo?posicion=7'>Pagina 4</a>
            int numeroPagina = 1;
            int numeroRegistros = await
                this.repo.GetNumeroRegistrosVistaDepartamentos();
            //VAMOS A GENERAR EL CODIGO HTML DESDE NUESTRO CONTROLLER
            string html = "<div>";
            for (int i = 1; i <= numeroRegistros; i += 2)
            {
                html +=
                    "<a href='PaginarGrupoVistaDepartamento?posicion="
                    + i + "'>Página " + numeroPagina + "</a> | ";
                numeroPagina += 1;
            }
            html += "</div>";
            ViewData["LINKS"] = html;
            List<VistaDepartamento> departamentos =
                await this.repo.GetGrupoVistaDepartamentoAsync(posicion.Value);
            return View(departamentos);
        }
        public async Task<IActionResult>
            PaginarGrupoDepartamentos(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeroRegistros = await
                this.repo.GetNumeroRegistrosVistaDepartamentos();
            ViewData["REGISTROS"] = numeroRegistros;
            List<Departamento> departamentos = await
                this.repo.GetGrupoDepartamentosAsync(posicion.Value);
            return View(departamentos);
        }

        public async Task<IActionResult>
            PaginarGrupoEmpleados(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int registros = await this.repo.GetNumeroEmpleadosAsync();
            List<Empleado> empleados =
                await this.repo.GetGrupoEmpleadosAsync(posicion.Value);
            ViewData["REGISTROS"] = registros;
            return View(empleados);
        }

        public async Task<IActionResult> EmpleadosOficio
            (int? posicion, string oficio)
        {
            if (posicion == null)
            {
                posicion = 1;
                return View();
            }
            else
            {
                List<Empleado> empleados = await
                    this.repo.GetGrupoEmpleadosOficioAsync(posicion.Value, oficio);
                int registros = await this.repo.GetNumeroEmpleadosOficioAsync(oficio);
                ViewData["REGISTROS"] = registros;
                ViewData["OFICIO"] = oficio;
                return View(empleados);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EmpleadosOficio
            (string oficio)
        {
            // Cuando buscamos, normalmente, en que posición comienza todo
            List<Empleado> empleados =
                await this.repo.GetGrupoEmpleadosOficioAsync(1, oficio);
            int registros = await this.repo.GetNumeroEmpleadosOficioAsync(oficio);
            ViewData["REGISTROS"] = registros;
            ViewData["OFICIO"] = oficio;
            return View(empleados);
        }

        public async Task<IActionResult> EmpleadosOficioOut
    (int? posicion, string oficio)
        {
            if (posicion == null)
            {
                posicion = 1;
                return View();
            }
            else
            {
                ModelPaginacionEmpleados model = await this.repo.GetGrupoEmpleadosOficioOutAsync(posicion.Value, oficio);
                ViewData["REGISTROS"] = model.NumeroRegistros;
                ViewData["OFICIO"] = oficio;
                return View(model.Empleados);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EmpleadosOficioOut
            (string oficio)
        {
            // Cuando buscamos, normalmente, en que posición comienza todo
            ModelPaginacionEmpleados model = await this.repo.GetGrupoEmpleadosOficioOutAsync(1, oficio);
            ViewData["REGISTROS"] = model.NumeroRegistros;
            ViewData["OFICIO"] = oficio;
            return View(model.Empleados);
        }



    }
}