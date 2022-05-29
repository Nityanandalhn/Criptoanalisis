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
        public virtual Entidades.Endpoint? Endpoint { get; set; }
        public virtual Entidades.Parametros? Parametros { get; set; }
    }
}
