using Negocio.Servicios;

namespace Negocio.Background
{
    public class ObtencionDatosApis : BackgroundService
    {
        private readonly ILogger<ObtencionDatosApis> _logger;
        private readonly ApiConfigurationService _service;
        public ObtencionDatosApis(ILogger<ObtencionDatosApis> logger, IServiceScopeFactory factory)
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
                _logger.LogInformation($"Consultado APIs");
                _logger.LogDebug("DEBUG");

                _service.GetAllEndpointsWithParameterInfo().ToList().ForEach(x => _logger.LogInformation(x.Url));

                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation($"Proceso de obtención de datos completado.");
        }
    }
}
