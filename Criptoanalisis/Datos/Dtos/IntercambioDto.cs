namespace Datos.Dtos
{
    public class IntercambioDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTimeOffset Fecha { get; set; }
        public string? Intercambiado { get; set; }
        public double Volumen { get; set; }
        public double Abierto { get; set; }
        public double Cerrado { get; set; }
        public double Alto { get; set; }
        public double Bajo { get; set; }
        public double Reciente { get; set; }

        public string? Endpoint { get; set; }
    }
}
