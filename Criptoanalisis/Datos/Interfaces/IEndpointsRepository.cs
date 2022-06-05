using Datos.Abstract.Interfaces;
using Datos.Entidades;

namespace Datos.Interfaces
{
    public interface IEndpointsRepository : IRepoBase<Endpoints>
    {
        public void IncluirParametro(Endpoints edp, Parametros parametro);
    }
}
