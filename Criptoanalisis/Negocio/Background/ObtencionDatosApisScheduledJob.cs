using Negocio.Servicios;
using Datos;
using Datos.Entidades;

namespace Negocio.Background
{
    public class ObtencionDatosApisScheduledJob : BackgroundService
    {
        private readonly ILogger<ObtencionDatosApisScheduledJob> _logger;
        private readonly IServiceScopeFactory _factory;

        private static string FechaCompleta { get => $"{DateTimeOffset.Now:O}"; }
        public ObtencionDatosApisScheduledJob(ILogger<ObtencionDatosApisScheduledJob> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{} Iniciando proceso de obtención de datos de cripto.", FechaCompleta);

            stoppingToken.Register(() =>
                _logger.LogCritical("{} Se ha detenido el proceso de obtención de datos de cripto.", FechaCompleta));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("{} Leyendo configuración desde la DB.", FechaCompleta);
                {
                    ApiConfigurationService? _service = _factory.CreateScope().ServiceProvider.GetRequiredService<ApiConfigurationService>();
                    _service.EndpointsConUsuariosActivos().ForEach(endpoint =>
                    {
                        Dictionary<string, string> parametrosBusqueda = new();
                        endpoint.ParametrosEndpoints!.ToList().ForEach(parm =>
                        {
                            _logger.LogInformation("{} Incluyendo parametro de filtrado {} asignado a la propiedad {}", FechaCompleta, parm!.Parametros!.Valor!, parm!.Parametros!.Mapea!);
                            parametrosBusqueda.Add(parm!.Parametros!.Mapea!, parm!.Parametros!.Valor!);
                        });
                        _logger.LogInformation("{} Realizando consulta sobre {}", FechaCompleta, endpoint.Url);

                        List<Intercambio> respuestas = JsonUtils.ConsultarApi<Intercambio>(endpoint.Url!, parametrosBusqueda, _service.GetAllMonedas().Select(m => m.Nombre).ToList()!);

                        respuestas.ForEach(r =>
                        {
                            _logger.LogInformation("Endpoint id {} Intercambio id {}", endpoint.Id, r.Id);
                            r.Fecha = DateTimeOffset.UtcNow;
                            //endpoint.Intercambios!.Add(r);
                            //r.Endpoint = new() { Id = endpoint.Id };
                            //_service.Guardar();
                            _service.CreateIntercambioDesdeProceso(r);
                        });
                        _service.UpdateEndpoint(endpoint);
                    });
                    
                }
                await Task.Delay(10000, stoppingToken);
            }

            _logger.LogInformation("{} Proceso de obtención de datos de cripto terminado.", FechaCompleta);
        }
    }
}
