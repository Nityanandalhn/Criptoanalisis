using Datos.Relaciones;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    [Table("usuario")]
    public class Usuario
    {
        [Required]
        [Column("mail")]
        public string? Mail { get; set; }
        [Column("pwd")]
        public string? Pwd { get; set; }
        [Column("login")]
        public string? Login { get; set; }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public ICollection<Intercambio>? Intercambios { get; set; }
    }
}
