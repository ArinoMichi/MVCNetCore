namespace MvcCoreAdoNet.Models
{
    public class Mascota
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Raza { get; set; }

        public Mascota(string nombre, int edad, string raza)
        {
            this.Nombre = nombre;
            this.Edad = edad;
            this.Raza = raza;
        }
    }
}
