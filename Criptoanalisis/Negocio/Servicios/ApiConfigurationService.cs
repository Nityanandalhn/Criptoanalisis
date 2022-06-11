using Datos.Dtos;
using Datos.Entidades;
using Datos.Interfaces;
using Datos.Mappers;
using Datos.Repositorios;
using System.Linq.Expressions;

namespace Negocio.Servicios
{
    public class ApiConfigurationService : IDisposable
    {
        protected readonly ILogger<ApiConfigurationService> _logger;
        protected readonly IEndpointsRepository _endpointRepo;
        protected readonly IParametroRepository _parametrosRepo;
        protected readonly IIntercambioRepository _intercambiosRepo;
        protected readonly IUsuarioRepository _usuarioRepo;
        protected readonly IMonedaRepository _monedaRepo;
        private bool disposedValue;

        public ApiConfigurationService(ILogger<ApiConfigurationService> logger, 
            EndpointsRepository endpointRepo,
            ParametrosRepository parametrosRepo,
            IntercambioRepository monedasRepo,
            UsuarioRepository usuarioRepo,
            MonedaRepository monedaRepo)
        {
            _logger = logger;
            _endpointRepo = endpointRepo;
            _parametrosRepo = parametrosRepo;
            _intercambiosRepo = monedasRepo;
            _usuarioRepo = usuarioRepo;
            _monedaRepo = monedaRepo;
        }

        public IEnumerable<EndpointDto> GetAllEndpointsWithParameterInfo()
        {
            _logger.LogInformation("Buscando todos los endpoint");
            return _endpointRepo.Get().Select(x => EndpointMapper.FromEntity(x));
        }

        public IEnumerable<EndpointDto> GetAllEndpointsByUserId(int userId)
        {
            _logger.LogInformation($"Buscando todos los endpoint del usuario {userId}");
            return _usuarioRepo.Get().Where(x => x.Id == userId).First()!.EndpointsActivos!.Select(x => EndpointMapper.FromEntity(x));
        }

        internal List<IntercambioDto> GetLastNIntercambios(int limit) => 
            _intercambiosRepo.Get().OrderByDescending(x => x.Fecha).Take(limit).Select(x => IntercambioMapper.FromEntity(x)).ToList();

        public List<ParametroDto> GetAllParametersWithEndpointInfo() =>
            _parametrosRepo.Get().Select(x => ParametroMapper.FromEntity(x)).ToList();

        public List<IntercambioDto> GetAllIntercambiosWithEndpointInfo() =>
            _intercambiosRepo.Get().Select(x => IntercambioMapper.FromEntity(x)).ToList();

        public List<UsuarioDto> GetAllUsuariosWithIntercambioInfo() =>
            _usuarioRepo.Get().Select(x => UsuarioMapper.FromEntity(x)).ToList();

        public List<MonedaDto> GetAllMonedas() =>
            _monedaRepo.Get().Select(x => MonedaMapper.FromEntity(x)).ToList();

        public List<Endpoints> GetEndpointBy(Expression<Func<Endpoints, bool>> predicado) => 
            _endpointRepo.GetBy(predicado).ToList();

        public Endpoints CreateEndpoint(EndpointCreateDto dto) => 
            _endpointRepo.GetBy(x => x.Url == dto.Url).Any() 
                ? throw new ApplicationException() 
                : _endpointRepo.Create(EndpointMapper.FromCreateDto(dto));

        public Endpoints CreateEndpointPorUsuario(EndpointCreateDto dto, int userId)
        {
            if (_endpointRepo.GetBy(x => x.Url == dto.Url).Any() || !_usuarioRepo.GetBy(x => x.Id == userId).Any())
                throw new ApplicationException();

            Endpoints edp = _endpointRepo.Create(EndpointMapper.FromCreateDto(dto));
            Usuario usr = _usuarioRepo.GetBy(x => x.Id == userId).First();
            usr.EndpointsActivos!.Add(edp);
            _usuarioRepo.Update(usr);
            return _endpointRepo.GetBy(x => x.Id == edp.Id).First();
        }

        public Parametro CreateParametro(ParametroCreateDto dto) =>
            _parametrosRepo.Create(ParametroMapper.FromCreateDto(dto));

        public Endpoints UpdateEndpoint(EndpointCreateDto dto, int id) 
            => _endpointRepo.Update(EndpointMapper.UpdateFromCreateDto(dto, BuscarEndpointPorId(id)));

        public Endpoints UpdateEndpoint(Endpoints edp) 
            => _endpointRepo.Update(edp);

        public Endpoints DeleteEndpoint(EndpointDto dto) =>
            _endpointRepo.Delete(EndpointMapper.FromDto(dto));

        public MonedaDto? CreateMoneda(MonedaDto dto) 
            => MonedaMapper.FromEntity(_monedaRepo.Create(MonedaMapper.FromDto(dto)));

        public EndpointDto IncluirParametroEnEndpoint(ParametroDto dto, int id)
        {
            Parametro param;
            try
            {
                param = BuscarParametroPorId(dto.Id);
            } 
            catch
            {
                param = _parametrosRepo.GetBy(x => x.Valor == dto.Valor && x.Mapea == dto.Mapea).Any() 
                    ? _parametrosRepo.GetBy(x => x.Valor == dto.Valor && x.Mapea == dto.Mapea).First() 
                    : CreateParametro(ParametroMapper.CreateDtoFromDto(dto));
            }
            _endpointRepo.IncluirParametro(BuscarEndpointPorId(id), param);
            return EndpointMapper.FromEntity(BuscarEndpointPorId(id));
        }

        public Endpoints BuscarEndpointPorId(int id) 
            => _endpointRepo.GetBy(x => x.Id == id).ToList()[0];

        public Parametro BuscarParametroPorId(int id)
            => _parametrosRepo.GetBy(x => x.Id == id).ToList()[0];

        public List<Endpoints> EndpointsConUsuariosActivos() => 
            _endpointRepo.GetAllEndpointInfo().Where(x => x.UsuariosActivos!.Count > 0).ToList();

        public Intercambio CreateIntercambioDesdeProceso(Intercambio intercambio) => 
            _intercambiosRepo.CrearDesdeProceso(intercambio);

        public void Guardar() => _endpointRepo.GuardarCambios();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _endpointRepo.Dispose();
                    _intercambiosRepo.Dispose();
                    _monedaRepo.Dispose();
                    _parametrosRepo.Dispose();
                    _usuarioRepo.Dispose();
                }

                disposedValue = true;
            }
        }

        //~ApiConfigurationService()
        //{
        //    Dispose(disposing: false);
        //}

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
