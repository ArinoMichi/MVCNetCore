using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.Controllers
{
    public class DepartamentosController : Controller
    {
        private RepositoryHospital repo;

        public DepartamentosController(RepositoryHospital repo)
        {
            this.repo = repo;
        }


        public async Task<IActionResult> Index()
        {
            List<Departamento> departamentos = 
                await this.repo.GetDepartamentosAsync();
            return View(departamentos);
        }

        public async Task<IActionResult> Detalles(int? posicion, int idDept)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            ModelDepartamentosDetalles model =
                await this.repo.GetDetallesDepartamentoEmpleadosAsync
                (posicion.Value, idDept);
            ViewData["DEPART"] = model.Departamento;
            return View();
        }

        public async Task<IActionResult> _Empleado(int? posicion, int iddepart)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            ModelDepartamentosDetalles model =
                await this.repo.GetDetallesDepartamentoEmpleadosAsync
                (posicion.Value, iddepart);
            int numeroRegistros = model.Registros;
            int siguiente = posicion.Value + 1;
            if (siguiente > numeroRegistros)
            {
                siguiente = numeroRegistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ÚLTIMO"] = numeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["DEPART"] = model.Departamento;
            ViewData["POSICION"] = posicion;
            return PartialView("_Empleado", model.Empleado);
        }

    }
}
