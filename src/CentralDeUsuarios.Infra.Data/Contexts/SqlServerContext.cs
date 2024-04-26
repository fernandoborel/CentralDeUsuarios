using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CentralDeUsuarios.Infra.Data.Contexts;

/// <summary>
/// Classe de contexto do banco de dados
/// </summary>
public class SqlServerContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=FIO32001599;Initial Catalog=BD_CentralDeUsuarios;Integrated Security=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());
    }

    public DbSet<Usuario> Usuario { get; set; }
}
