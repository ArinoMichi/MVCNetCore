using PracticaComics.Models;

namespace PracticaComics.Repositories
{
    public interface IRepositoryComics
    {
        Task<List<Comic>> GetComicsAsync();

        Task<Comic> GetDetallesComicAsync(int idComic);

        Task InsertComicAsync(Comic comic);
        Task DeleteComicAsync(int idComic);
    }
}
