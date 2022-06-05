using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Datos.Abstract.Interfaces
{
    //WIP - Generificando repositorio
    public interface IRepoBase<Entidad> : IDisposable where Entidad : class
    {
        /// <summary>
        /// Método que devuelve todas las entidades.
        /// </summary>
        /// <returns>Set con las entidades.</returns>
        IQueryable<Entidad> Get();
        /// <summary>
        /// Método que devuelve la entidad en la posición indicada dentro del listado.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>La entidad encontrada en la posición indicada.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Entidad GetAtPos(int pos);
        /// <summary>
        /// Método que devuelve un listado de entidades que cumplen los criterios de la expresión indicada.
        /// </summary>
        /// <param name="predicado"></param>
        /// <returns>Set con las entidades.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        IQueryable<Entidad> GetBy(Expression<Func<Entidad, bool>> predicado);
        /// <summary>
        /// Método que persiste una nueva entidad.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>La nueva entidad almacenada.</returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        Entidad Create(Entidad t);
        /// <summary>
        /// Método que modifica y persiste una entidad.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>La entidad modificada.</returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        Entidad Update(Entidad t);
        /// <summary>
        /// Método que elimina y persiste una entidad.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>La entidad eliminada.</returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        Entidad Delete(Entidad t);

        /// <summary>
        /// Método que persiste todos los cambios realizados sobre el contexto.
        /// </summary>
        void GuardarCambios();
    }
}
