using Datos.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Relaciones
{
    [Table("endpoint_parametros")]
    public class ParametrosEndpoint
    {
        [Required]
        [Column("cod_edp")]
        public int EndpointId { get; set; }
        [Required]
        [Column("cod_prm")]
        public int ParametroId { get; set; }
        public virtual Endpoints? Endpoints { get; set; }
        public virtual Parametros? Parametros { get; set; }
    }
}
