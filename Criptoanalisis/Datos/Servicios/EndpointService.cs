using Datos.Dtos;
using Datos.Mappers;
using Datos.Repositorios;

namespace Datos.Servicios
{
    public class EndpointService
    {
        EndpointRepository endpointRepo;
        public EndpointService(EndpointRepository repo) => endpointRepo = repo;

        public List<EndpointDto> GetAllEndpointsWithParameterInfo() => endpointRepo.Get().Select(x => EndpointMapper.FromEntity(x)).ToList();
    }
}
