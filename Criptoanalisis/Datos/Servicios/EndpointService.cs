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

        public Entidades.Endpoint Create(EndpointCreateDto dto) =>
            _endpointRepo.Create(EndpointMapper.FromCreateDto(dto));

        public Entidades.Endpoint Update(EndpointCreateDto dto, int id)
        {
            Entidades.Endpoint edp = _endpointRepo.GetBy(x => x.Id == id).ToList()[0];
            edp.Url = dto.Url;
            edp.Tipo = dto.Tipo;
            return _endpointRepo.Update(edp);
        }

        public Entidades.Endpoint Delete(EndpointDto dto) =>
            _endpointRepo.Delete(EndpointMapper.FromDto(dto));
    }
}
