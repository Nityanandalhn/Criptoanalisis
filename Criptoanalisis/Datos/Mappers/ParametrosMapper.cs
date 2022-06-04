using Datos.Dtos;
using Datos.Entidades;

namespace Datos.Mappers
{
    public class ParametrosMapper
    {
        public static ParametroDto FromEntity(Parametros parametro) => new ()
        {
            Id = parametro.Id,
            Tipo = parametro.Tipo,
            Mapea = parametro.Mapea,
            Valor = parametro.Valor,
            Endpoints = parametro.ParametrosEndpoints!.Select(x => x.Endpoints!.Url).ToList()!
        };

        public static Parametros FromDto(ParametroDto dto) => new()
        {
            Id = dto.Id,
            Mapea = dto.Mapea,
            Tipo = dto.Tipo,
            Valor = dto.Valor
        };

        public static Parametros FromCreateDto(ParametroCreateDto dto) => new()
        {
            Mapea = dto.Mapea,
            Tipo = dto.Tipo,
            Valor = dto.Valor
        };
    }
}
