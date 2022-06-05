using Datos.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Relaciones
{
    //Modelo M-M que permite incluir propiedades de relación
    //Encontré esta opción antes del modelo M-M 'simple'
    //https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
    [Table("endpoint_parametros")]
    public class ParametroEndpoint
    {
        [Required]
        [Column("cod_edp")]
        public int EndpointId { get; set; }
        [Required]
        [Column("cod_prm")]
        public int ParametroId { get; set; }
        public virtual Endpoints? Endpoints { get; set; }
        public virtual Parametro? Parametros { get; set; }
    }
}
