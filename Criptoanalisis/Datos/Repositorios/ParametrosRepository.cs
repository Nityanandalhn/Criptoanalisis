using Datos.Entidades;
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class ParametrosRepository : IParametroRepository
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public ParametrosRepository() => DbContext = new();

        public Parametro Create(Parametro parametro) => 
            Persist(() => DbContext.Parametros!.Add(parametro));

        public Parametro Delete(Parametro parametro) => 
            Persist(() => DbContext.Parametros!.Remove(parametro));

        public IQueryable<Parametro> Get() => 
            DbContext.Parametros!.Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Endpoints);

        public Parametro GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Parametro> GetBy(Expression<Func<Parametro, bool>> predicado) => 
            DbContext.Parametros!.Where(predicado).Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Endpoints);

        public Parametro Update(Parametro parametro) => 
            Persist(() => DbContext.Parametros!.Update(parametro));

        protected virtual Parametro Persist(Func<EntityEntry<Parametro>> act)
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
    }
}
