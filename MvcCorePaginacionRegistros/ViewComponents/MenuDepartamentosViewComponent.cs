using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.ViewComponents
{
    public class MenuDepartamentosViewComponent : ViewComponent
    {
        private RepositoryHospital repo;

        public MenuDepartamentosViewComponent(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Departamento> departamentos =
                await this.repo.GetDepartamentosAsync();
            return View(departamentos);
        }
    }

}
