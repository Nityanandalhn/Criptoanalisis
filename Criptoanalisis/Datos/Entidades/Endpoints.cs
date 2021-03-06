using Datos.Relaciones;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    [Table("endpoint")]
    public class Endpoints
    {
        [Required]
        [Column("url")]
        public string? Url { get; set; }
        [Required]
        [Column("tipo")]
        public string? Tipo { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public virtual ICollection<ParametroEndpoint>? ParametrosEndpoints { get; set; }
        public ICollection<Intercambio>? Intercambios { get; set; }
        public ICollection<Usuario>? UsuariosActivos { get; set; }
    }
}
