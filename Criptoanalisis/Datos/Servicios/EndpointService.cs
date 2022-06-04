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

        public List<Entidades.Endpoints> GetBy(Expression<Func<Entidades.Endpoints, bool>> predicado) => 
            _endpointRepo.GetBy(predicado).ToList();

        public Entidades.Endpoints Create(EndpointCreateDto dto) =>
            _endpointRepo.Create(EndpointMapper.FromCreateDto(dto));

        public Entidades.Endpoints Update(EndpointCreateDto dto, int id)
        {
            Entidades.Endpoints edp = _endpointRepo.GetBy(x => x.Id == id).ToList()[0];
            edp.Url = dto.Url ?? edp.Url;
            edp.Tipo = dto.Tipo ?? edp.Tipo;
            return _endpointRepo.Update(edp);
        }

        public Entidades.Endpoints Delete(EndpointDto dto) =>
            _endpointRepo.Delete(EndpointMapper.FromDto(dto));
    }
}
