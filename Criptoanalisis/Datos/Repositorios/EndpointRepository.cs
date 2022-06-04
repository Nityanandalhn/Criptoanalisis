﻿using Datos.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class EndpointRepository : IRepoBase<Entidades.Endpoint, CriptoAnalisisContext>
    {
        protected CriptoAnalisisContext RepoContext { get; set; }
        public EndpointRepository() => RepoContext = new();
        public Entidades.Endpoint Create(Entidades.Endpoint t)
        {
            throw new NotImplementedException();
        }

        public Entidades.Endpoint Delete(Entidades.Endpoint t)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            RepoContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<Entidades.Endpoint> Get()
        {
            return RepoContext.Endpoints.Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Parametros);
        }

        public Entidades.Endpoint GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Entidades.Endpoint> GetBy(Expression<Func<Entidades.Endpoint, bool>> predicado)
        {
            throw new NotImplementedException();
        }

        public Entidades.Endpoint Update(Entidades.Endpoint t)
        {
            throw new NotImplementedException();
        }
    }
}
