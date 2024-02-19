namespace MvcNetCoreEF.Models
{
    public class ModelEmpleados
    {
        public List<Empleado> Empleados { get; set; }
        public int SumaSalarial { get; set; }
        public double MediaSalarial { get; set; }
    }
}
