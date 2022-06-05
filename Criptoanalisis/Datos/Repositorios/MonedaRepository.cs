using Datos.Entidades;
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class MonedaRepository : IMonedaRepository
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public MonedaRepository() => DbContext = new();

        public Moneda Create(Moneda usuario) => 
            Persist(() => DbContext.Monedas!.Add(usuario));

        public Moneda Delete(Moneda usuario) => 
            Persist(() => DbContext.Monedas!.Remove(usuario));

        public IQueryable<Moneda> Get() => 
            DbContext.Monedas!.Include(e => e.Usuarios);

        public Moneda GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Moneda> GetBy(Expression<Func<Moneda, bool>> predicado) => 
            DbContext.Monedas!.Where(predicado);

        public Moneda Update(Moneda usuario) => 
            Persist(() => DbContext.Monedas!.Update(usuario));

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
