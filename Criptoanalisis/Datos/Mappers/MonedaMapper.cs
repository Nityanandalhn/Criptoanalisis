using Datos.Dtos;
using Datos.Entidades;

namespace Datos.Mappers
{
    public class MonedaMapper
    {
        public static MonedaDto FromEntity(Moneda intercambio) => new ()
        {
            Nombre = intercambio.Nombre
        };

        public static Moneda FromDto(MonedaDto dto) => new()
        {
            Nombre = dto.Nombre
        };
    }
}
