using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    [Table("intercambio")]
    [MetaDatosJson("Nombre")]
    public class Intercambio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nombre")]
        public string? Nombre{ get; set; }
        [Required]
        [Column("fecha")]
        public DateTimeOffset Fecha { get; set; }
        [Required]
        [Column("intercambiado")]
        public string? Intercambiado { get; set; }
        [Column("volumen")]
        public double Volumen { get; set; }
        [Column("abierto")]
        public double Abierto { get; set; }
        [Column("cerrado")]
        public double Cerrado { get; set; }
        [Column("alto")]
        public double Alto { get; set; }
        [Column("bajo")]
        public double Bajo { get; set; }
        [Column("reciente")]
        public double Reciente { get; set; }

        [Column("cod_edp")]
        public int EndpointId { get; set; }
        public Endpoints? Endpoint { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
