﻿using Datos.Base;
using Datos.Entidades;
using Datos.Relaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Datos.Repositorios
{
    public class EndpointRepository : IRepoBase<Endpoints, CriptoAnalisisContext>
    {
        protected CriptoAnalisisContext DbContext { get; set; }
        public EndpointRepository() => DbContext = new();

        public Endpoints Create(Endpoints endpoint) => 
            Persist(() => DbContext.Endpoints!.Add(endpoint));

        public Endpoints Delete(Endpoints endpoint) => 
            Persist(() => DbContext.Endpoints!.Remove(endpoint));

        public IQueryable<Endpoints> Get() => 
            DbContext.Endpoints!.Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Parametros);

        public Endpoints GetAtPos(int pos)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Endpoints> GetBy(Expression<Func<Endpoints, bool>> predicado) => 
            DbContext.Endpoints!.Where(predicado).Include(e => e.ParametrosEndpoints)!.ThenInclude(pe => pe.Parametros);

        public Endpoints Update(Endpoints endpoint) => 
            Persist(() => DbContext.Endpoints!.Update(endpoint));

        protected virtual Endpoints Persist(Func<EntityEntry<Endpoints>> act)
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
        public Endpoints IncluirParametro(Endpoints edp, Parametros parametro)
        {
            edp.ParametrosEndpoints!.Add(new ParametrosEndpoint()
            {
                EndpointId = edp.Id,
                ParametroId = parametro.Id,
                Endpoints = edp,
                Parametros = parametro
            });
            DbContext.SaveChanges();
            return edp;
        }
    }
}
