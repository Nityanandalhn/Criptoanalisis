using Datos.Dtos;
using Datos.Entidades;

namespace Datos.Mappers
{
    public class UsuarioMapper
    {
        public static UsuarioDto FromEntity(Usuario usuario) => new ()
        {
            Id = usuario.Id,
            Login = usuario.Login,
            Mail = usuario.Mail,
            Pwd = usuario.Pwd,
            Intercambios = usuario.Intercambios!.Select(x => x.Nombre)!,
            EndpointsActivos = usuario.EndpointsActivos!.Select(x => x.Url)!
        };

        public static Usuario FromDto(UsuarioDto dto) => new()
        {
            Id = dto.Id,
            Login = dto.Login,
            Mail = dto.Mail,
            Pwd = dto.Pwd
        };

        public static Usuario FromCreateDto(UsuarioCreateDto dto) => new()
        {
            Login = dto.Login,
            Mail = dto.Mail,
            Pwd = dto.Pwd
        };
    }
}
