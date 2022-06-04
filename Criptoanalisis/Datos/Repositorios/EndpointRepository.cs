using Datos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class EndpointRepository : IRepoBase<Entidades.Endpoint, CriptoAnalisisContext>
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public EndpointRepository() => DbContext = new();

        public Entidades.Endpoint Create(Entidades.Endpoint endpoint) => 
            Persist(() => DbContext.Endpoints!.Add(endpoint));

        public Entidades.Endpoint Delete(Entidades.Endpoint endpoint) => 
            Persist(() => DbContext.Endpoints!.Remove(endpoint));

        public IQueryable<Entidades.Endpoint> Get() => 
            DbContext.Endpoints!.Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Parametros);

        public Entidades.Endpoint GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Entidades.Endpoint> GetBy(Expression<Func<Entidades.Endpoint, bool>> predicado) => 
            DbContext.Endpoints!.Where(predicado).Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Parametros);

        public Entidades.Endpoint Update(Entidades.Endpoint endpoint) => 
            Persist(() => DbContext.Endpoints!.Update(endpoint));

        protected virtual Entidades.Endpoint Persist(Func<EntityEntry<Entidades.Endpoint>> act)
        {
            var res = act.Invoke();
            DbContext.SaveChanges();
            return res.Entity;
        }

        public void Dispose()
        {
            DbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
