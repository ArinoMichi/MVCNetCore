using MvcNetCoreEF.Data;
using MvcNetCoreEF.Models;

namespace MvcNetCoreEF.Repositories
{
    public class RepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        //METODO PARA RECUPERAR LOS DISTINTOS OFICIOS
        public List<string> GetOficios()
        {
            var consulta = (from datos in this.context.Empleados
                           select datos.Oficio).Distinct();
            return consulta.ToList();
        }

        //METODO PARA RECUPERAR EMPLEADOS POR SU OFICIO
        public List<Empleado> GetEmpleadosOficio(string oficio)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.Oficio == oficio
                           select datos;
            return consulta.ToList();
        }

        //TENDREMOS UN METODO PARA INCREMENTAR EL SALARIO POR 
        //OFICIO Y DEVOLVERA EL MODEL CON LOS EMPLEADOS Y SU RESUMEN
        public ModelEmpleados IncrementarSalarioOficios(int incremento, string oficio)
        {
            //RECUPERAMOS LOS DATOS D ELOS EMPLEADOS DE UN OFICIO
            List<Empleado> empleados = this.GetEmpleadosOficio(oficio);
            //DEBEMOS RECORRER CADA EMPLEADO E IR INCREMENTANDO SU SALARIO
            foreach(Empleado empleado in empleados)
            {
                empleado.Salario += incremento;
            }
            //GUARDAMOS LOS CAMBIOS EN LA BASE DE DATOS
            this.context.SaveChanges();
            //MEDIANTE LAMBDA, DEBEMOS RECUPERAR EL RESUMEN DE LOS DATOS
            int suma = empleados.Sum(x => x.Salario);
            double media = empleados.Average(y => y.Salario);

            ModelEmpleados model = new ModelEmpleados();
            model.Empleados= empleados;
            model.SumaSalarial = suma;
            model.MediaSalarial = media;

            return model;
        }
    }
}
