using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    [Table("parametros")]
    public class Parametro
    {
        [Required]
        [Column("valor")]
        public string? Valor { get; set; }
        [Required]
        [Column("mapping_modelo")]
        public string? Mapea { get; set; }
        [Required]
        [Column("tipo")]
        public string? Tipo { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public virtual ICollection<Relaciones.ParametroEndpoint>? ParametrosEndpoints { get; set; }
    }
}
