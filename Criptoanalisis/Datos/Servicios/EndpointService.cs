using Datos.Dtos;
using Datos.Mappers;
using Datos.Repositorios;
using System.Linq.Expressions;

namespace Datos.Servicios
{
    public class EndpointService
    {
        protected readonly EndpointRepository _endpointRepo;
        public EndpointService(EndpointRepository repo) => 
            _endpointRepo = repo;

        public List<EndpointDto> GetAllEndpointsWithParameterInfo() => 
            _endpointRepo.Get().Select(x => EndpointMapper.FromEntity(x)).ToList();

        public List<Entidades.Endpoint> GetBy(Expression<Func<Entidades.Endpoint, bool>> predicado) => 
            _endpointRepo.GetBy(predicado).ToList();

        public Entidades.Endpoint Update(EndpointDto dto) => 
            _endpointRepo.Update(EndpointMapper.FromDto(dto));

        public Entidades.Endpoint Delete(EndpointDto dto) =>
            _endpointRepo.Delete(EndpointMapper.FromDto(dto));
    }
}
