using Datos.Abstract.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Abstract.Implementaciones
{
    //WIP - Generificando repositorio
    /*public class RepoBaseImpl<Entidad, Contexto> : IRepoBase<Entidad, Contexto> where Entidad : class where Contexto : DbContext, new()
    {
        /// <summary>
        /// Contexto de persistencia.
        /// </summary>
        protected Contexto RepoContext { get; set; }
        /// <summary>
        /// Constructor que inicializa el contexto de persistencia.
        /// </summary>
        public RepoBaseImpl() => RepoContext = new();
        /// <summary>
        /// Método que devuelve todas las entidades.
        /// </summary>
        /// <returns>Set con las entidades.</returns>
        public virtual IQueryable<Entidad> Get() => RepoContext.Set<Entidad>();
        /// <summary>
        /// Método que devuelve un listado de entidades que cumplen los criterios de la expresión indicada.
        /// </summary>
        /// <param name="predicado"></param>
        /// <returns>Set con las entidades.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual IQueryable<Entidad> GetBy(Expression<Func<Entidad, bool>> predicado) => RepoContext.Set<Entidad>().Where(predicado);
        /// <summary>
        /// Método que devuelve la entidad en la posición indicada dentro del listado.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>La entidad encontrada en la posición indicada.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public virtual Entidad GetAtPos(int pos) => RepoContext.Set<Entidad>().ToList()[pos];
        /// <summary>
        /// Método que persiste una nueva entidad.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>La nueva entidad almacenada.</returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public virtual Entidad Create(Entidad entity) => Persist(() => RepoContext.Set<Entidad>().Add(entity));
        /// <summary>
        /// Método que modifica y persiste una entidad.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>La entidad modificada.</returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public virtual Entidad Update(Entidad entity) => Persist(() => RepoContext.Set<Entidad>().Update(entity));
        /// <summary>
        /// Método que elimina y persiste una entidad.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>La entidad eliminada.</returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public virtual Entidad Delete(Entidad entity) => Persist(() => RepoContext.Set<Entidad>().Remove(entity));
        /// <summary>
        /// Método que ejecuta una acción y persiste los cambios.
        /// </summary>
        /// <param name="act"></param>
        /// <returns>La entidad persistida sobre la que sea realizado la acción</returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        protected virtual Entidad Persist(Func<EntityEntry<Entidad>> act)
        {
            var r = act.Invoke();
            RepoContext.SaveChanges();
            return r.Entity;
        }
        /// <summary>
        /// Llamada al colector de basura :>
        /// </summary>
        public virtual void Dispose()
        {
            RepoContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }*/
}
