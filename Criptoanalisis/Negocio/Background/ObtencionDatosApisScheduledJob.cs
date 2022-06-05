using Negocio.Servicios;

namespace Negocio.Background
{
    public class ObtencionDatosApisScheduledJob : BackgroundService
    {
        private readonly ILogger<ObtencionDatosApisScheduledJob> _logger;
        private readonly ApiConfigurationService _service;

        private static string FechaCompleta { get => $"{DateTimeOffset.Now:O}"; }
        public ObtencionDatosApisScheduledJob(ILogger<ObtencionDatosApisScheduledJob> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _service = factory.CreateScope().ServiceProvider.GetRequiredService<ApiConfigurationService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{} Iniciando proceso de obtención de datos de cripto.", FechaCompleta);

            stoppingToken.Register(() =>
                _logger.LogCritical("{} Se ha detenido el proceso de obtención de datos de cripto.", FechaCompleta));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("{} Leyendo configuración desde la DB.", FechaCompleta);

                _service.EndpointsConUsuariosActivos().ForEach(x =>
                {
                    _logger.LogInformation("{} Realizando consulta sobre {}", FechaCompleta, x.Url);


                });

                await Task.Delay(10000, stoppingToken);
            }

            _logger.LogInformation("{} Proceso de obtención de datos de cripto terminado.", FechaCompleta);
        }
    }
}
