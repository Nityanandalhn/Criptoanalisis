using Datos.Dtos;
using Datos.Entidades;
using Datos.Mappers;
using Datos.Relaciones;
using Datos.Repositorios;
using System.Linq.Expressions;

namespace Datos.Servicios
{
    public class ApiConfigurationService
    {
        protected readonly EndpointRepository _endpointRepo;
        protected readonly ParametrosRepository _parametrosRepo;
        public ApiConfigurationService(EndpointRepository endpointRepo, ParametrosRepository parametrosRepo)
        {
            _endpointRepo = endpointRepo;
            _parametrosRepo = parametrosRepo;
        }

        public List<EndpointDto> GetAllEndpointsWithParameterInfo() => 
            _endpointRepo.Get().Select(x => EndpointMapper.FromEntity(x)).ToList();

        public List<ParametroDto> GetAllParametersWithEndpointInfo() =>
            _parametrosRepo.Get().Select(x => ParametrosMapper.FromEntity(x)).ToList();

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
                param = _parametrosRepo.Create(ParametrosMapper.FromDto(dto));
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
