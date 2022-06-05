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
        public DbSet<Parametro>? Parametros { get; set; }
        public DbSet<Intercambio>? Intercambios { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Moneda>? Monedas { get; set; }
        public DbSet<ParametroEndpoint>? ParametrosEndpoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder//.UseLazyLoadingProxies()
                    .UseNpgsql(Criptoanalisis.Properties.Resources.ConnectionStringProyecto)
                    .LogTo(x => Console.WriteLine(x), LogLevel.Information)
                    .EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("public");
            builder.Entity<Endpoints>().HasMany(p => p.ParametrosEndpoints).WithOne(e => e.Endpoints).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Parametro>().HasMany(p => p.ParametrosEndpoints).WithOne(e => e.Parametros).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Intercambio>().HasOne(m => m.Endpoint).WithMany(e => e.Intercambios).HasForeignKey(x => x.Id);
            builder.Entity<Usuario>().HasMany(m => m.Intercambios).WithMany(e => e.Usuarios).UsingEntity<Dictionary<string, object>>("intercambio_usuario", 
                j => j.HasOne<Intercambio>().WithMany().HasForeignKey("cod_itc").HasConstraintName("fk_intercambio").OnDelete(DeleteBehavior.SetNull),
                j => j.HasOne<Usuario>().WithMany().HasForeignKey("cod_usr").HasConstraintName("fk_usuario").OnDelete(DeleteBehavior.SetNull)
            );
            builder.Entity<Usuario>().HasMany(m => m.EndpointsActivos).WithMany(e => e.UsuariosActivos).UsingEntity<Dictionary<string, object>>("usuario_endpoint",
                j => j.HasOne<Endpoints>().WithMany().HasForeignKey("cod_edp").HasConstraintName("fk_endpoint").OnDelete(DeleteBehavior.SetNull),
                j => j.HasOne<Usuario>().WithMany().HasForeignKey("cod_usr").HasConstraintName("fk_usuario").OnDelete(DeleteBehavior.SetNull)
            );
            builder.Entity<ParametroEndpoint>().HasKey(pe => new { pe.ParametroId, pe.EndpointId });
            builder.Entity<ParametroEndpoint>().HasOne(pe => pe.Endpoints).WithMany(e => e.ParametrosEndpoints).HasForeignKey(pe => pe.EndpointId);
            builder.Entity<ParametroEndpoint>().HasOne(pe => pe.Parametros).WithMany(p => p.ParametrosEndpoints).HasForeignKey(pe => pe.ParametroId);
        }
        //.Entity<Entidades.Endpoint>();
        //.HasPostgresEnum<Metodos>()
        //.Entity<Endpoint>().HasKey(apis => apis.Id).HasName("endpoint_pkey");
    }
}