using Datos.Entidades;
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public UsuarioRepository() => DbContext = new();

        public Usuario Create(Usuario usuario) => 
            Persist(() => DbContext.Usuarios!.Add(usuario));

        public Usuario Delete(Usuario usuario) => 
            Persist(() => DbContext.Usuarios!.Remove(usuario));

        public IQueryable<Usuario> Get() => 
            DbContext.Usuarios!.Include(e => e.Intercambios)!;

        public Usuario GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Usuario> GetBy(Expression<Func<Usuario, bool>> predicado) => 
            DbContext.Usuarios!.Where(predicado).Include(e => e.Intercambios)!;

        public Usuario Update(Usuario usuario) => 
            Persist(() => DbContext.Usuarios!.Update(usuario));

        protected virtual Usuario Persist(Func<EntityEntry<Usuario>> act)
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
