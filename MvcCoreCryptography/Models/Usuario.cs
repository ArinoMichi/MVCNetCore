using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreCryptography.Models
{
    [Table("USERS")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("SALT")]
        public string Salt { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("ACTIVO")]
        public bool Activo { get; set; }
        [Column("TOKENMAIL")]
        public string TokenMail { get; set; }

        //UNA VENTAJA CON EF ES QUE LOS TIPOS DE 
        //DATOS VARBINARY, BLOB, CLOB SON CONVERTIDOS
        //AUTOMATICAMENTE A byte[]
        [Column("PASS")]
        public byte[] Password { get; set; }
    }
}
