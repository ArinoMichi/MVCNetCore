using MvcCrudDepartamentosEF.Data;
using MvcCrudDepartamentosEF.Models;

namespace MvcCrudDepartamentosEF.Repositories
{
    public class RepositoryDepartamentos
    {
        private DepartamentoContext context;

        public RepositoryDepartamentos(DepartamentoContext context)
        {
            this.context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.context.Departamentos
                           select datos;
            return consulta.ToList();
        }

        public Departamento FindDepartamento(int id)
        {
            var consulta = from datos in this.context.Departamentos
                           where datos.IdDept == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void InsertDepartamento(Departamento departamento)
        {
            this.context.Departamentos.Add(departamento);
            this.context.SaveChanges();
        }

        public void DeleteDepartamento(int idDept)
        {
            Departamento dept = this.FindDepartamento(idDept);
            this.context.Departamentos.Remove(dept);
            this.context.SaveChanges();
        }

        public void Update(Departamento departamento)
        {
            Departamento dept = this.FindDepartamento(departamento.IdDept);
            dept.Nombre = departamento.Nombre;
            dept.Localidad = departamento.Localidad;

            this.context.SaveChanges();

        }


    }
}
