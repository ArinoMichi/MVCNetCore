using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using PracticaComics.Data;
using PracticaComics.Models;
using System.Data;

#region STORED PROCEDURES
//CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC(
//    P_NOMBRE NVARCHAR2,
//    P_IMAGEN NVARCHAR2,
//    P_DESCRIPCION NVARCHAR2
//)
//AS
//    NUEVO_ID NUMBER;
//BEGIN

//    SELECT MAX(IDCOMIC) +1 INTO NUEVO_ID FROM COMICS;

//INSERT INTO COMICS (IDCOMIC, NOMBRE, IMAGEN, DESCRIPCION)
//    VALUES(NUEVO_ID, P_NOMBRE, P_IMAGEN, P_DESCRIPCION);

//END;
#endregion

namespace PracticaComics.Repositories
{
    public class RepositoryComicsOracle : IRepositoryComics
    {
        private HospitalContext context;

        public RepositoryComicsOracle(HospitalContext context)
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
            string sql = "BEGIN SP_INSERT_COMIC(:NOMBRE, :IMAGEN, :DESCRIPCION); END;";

            var nombre = new OracleParameter(":NOMBRE", OracleDbType.NVarchar2, ParameterDirection.Input) { Value = comic.Nombre };
            var imagen = new OracleParameter(":IMAGEN", OracleDbType.NVarchar2, ParameterDirection.Input) { Value = comic.Imagen };
            var descripcion = new OracleParameter(":DESCRIPCION", OracleDbType.NVarchar2, ParameterDirection.Input) { Value = comic.Descripcion };

            await context.Database.ExecuteSqlRawAsync(sql, nombre, imagen, descripcion);
        }

    }
}
