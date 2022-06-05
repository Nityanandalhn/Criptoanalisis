using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    [Table("moneda")]
    public class Moneda
    {
        [Key]
        [Column("nombre")]
        public string? Nombre { get; set; }
    }
}
