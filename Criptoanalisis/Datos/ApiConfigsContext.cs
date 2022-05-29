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
    public class ApiConfigsContext : DbContext
    {
        static ApiConfigsContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<Metodos>();
        public DbSet<Apis>? Apis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseNpgsql(Criptoanalisis.Properties.Resources.ConnectionString)
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder builder) 
            => builder.HasDefaultSchema("public")
                    .HasPostgresEnum<Metodos>()
                    .Entity<Apis>().HasKey(apis => new { apis.Endpoint, apis.Metodo }).HasName("apis_pkey");
    }

    [Table("api_configs")]
    public class Apis
    {
        [Required]
        [Column("endpoint")]
        public string? Endpoint { get; set; }
        [Required]
        [Column("metodo")]
        public Metodos Metodo { get; set; }
        [Column("propiedades")]
        public List<string>? Propiedades { get; set; }
    }

    public enum Metodos
    {
        [PgName("GET")]
        GET,
        [PgName("POST")]
        POST, 
        [PgName("PUT")]
        PUT, 
        [PgName("DELETE")]
        DELETE
    }
}