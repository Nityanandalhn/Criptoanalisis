using Datos.Dtos;
using Datos.Entidades;
using Datos.Interfaces;
using Datos.Mappers;
using Datos.Repositorios;
using System.Linq.Expressions;

namespace Negocio.Servicios
{
    public class ApiConfigurationService
    {
        protected readonly ILogger<ApiConfigurationService> _logger;
        protected readonly IEndpointsRepository _endpointRepo;
        protected readonly IParametrosRepository _parametrosRepo;
        protected readonly IMonedaRepository _monedasRepo;

        public ApiConfigurationService(ILogger<ApiConfigurationService> logger, EndpointsRepository endpointRepo, ParametrosRepository parametrosRepo, MonedaRepository monedasRepo)
        {
            _logger = logger;
            _endpointRepo = endpointRepo;
            _parametrosRepo = parametrosRepo;
            _monedasRepo = monedasRepo;
        }

        public IEnumerable<EndpointDto> GetAllEndpointsWithParameterInfo()
        {
            _logger.LogInformation("Buscando todos los endpoint");
            return _endpointRepo.Get().Select(x => EndpointMapper.FromEntity(x));
        }

        public List<ParametroDto> GetAllParametersWithEndpointInfo() =>
            _parametrosRepo.Get().Select(x => ParametrosMapper.FromEntity(x)).ToList();

        public List<Moneda> GetAllMonedasWithEndpointInfo() =>
            _monedasRepo.Get().ToList();

        public List<Endpoints> GetEndpointBy(Expression<Func<Endpoints, bool>> predicado) => 
            _endpointRepo.GetBy(predicado).ToList();

        public Endpoints CreateEndpoint(EndpointCreateDto dto) =>
            _endpointRepo.Create(EndpointMapper.FromCreateDto(dto));

        public Parametros CreateParametro(ParametroCreateDto dto) =>
            _parametrosRepo.Create(ParametrosMapper.FromCreateDto(dto));

        public Endpoints UpdateEndpoint(EndpointCreateDto dto, int id)
        {
            Endpoints edp = ObtenerEndpointPorId(id);
            edp.Url = dto.Url ?? edp.Url;
            edp.Tipo = dto.Tipo ?? edp.Tipo;
            return _endpointRepo.Update(edp);
        }

        public Endpoints DeleteEndpoint(EndpointDto dto) =>
            _endpointRepo.Delete(EndpointMapper.FromDto(dto));

        public EndpointDto IncluirParametroEnEndpoint(ParametroDto dto, int id)
        {
            Parametros param;
            try
            {
                param = ObtenerParametroPorId(dto.Id);
            } 
            catch
            {
                param = CreateParametro(ParametrosMapper.CreateDtoFromDto(dto));
            }
            _endpointRepo.IncluirParametro(ObtenerEndpointPorId(id), param);
            return EndpointMapper.FromEntity(ObtenerEndpointPorId(id));
        }

        private Endpoints ObtenerEndpointPorId(int id) 
            => _endpointRepo.GetBy(x => x.Id == id).ToList()[0];

        private Parametros ObtenerParametroPorId(int id)
            => _parametrosRepo.GetBy(x => x.Id == id).ToList()[0];
    }
}
