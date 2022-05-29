using Datos.Base;
using Microsoft.EntityFrameworkCore;

namespace Datos.Implementaciones
{
    public class EndpointRepository : RepoBaseImpl<Entidades.Endpoint, EndpointParametrosContext>
    {
        public override IQueryable<Entidades.Endpoint> Get()
        {
            return base.Get().Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Parametros);
        }
    }
}
