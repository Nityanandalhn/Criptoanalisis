namespace Datos.Dtos
{
    public class UsuarioDto
    {
        public string? Mail { get; set; }
        public string? Pwd { get; set; }
        public string? Login { get; set; }
        public int Id { get; set; }
        public IEnumerable<string>? Intercambios { get; set; }
        public IEnumerable<string>? EndpointsActivos { get; set; }
    }
}
