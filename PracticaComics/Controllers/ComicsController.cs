using Microsoft.AspNetCore.Mvc;
using PracticaComics.Models;
using PracticaComics.Repositories;

namespace PracticaComics.Controllers
{
    public class ComicsController : Controller
    {
        private IRepositoryComics repo;

        public ComicsController(IRepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Comic> comics = await repo.GetComicsAsync();
            return View(comics);
        }

        public async Task<IActionResult> Details(int idComic)
        {
            Comic comic = await this.repo.GetDetallesComicAsync(idComic);
            return View(comic);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comic comic)
        {
            await this.repo.InsertComicAsync(comic);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idComic)
        {
            await this.repo.DeleteComicAsync(idComic);
            return RedirectToAction("Index");
        }
    }
}
