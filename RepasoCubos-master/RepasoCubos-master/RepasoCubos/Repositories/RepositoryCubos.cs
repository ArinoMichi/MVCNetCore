using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepasoCubos.Data;
using RepasoCubos.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.RegularExpressions;
using System.Data;

namespace RepasoCubos.Repositories
{
    #region COSITAS
    //    create view V_CUBOS_INDIVIDUAL 
    //as 
    //    select CAST(
    //    ROW_NUMBER() over (ORDER BY ID_CUBO) AS INT) AS POSICION,
    //    ISNULL(ID_CUBO, 0) AS ID_CUBO, NOMBRE, MODELO, MARCA, IMAGEN, PRECIO FROM CUBOS
    //go


    //    EJEMPLO DE OTRA VISTA
    //        create view V_GRUPO_EMPLEADOS 
    //as 
    //    select cast(
    //        row_number() over (order by apellido) as int) as posicion,  
    //        ISNULL(EMP_NO, 0) AS EMP_NO, APELLIDO
    //        , OFICIO, SALARIO, DEPT_NO FROM EMP
    //go

    //INCLUIR TODOS LOS DATOS DE LA TABLA PA K NO DE ERROR 
    //    create procedure SP_GRUPO_CUBOS
    //(@posicion int)
    //as 
    //    select ID_CUBO, NOMBRE, MODELO, IMAGEN, MARCA
    //    from V_CUBOS_INDIVIDUAL
    //    where POSICION >= @posicion and POSICION<(@posicion + 2) 
    //go


    //    create procedure SP_GRUPO_CUBOS_MARCA
    //(@posicion int, @marca nvarchar(50)) 
    //as 
    //select ID_CUBO, NOMBRE, MARCA, MODELO, IMAGEN, PRECIO from
    //    (select cast(
    //    ROW_NUMBER() OVER (ORDER BY NOMBRE) as int) AS POSICION
    //    , ID_CUBO, NOMBRE, MARCA, MODELO, IMAGEN, PRECIO
    //    from CUBOS
    //    where MARCA = @marca) as QUERY
    //    where QUERY.POSICION >= @posicion and QUERY.POSICION<(@posicion + 2) 
    //go



    //    create procedure SP_GRUPO_CUBOS_MARCA_OUT
    //(@posicion int, @marca nvarchar(50), @registros int out) 
    //as 
    //select @registros = count(ID_CUBO) from CUBOS
    //where MARCA = @marca
    //select ID_CUBO, NOMBRE, MARCA, MODELO, IMAGEN, PRECIO from
    //    (select cast(
    //    ROW_NUMBER() OVER (ORDER BY NOMBRE) as int) AS POSICION
    //    , ID_CUBO, NOMBRE, MARCA, MODELO, IMAGEN, PRECIO
    //    from CUBOS
    //    where MARCA = @marca) as QUERY
    //    where QUERY.POSICION >= @posicion and QUERY.POSICION<(@posicion + 2) 
    //go

    #endregion
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<int> GetNumeroRegistrosVistaCubos()
        {
            return await this.context.VistaCubos.CountAsync();
        }
        public async Task<VistaCubos> GetVistaCubosAsync(int posicion)
        {
            VistaCubos vista = await this.context.VistaCubos.Where(z => z.Posicion == posicion).FirstOrDefaultAsync();
            return vista;
        }

        public async Task<List<VistaCubos>> PaginarCubosEnGrupo(int posicion)
        {
            var consulta = from datos in this.context.VistaCubos
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 5) // de dos en dos los cubos o cantidad aqui y controller
                           select datos;

            return await consulta.ToListAsync();
        }

        //CON PROCEDURE LLAMAMOS AL MODELO NO A LA VISTA PORQUE EL PROCEDURE NO DEVUELVE POSICION, SOLO DEVUELVE ID
        public async Task<List<Cubo>> GetGrupoProcedureAsync(int posicion)
        {
            string sql = "SP_GRUPO_CUBOS @posicion";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            var consulta =  this.context.Cubos.FromSqlRaw(sql, pamPosicion);
            return await consulta.ToListAsync();
        }

        public async Task<int> GetNumeroCubosMarcaAsync(string marca)
        {
            return await this.context.Cubos
                .Where(z => z.Marca == marca).CountAsync();
        }

        public async Task<List<Cubo>> GetGrupoCubosMarcaAsync
            (int posicion, string marca)
        {
            string sql = "SP_GRUPO_CUBOS_MARCA @posicion, @marca";
            SqlParameter pamPosicion =
                new SqlParameter("@posicion", posicion);
            SqlParameter pamMarca =
                new SqlParameter("@marca", marca);
            var consulta = this.context.Cubos.FromSqlRaw
                (sql, pamPosicion, pamMarca);
            return await consulta.ToListAsync();
        }

        //public async Task<List<string>> GetAllMarcasCubos()
        //{
        //    List<string> marcas = await this.context.Cubos
        //        .Select(c => c.Marca)
        //        .Distinct()
        //        .ToListAsync();
        //    return marcas;
        //}


        //EL CONTROLLER NOS VA A DAR UNA POSICION Y UNA MARCA 
        //DEBEMOS DEVOLVER LOS CUBOS Y EL NUMERO DE REGISTROS 
        public async Task<ModelPaginacionCubos> GetGrupoCubisMarcaOutAsync
            (int posicion, string marca)
        {
            string sql = "SP_GRUPO_CUBOS_MARCA_OUT @posicion, @marca, "
                + " @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamMarca = new SqlParameter("@marca", marca);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);
            pamRegistros.Direction = ParameterDirection.Output;
            var consulta =
                this.context.Cubos.FromSqlRaw
                (sql, pamPosicion, pamMarca, pamRegistros);
            //PRIMERO DEBEMOS EJECUTAR LA CONSULTA PARA PODER RECUPERAR  
            //LOS PARAMETROS DE SALIDA 
            List<Cubo> cubos = await consulta.ToListAsync();
            int registros = (int)pamRegistros.Value;
            return new ModelPaginacionCubos
            {
                NumeroRegistros = registros,
                Cubos = cubos
            };
        }

        public async Task<List<string>> ObtenerTodasLasMarcas()
        {
            List<string> marcas = await this.context.Cubos
                .Select(c => c.Marca)
                .Distinct()
                .ToListAsync();
            return marcas;
        }

        /// CARRITO A PARTIR DE AQUI 
        /// 
        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }
        public async Task<Cubo> FindCuboAsync(int idCubo)
        {
            return await this.context.Cubos.FirstOrDefaultAsync(z => z.IdCubo == idCubo);
        }

        public async Task<List<Cubo>>
            GetCubosSessionAsync(List<int> ids)
        {
            //PARA REALIZAR UN IN DENTRO DE LINQ, DEBEMOS HACERLO 
            //CON Collection.Contains(dato a buscar)
            //select * from EMP where EMP_NO in (7777,8888,9999)
            var consulta = from datos in this.context.Cubos
                           where ids.Contains(datos.IdCubo)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }

        public async Task EjecutarCompra(List<int> ids)
        {
            List<Cubo> cubos = await GetCubosSessionAsync(ids);

            if (cubos != null && cubos.Any())
            {
                int nuevoId = this.context.Compras.Max(c => (int?)c.IdCompra) ?? 0;
                nuevoId++;

                for (int i = 0; i < cubos.Count; i++)
                {
                    Compra compra = new Compra
                    {
                        IdCompra = nuevoId,  // Mismo IdCompra para todos los registros
                        NombreCubo = cubos[i].Nombre,
                        Precio = cubos[i].Precio,
                        FechaPedido = DateTime.Now
                    };
                    this.context.Compras.Add(compra);
                }
                await this.context.SaveChangesAsync();
            }
        }



    }
}
