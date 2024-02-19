using Microsoft.EntityFrameworkCore;
using MvcNetCoreEF.Models;
using System.Security.Cryptography.X509Certificates;

namespace MvcNetCoreEF.Data
{
    public class HospitalContext : DbContext
    {
        //TENDREMOS UN CONSTRUCTOR QUE RECIBA LAS OPCIONES DE
        //INICIO Y CONEXION DE LA BASE DE DATOS
        //DICHAS OPCDIONES DEBEN SER ENVIADAS A LA CLASE BASE
        public HospitalContext(DbContextOptions<HospitalContext> options) 
            :base(options)
        { }
        //POR CADA MODEL NECESITAMOS UNA COLECCION DbSet QUE SERA LA QUE 
        //UTILIZAREMOS PARA LAS CONSULTAS LINQ
        public DbSet<Hospital> Hospitales { get; set; }

        public DbSet<Empleado> Empleados { get; set; }


    }
}
