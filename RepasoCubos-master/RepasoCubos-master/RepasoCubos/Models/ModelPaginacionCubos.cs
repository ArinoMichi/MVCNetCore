using System.ComponentModel.DataAnnotations;

namespace RepasoCubos.Models
{
    public class ModelPaginacionCubos
    {
        public int NumeroRegistros { get; set; }
        public List<Cubo> Cubos { get; set; }
    }
}
