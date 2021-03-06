using Datos.Abstract.Interfaces;
using Datos.Entidades;

namespace Datos.Interfaces
{
    public interface IIntercambioRepository : IRepoBase<Intercambio>
    {
        public Intercambio CrearDesdeProceso(Intercambio intercambio);
    }
}
