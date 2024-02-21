using Microsoft.AspNetCore.Mvc;
using MvcCoreProceduresEF.Models;
using MvcCoreProceduresEF.Repositories;

namespace MvcCoreProceduresEF.Controllers
{
    public class EmpleadosViewController : Controller
    {
        private RepositoryViewEmpleados repo;

        public EmpleadosViewController(RepositoryViewEmpleados repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<ViewEmpleado> model = await this.repo.GetEmpleadosAsync();
            return View(model);
        }
    }
}
