using Datos.Dtos;

namespace Datos.Mappers
{
    public class EndpointMapper
    {
        public static EndpointDto FromEntity(Entidades.Endpoint endpoint) => new ()
        {
            Id = endpoint.Id,
            Tipo = endpoint.Tipo,
            Url = endpoint.Url,
            Parametros = endpoint.ParametrosEndpoints!.Select(x => x.Parametros!.Salida).ToList()!.Concat(endpoint.ParametrosEndpoints!.Select(x => x.Parametros!.Entrada).ToList()!).ToList()!
        };

        public static Entidades.Endpoint FromDto(EndpointDto dto) => new()
        {
            Url = dto.Url,
            Tipo = dto.Tipo,
            Id = dto.Id
        };
    }
}
