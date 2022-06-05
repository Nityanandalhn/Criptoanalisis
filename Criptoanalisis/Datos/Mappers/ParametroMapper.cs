using Datos.Dtos;
using Datos.Entidades;

namespace Datos.Mappers
{
    public class ParametroMapper
    {
        public static ParametroDto FromEntity(Parametro parametro) => new ()
        {
            Id = parametro.Id,
            Tipo = parametro.Tipo,
            Mapea = parametro.Mapea,
            Valor = parametro.Valor,
            Endpoints = parametro.ParametrosEndpoints!.Select(x => x.Endpoints!.Url).ToList()!
        };

        public static Parametro FromDto(ParametroDto dto) => new()
        {
            Id = dto.Id,
            Mapea = dto.Mapea,
            Tipo = dto.Tipo,
            Valor = dto.Valor
        };

        public static Parametro FromCreateDto(ParametroCreateDto dto) => new()
        {
            Mapea = dto.Mapea,
            Tipo = dto.Tipo,
            Valor = dto.Valor
        };

        public static ParametroCreateDto CreateDtoFromDto(ParametroDto dto) => new()
        {
            Mapea = dto.Mapea,
            Tipo = dto.Tipo,
            Valor = dto.Valor
        };
    }
}
