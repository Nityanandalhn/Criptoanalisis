namespace Datos.Dtos
{
    public class ParametroDto
    {
        public string? Tipo { get; set; }
        public string? Valor { get; set; }
        public string? Mapea { get; set; }
        public int Id { get; set; }
        public List<string>? Endpoints { get; set; }
    }
}
