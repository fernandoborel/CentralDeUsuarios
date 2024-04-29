using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CentralDeUsuarios.Infra.Data.Contexts;

/// <summary>
/// Classe para injeção de dependência do contexto do banco de dados
/// </summary>
public class SqlServerContextMigration : IDesignTimeDbContextFactory<SqlServerContext>
{
    /// <summary>
    /// Injetor de dependência do DbContext sempre que executar o Migrations
    /// </summary>
    public SqlServerContext CreateDbContext(string[] args)
    {
        #region appsettings.json

        var configuratioBuilder = new ConfigurationBuilder();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        configuratioBuilder.AddJsonFile(path, false);

        #endregion

        #region ConnectionString

        var root = configuratioBuilder.Build();
        var connectionString = root.GetSection("ConnectionString").GetSection("CentralDeUsuarios").Value;

        #endregion

        #region SqlServerContext

        var builder = new DbContextOptionsBuilder<SqlServerContext>();
        builder.UseSqlServer(connectionString);

        return new SqlServerContext(builder.Options);

        #endregion
    }
}
