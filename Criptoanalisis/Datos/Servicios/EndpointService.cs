using Datos.Repositorios;

namespace Datos.Servicios
{
    public class EndpointService
    {
        EndpointRepository endpointRepo;
        public EndpointService(EndpointRepository repo)
        {
            endpointRepo = repo;
        }


    }
}
