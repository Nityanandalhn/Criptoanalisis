namespace Datos.Dtos
{
    public class EndpointDto
    {
        public string? Url { get; set; }

        public string? Tipo { get; set; }

        public int Id { get; set; }

        public List<string>? Parametros { get; set; }
    }
}
