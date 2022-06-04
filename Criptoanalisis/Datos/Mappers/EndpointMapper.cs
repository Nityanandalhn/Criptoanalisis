using Datos.Dtos;

namespace Datos.Mappers
{
    public class EndpointMapper
    {
        public static EndpointDto fromEntity(Entidades.Endpoint endpoint) => new ()
        {
            Id = endpoint.Id,
            Tipo = endpoint.Tipo,
            Url = endpoint.Url
        };

        public static Entidades.Endpoint fromDto(EndpointDto dto) => new()
        {
            Url = dto.Url,
            Tipo = dto.Tipo,
            Id = dto.Id
        };
    }
}
