using Datos.Entidades;
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class IntercambioRepository : IIntercambioRepository
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public IntercambioRepository() => DbContext = new();

        public Intercambio Create(Intercambio parametro) => 
            Persist(() => DbContext.Monedas!.Add(parametro));

        public Intercambio Delete(Intercambio parametro) => 
            Persist(() => DbContext.Monedas!.Remove(parametro));

        public IQueryable<Intercambio> Get() => 
            DbContext.Monedas!.Include(e => e.Endpoints)!;

        public Intercambio GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Intercambio> GetBy(Expression<Func<Intercambio, bool>> predicado) => 
            DbContext.Monedas!.Where(predicado).Include(e => e.Endpoints)!;

        public Intercambio Update(Intercambio moneda) => 
            Persist(() => DbContext.Monedas!.Update(moneda));

        protected virtual Intercambio Persist(Func<EntityEntry<Intercambio>> act)
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
