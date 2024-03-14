using Microsoft.EntityFrameworkCore;
using PracticaCubos.Data;
using PracticaCubos.Models;

namespace PracticaCubos.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

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
