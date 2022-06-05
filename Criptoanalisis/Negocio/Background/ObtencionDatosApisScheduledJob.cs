using Negocio.Servicios;

namespace Negocio.Background
{
    public class ObtencionDatosApisScheduledJob : BackgroundService
    {
        private readonly ILogger<ObtencionDatosApisScheduledJob> _logger;
        private readonly ApiConfigurationService _service;
        public ObtencionDatosApisScheduledJob(ILogger<ObtencionDatosApisScheduledJob> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _service = factory.CreateScope().ServiceProvider.GetRequiredService<ApiConfigurationService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Iniciando tarea de obtención de datos.");

            stoppingToken.Register(() =>
                _logger.LogCritical($"Se ha detenido el proceso de obtención de datos de apis."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{DateTimeOffset.Now} Leyendo configuración desde la DB.");

                _service.GetAllEndpointsWithParameterInfo().ToList().ForEach(x => _logger.LogInformation(x.Url));

                await Task.Delay(60000, stoppingToken);
            }

            _logger.LogInformation($"Proceso de obtención de datos completado.");
        }
    }
}
