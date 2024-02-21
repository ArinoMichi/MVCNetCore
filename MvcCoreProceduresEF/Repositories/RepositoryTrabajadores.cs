using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;

namespace MvcCoreProceduresEF.Repositories
{
    public class RepositoryTrabajadores
    {
        private HospitalContext context;

        public RepositoryTrabajadores(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Trabajador>> GetTrabajadoresAsync()
        {
            var consulta = from datos in this.context.Trabajadores
                           select datos;
            return await consulta.ToListAsync();
        }
        public async Task<List<string>> GetOficiosAsync()
        {
            var consulta = (from datos in this.context.Trabajadores
                            select datos.Oficio).Distinct();
            return await consulta.ToListAsync();
        }


        public async Task<TrabajadoresModel> GetTrabajadoresOficioAsync(string oficio)
        {
            //LA UNICA DIFERENCIA ES QUE DEBEMOS INCLUIR LA PALABRA 
            //OUT EN CADA PARAMETRO DE SALIDA
            //SP_NOMBREPROCEDURE @PARAM1, @PARAM2, @PARAM3 OUT
            string sql = "SP_TRABAJADORES_OFICIO @oficio, @personas OUT, " +
                "@media OUT, @suma OUT";
            SqlParameter pamOficio = new SqlParameter("@oficio", oficio);
            //LOS PARAMETROS DE SALIDA SIEMPRE LLEVARAN UN VALOR POR DEFECO
            SqlParameter pamPersonas = new SqlParameter("@personas", -1);
            SqlParameter pamMedia = new SqlParameter("@media", -1);
            SqlParameter pamSuma = new SqlParameter("@suma", -1);

            //INDICAMOS LA DIRECCION DE LOS PARAMETROS OUT
            pamPersonas.Direction = System.Data.ParameterDirection.Output;
            pamMedia.Direction = System.Data.ParameterDirection.Output;
            pamSuma.Direction = System.Data.ParameterDirection.Output;
            //EJECUTAMOS LA CONSULTA DE SELECCION
            var consulta = this.context.Trabajadores.FromSqlRaw(sql,
                pamOficio, pamPersonas, pamMedia, pamSuma);

            //CREAMOS NUESTRO MODEL PARA RECUPERAR LOS DATOS
            TrabajadoresModel model = new TrabajadoresModel();
            //LOS PARAMETROS SE RECUPERAN DESPUES DE EXTRAER LOS 
            //DATOS DEL SELECT (CUANDO SE CIERRA EL Reader)
            model.Trabajadores = await consulta.ToListAsync();
            model.Personas = int.Parse(pamPersonas.Value.ToString());
            model.MediaSalarial = int.Parse(pamMedia.Value.ToString());
            model.SumaSalarial = int.Parse(pamSuma.Value.ToString());

            return model;
        }
    }
}
