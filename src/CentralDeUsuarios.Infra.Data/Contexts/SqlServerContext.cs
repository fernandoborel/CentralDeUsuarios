using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CentralDeUsuarios.Infra.Data.Contexts;

/// <summary>
/// Classe de contexto do banco de dados
/// </summary>
public class SqlServerContext : DbContext
{

    public SqlServerContext(DbContextOptions<SqlServerContext> dbContextOptions) : base(dbContextOptions)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());
    }

    public DbSet<Usuario> Usuario { get; set; }
}
