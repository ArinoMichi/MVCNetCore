namespace MvcCoreProceduresEF.Models
{
    public class TrabajadoresModel
    {
        public List<Trabajador> Trabajadores { get; set; }
        public int Personas { get; set; }
        public int SumaSalarial { get; set; }
        public int MediaSalarial { get; set; }
    }
}
