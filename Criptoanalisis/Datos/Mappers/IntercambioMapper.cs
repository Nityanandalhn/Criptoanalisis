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
            Cerrado = intercambio.Cerrado,
            Exchange = intercambio.Exchange,
            Fecha = intercambio.Fecha,
            Nombre = intercambio.Nombre,
            Reciente = intercambio.Reciente,
            Volumen = intercambio.Volumen,
            Endpoints = intercambio.Endpoints!.Select(x => x.Url)!
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
            Cerrado = dto.Cerrado,
            Exchange = dto.Exchange,
            Fecha = dto.Fecha,
            Nombre = dto.Nombre,
            Reciente = dto.Reciente,
            Volumen = dto.Volumen
        };
    }
}
