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

        public Intercambio Create(Intercambio intercambio) => 
            Persist(() => DbContext.Intercambios!.Add(intercambio));

        public Intercambio Delete(Intercambio intercambio) => 
            Persist(() => DbContext.Intercambios!.Remove(intercambio));

        public IQueryable<Intercambio> Get() => 
            DbContext.Intercambios!.Include(e => e.Endpoint)!;

        public Intercambio GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Intercambio> GetBy(Expression<Func<Intercambio, bool>> predicado) => 
            DbContext.Intercambios!.Where(predicado).Include(e => e.Endpoint)!;

        public Intercambio Update(Intercambio intercambio) => 
            Persist(() => DbContext.Intercambios!.Update(intercambio));

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

        public Intercambio CrearDesdeProceso(Intercambio intercambio)
        {
            return Persist(() => DbContext.Intercambios!.Attach(intercambio));
        }
    }
}
