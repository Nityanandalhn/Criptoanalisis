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
    }
}
