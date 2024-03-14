using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.ViewComponents
{
    public class MenuCochesViewComponent : ViewComponent
    {
        //POR SUPUESTO, TENEMOS INYECCION
        private RepositoryCoches repo;

        public MenuCochesViewComponent(RepositoryCoches repo)
        {
            this.repo = repo;
        }

        //PODRIAMOS TENER TODOS LOS METODOS QUE DESEEMOS EN LA CLASE
        //ES OBLIGATORIO TENER UN METODO LLAMADO InvokeAsync QUE 
        //SERA EL QUE ADMINISTRE EL DIBUJO CON EL MODEL
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Coche> coches = this.repo.GetCoches();
            return View(coches);
        }
    }
}
