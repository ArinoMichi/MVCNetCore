using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreEFMultiplesBBDD.Models
{
    [Table("v_empleados")]
    public class Empleado
    {
        [Key]
        [Column("EMP_NO")]
        public int EmpNo { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("DEPT_NO")]
        public int DeptNo { get; set; }
        [Column("DNOMBRE")]
        public string Dnombre { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
    }
}
