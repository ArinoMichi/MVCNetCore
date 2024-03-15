using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RepasoCubos.Models
{
    [Table("V_CUBOS_INDIVIDUAL")]
    public class VistaCubos
    {
        [Key]
        [Column("ID_CUBO")]
        public int IdCubo { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("MODELO")]
        public string Modelo { get; set; }
        [Column("MARCA")]
        public string Marca { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("PRECIO")]
        public int Precio { get; set; }
        [Column("POSICION")]
        public int Posicion { get; set; }
    }
}
