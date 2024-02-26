using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaComics.Data;
using PracticaComics.Models;

#region STORED PROCEDURES
//CREATE PROCEDURE SP_INSERT_COMIC
//    @NOMBRE NVARCHAR(255),
//    @IMAGEN NVARCHAR(255),
//    @DESCRIPCION NVARCHAR(MAX)
//AS
//BEGIN
//    DECLARE @NUEVOID INT = (SELECT MAX(IDCOMIC) +1 FROM COMICS);

//INSERT INTO COMICS
//    VALUES (@NUEVOID, @NOMBRE, @IMAGEN, @DESCRIPCION);
//END;
#endregion


namespace PracticaComics.Repositories
{
    public class RepositoryComicsSqlServer : IRepositoryComics
    {

        private HospitalContext context;

        public RepositoryComicsSqlServer(HospitalContext context)
        {
            this.context = context;
        }

        public async Task DeleteComicAsync(int idComic)
        {
            var comicToDelete = await context.Comics.FindAsync(idComic);
            context.Comics.Remove(comicToDelete);
            await context.SaveChangesAsync();

        }

        public async Task<List<Comic>> GetComicsAsync()
        {
            var consulta = from datos in this.context.Comics
                           select datos;
            return await consulta.ToListAsync();
        }

        public Task<Comic> GetDetallesComicAsync(int idComic)
        {
            var consulta = from datos in this.context.Comics
                           where datos.idComic == idComic
                           select datos;
            return consulta.FirstOrDefaultAsync();
        }

        public async Task InsertComicAsync(Comic comic)
        {
            string sql = "SP_INSERT_COMIC @NOMBRE, @IMAGEN, @DESCRIPCION";
            var nombre = new SqlParameter("@NOMBRE", comic.Nombre);
            var imagen = new SqlParameter("@IMAGEN", comic.Imagen);
            var descripcion = new SqlParameter("@DESCRIPCION", comic.Descripcion);

            await context.Database.ExecuteSqlRawAsync(sql, nombre, imagen, descripcion);
        }

    }
}
