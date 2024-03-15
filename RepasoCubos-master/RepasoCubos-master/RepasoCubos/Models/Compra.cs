using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RepasoCubos.Models
{
    [Table("COMPRA")]
    public class Compra
    {
        [Column("id_compra")]
        public int IdCompra { get; set; }
        [Key]
        [Column("nombre_cubo")]
        public string NombreCubo { get; set; }
        [Column("precio")]
        public int Precio { get; set; }
        [Column("fechapedido")]
        public DateTime FechaPedido { get; set; }
    }
}
