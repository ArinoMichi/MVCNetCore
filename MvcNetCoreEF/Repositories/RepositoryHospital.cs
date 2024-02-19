using MvcNetCoreEF.Data;
using MvcNetCoreEF.Models;

namespace MvcNetCoreEF.Repositories
{
    public class RepositoryHospital
    {
        //LA CLASE REPO SIEMPRE RECIBIRA EL CONTEXT
        //MEDIANTE INYECCION DE DEPENDENCIAS
        private HospitalContext context;
        
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public List<Hospital> GetHospitales()
        {
            var consulta = from datos in this.context.Hospitales
                           select datos;
            return consulta.ToList();
        }

        public Hospital FindHospital(int idHospital)
        {
            var consulta = from datos in this.context.Hospitales
                           where datos.IdHospital == idHospital
                           select datos;
            //PODRIA SER QUE NO ENCONTRASEMOS HOSPITAL POR EL ID
            //TENEMOS UN METODO QUE ES FirstOrDefault() QUE DEVUELVE
            //EL PRIMER REGISTRO O EL VALOR POR DEFECTO DEL MODEL
            return consulta.FirstOrDefault();
        }

        public void InsertHospital(Hospital hospital)
        {
            //INSTANCIAR NUESTRO MODEL
            Hospital hosp = hospital;
            //ASIGNAMOS LAS PROPIEDADES
            //AÑADIMOS EL MODEL A NUESTRA COLECCION DbSet
            this.context.Hospitales.Add(hosp);
            //ALMACENAMOS LOS DATOS EN LA BASE DE DATOS
            this.context.SaveChanges();
        }

        public void DeleteHospita(int idHospital)
        {
            //BUSCAMOS EL MODEL HOSPITAL PARA ELIMINARLO
            Hospital hospital = this.FindHospital(idHospital);
            //ELIMINAMOS EL OBJETO EN EL DbSet
            this.context.Hospitales.Remove(hospital);
            //ALMACENAMOS LOS CAMBIOS EN LA BASE DE DATOS
            this.context.SaveChanges();
        }

        public void UpdateHospital(Hospital hospital)
        {
            //BUSCAMOS EL HOSPITAL POR ID
            Hospital hospitalOld = this.FindHospital(hospital.IdHospital);
            //MODIFICAMOS SUS DATOS
            hospitalOld.IdHospital = hospital.IdHospital;
            hospitalOld.Nombre = hospital.Nombre;
            hospitalOld.Direccion = hospital.Direccion;
            hospitalOld.Telefono = hospital.Telefono;
            hospitalOld.Camas = hospital.Camas;
            //DIRECTAMENTE MODIFICAMOS LA BASE DE DATOS
            this.context.SaveChanges();
        }

    }
}
