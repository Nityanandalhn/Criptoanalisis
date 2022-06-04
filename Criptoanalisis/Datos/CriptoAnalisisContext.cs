using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;
using NpgsqlTypes;

namespace Datos
{
    public class CriptoAnalisisContext : DbContext
    {
        //static EndpointContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<Metodos>();
        public DbSet<Entidades.Endpoint>? Endpoints { get; set; }
        public DbSet<Entidades.Parametros>? Parametros { get; set; }
        public DbSet<Relaciones.ParametrosEndpoint>? ParametrosEndpoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder//.UseLazyLoadingProxies()
                    .UseNpgsql(Criptoanalisis.Properties.Resources.ConnectionStringProyecto)
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("public");
            //builder.Entity<Entidades.Parametros>().HasMany(p => p.Endpoints).WithMany(e => e.Parametros);
            builder.Entity<Relaciones.ParametrosEndpoint>().HasKey(pe => new { pe.ParametroId, pe.EndpointId });
            builder.Entity<Relaciones.ParametrosEndpoint>().HasOne(pe => pe.Endpoint).WithMany(e => e.ParametrosEndpoints).HasForeignKey(pe => pe.EndpointId);
            builder.Entity<Relaciones.ParametrosEndpoint>().HasOne(pe => pe.Parametros).WithMany(p => p.ParametrosEndpoints).HasForeignKey(pe => pe.ParametroId);
        }
        //.Entity<Entidades.Endpoint>();
        //.HasPostgresEnum<Metodos>()
        //.Entity<Endpoint>().HasKey(apis => apis.Id).HasName("endpoint_pkey");
    }
}