using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCorePaginacionRegistros.Models
{
    [Table("V_DEPARTAMENTOS_INDIVIDUAL")]
    public class VistaDepartamento
    {
        [Key]
        [Column("DEPT_NO")]
        public int IdDepartamento { get; set; }
        [Column("DNOMBRE")]
        public string Nombre { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
        [Column("POSICION")]
        public int Posicion { get; set; }
    }
}
