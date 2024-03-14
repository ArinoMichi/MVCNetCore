using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaCubos.Models
{
    [Table("CUBOS")]
    public class Cubo
    {
        [Key]
        [Column("id_cubo")]
        public int IdCubo { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("modelo")]
        public string Modelo { get; set; }
        [Column("marca")]
        public string Marca { get; set; }
        [Column("imagen")]
        public string Imagen { get; set; }
        [Column("precio")]
        public int Precio { get; set; }
    }
}


//CREATE TABLE[dbo].[CUBOS] (

//    [id_cubo][int] NOT NULL PRIMARY KEY,

//    [nombre] [nvarchar] (500) NOT NULL,

//    [modelo] [nvarchar] (500) NOT NULL,

//    [marca] [nvarchar] (500) NOT NULL,

//    [imagen] [nvarchar] (500) NOT NULL,

//    [precio] [int] NOT NULL)
//GO