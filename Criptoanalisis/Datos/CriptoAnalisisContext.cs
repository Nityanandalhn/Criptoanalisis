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
using Datos.Entidades;
using Datos.Relaciones;

namespace Datos
{
    public class CriptoAnalisisContext : DbContext
    {
        //static EndpointContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<Metodos>();
        public DbSet<Endpoints>? Endpoints { get; set; }
        public DbSet<Parametros>? Parametros { get; set; }
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
            builder.Entity<Endpoints>().HasMany(p => p.ParametrosEndpoints).WithOne(e => e.Endpoints).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Parametros>().HasMany(p => p.ParametrosEndpoints).WithOne(e => e.Parametros).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<ParametrosEndpoint>().HasKey(pe => new { pe.ParametroId, pe.EndpointId });
            builder.Entity<ParametrosEndpoint>().HasOne(pe => pe.Endpoints).WithMany(e => e.ParametrosEndpoints).HasForeignKey(pe => pe.EndpointId);
            builder.Entity<ParametrosEndpoint>().HasOne(pe => pe.Parametros).WithMany(p => p.ParametrosEndpoints).HasForeignKey(pe => pe.ParametroId);
        }
        //.Entity<Entidades.Endpoint>();
        //.HasPostgresEnum<Metodos>()
        //.Entity<Endpoint>().HasKey(apis => apis.Id).HasName("endpoint_pkey");
    }
}