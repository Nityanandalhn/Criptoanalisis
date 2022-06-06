using Datos.Dtos;
using Datos.Entidades;

namespace Datos.Mappers
{
    public class IntercambioMapper
    {
        public static IntercambioDto FromEntity(Intercambio intercambio) => new ()
        {
            Id = intercambio.Id,
            Abierto = intercambio.Abierto,
            Alto = intercambio.Alto,
            Bajo = intercambio.Bajo,
            Ultimo = intercambio.Ultimo,
            Intercambiado = intercambio.Intercambiado,
            Fecha = intercambio.Fecha,
            Nombre = intercambio.Nombre,
            Reciente = intercambio.Reciente,
            Volumen = intercambio.Volumen,
            Endpoint = intercambio.Endpoint!.Url
        };

        public static Intercambio FromDto(IntercambioDto dto) => new()
        {
            Id = dto.Id,
        };

        public static Intercambio FromCreateDto(IntercambioCreateDto dto) => new()
        {
            Abierto = dto.Abierto,
            Alto = dto.Alto,
            Bajo = dto.Bajo,
            Ultimo = dto.Ultimo,
            Intercambiado = dto.Intercambiado,
            Fecha = dto.Fecha,
            Nombre = dto.Nombre,
            Reciente = dto.Reciente,
            Volumen = dto.Volumen
        };
    }
}
