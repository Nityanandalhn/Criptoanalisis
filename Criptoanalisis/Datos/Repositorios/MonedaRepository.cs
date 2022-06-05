using Datos.Base;
using Datos.Entidades;
using Datos.Relaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class MonedaRepository : IRepoBase<Moneda, CriptoAnalisisContext>
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public MonedaRepository() => DbContext = new();

        public Moneda Create(Moneda parametro) => 
            Persist(() => DbContext.Monedas!.Add(parametro));

        public Moneda Delete(Moneda parametro) => 
            Persist(() => DbContext.Monedas!.Remove(parametro));

        public IQueryable<Moneda> Get() => 
            DbContext.Monedas!.Include(e => e.Endpoints)!;

        public Moneda GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Moneda> GetBy(Expression<Func<Moneda, bool>> predicado) => 
            DbContext.Monedas!.Where(predicado).Include(e => e.Endpoints)!;

        public Moneda Update(Moneda moneda) => 
            Persist(() => DbContext.Monedas!.Update(moneda));

        protected virtual Moneda Persist(Func<EntityEntry<Moneda>> act)
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
