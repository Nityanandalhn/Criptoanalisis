using Datos.Dtos;
using Datos.Entidades;

namespace Datos.Mappers
{
    public class EndpointMapper
    {
        public static EndpointDto FromEntity(Endpoints endpoint) => new ()
        {
            Id = endpoint.Id,
            Tipo = endpoint.Tipo,
            Url = endpoint.Url,
            Parametros = endpoint.ParametrosEndpoints!.Select(x => x.Parametros!.Valor).ToList()! //Problema de rendimiento
            //Acabo de darme cuenta que llamar a ToList hace que entity framework lance las query.
            //Pendiente de optimizar
        };

        public static Endpoints FromDto(EndpointDto dto) => new()
        {
            Url = dto.Url,
            Tipo = dto.Tipo,
            Id = dto.Id
        };

        public static Endpoints FromCreateDto(EndpointCreateDto dto) => new()
        {
            Url = dto.Url,
            Tipo = dto.Tipo
        };
    }
}
