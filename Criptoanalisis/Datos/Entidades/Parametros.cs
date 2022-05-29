using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    [Table("parametros")]
    public class Parametros
    {
        [Required]
        [Column("entrada")]
        public string? Entrada { get; set; }
        [Required]
        [Column("salida")]
        public string? Salida { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public ICollection<Entidades.Endpoint>? EndpointsS { get; set; }
    }
}
