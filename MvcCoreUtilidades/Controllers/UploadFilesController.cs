using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperUploadFiles helperUploadFiles;

        public UploadFilesController(HelperUploadFiles helperUploadFiles)
        {
            this.helperUploadFiles = helperUploadFiles;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            string path = await this.helperUploadFiles.UploadFileAsync(fichero, Folders.Uploads);
            ViewData["MENSAJE"] = "Fichero subido a " + path;

            return View();
        }
    }
}
