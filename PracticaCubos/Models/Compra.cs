using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaCubos.Models
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

//CREATE TABLE[dbo].[COMPRA] (

//    [id_compra][int] NOT NULL,

//    [nombre_cubo] [nvarchar] (50) NULL,

//    [precio] [int] NOT NULL,

//    [fechapedido] [datetime] NOT NULL
//)
//GO
