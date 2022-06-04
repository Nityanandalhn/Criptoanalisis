using Datos.Base;
using Datos.Entidades;
using Datos.Relaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class ParametrosRepository : IRepoBase<Parametros, CriptoAnalisisContext>
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public ParametrosRepository() => DbContext = new();

        public Parametros Create(Parametros parametro) => 
            Persist(() => DbContext.Parametros!.Add(parametro));

        public Parametros Delete(Parametros parametro) => 
            Persist(() => DbContext.Parametros!.Remove(parametro));

        public IQueryable<Parametros> Get() => 
            DbContext.Parametros!.Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Endpoints);

        public Parametros GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Parametros> GetBy(Expression<Func<Parametros, bool>> predicado) => 
            DbContext.Parametros!.Where(predicado).Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Parametros);

        public Parametros Update(Parametros endpoint) => 
            Persist(() => DbContext.Parametros!.Update(endpoint));

        protected virtual Parametros Persist(Func<EntityEntry<Parametros>> act)
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
        public void GuardarCambios() => DbContext.SaveChanges();
        public Parametros IncluirParametro(Endpoints edp, Parametros parametro)
        {
            edp.ParametrosEndpoints!.Add(new ParametrosEndpoint()
            {
                EndpointId = edp.Id,
                ParametroId = parametro.Id,
                Endpoints = edp,
                Parametros = parametro
            });
            DbContext.SaveChanges();
            return parametro;
        }
    }
}
