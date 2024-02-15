namespace NetCoreLinqToSqlInjection.Models
{
    public interface ICoche
    {
        //LAS INTERFACES NO TIENEN AMBITO, SOLAMENTE
        //LOS METODOS Y PROPIEDADES QUE DEBE TENER UN OBJETO

        string Marca { get; set; }
        string Modelo { get; set; }
        string Imagen { get; set; }
        int Velocidad { get; set; }
        int VelocidadMaxima { get; set; }

        int Acelerar();
        int Frenar();
    }
}
