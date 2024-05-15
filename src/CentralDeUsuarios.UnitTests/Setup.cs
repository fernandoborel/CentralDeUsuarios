using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Domain.Services;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CentralDeUsuarios.UnitTests;

/// <summary>
/// Classe para configuração de injeção de dependência do xUnit.
/// </summary>
public class Setup : Xunit.Di.Setup
{
    protected override void Configure()
    {
        ConfigureAppConfiguration((hostingContext, config) =>
        {
            #region Ativar a Injeção de dependência no XUnit
            
            bool reloadOnChange = hostingContext.Configuration
           .GetValue("hostBuilder:reloadConfigOnChange", true);
            if (hostingContext.HostingEnvironment.IsDevelopment())
                config.AddUserSecrets<Setup>(true, reloadOnChange);
            
            #endregion
        });

        ConfigureServices((context, services) =>
        {
            #region Localizar o arquivo appsettings.json
            
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(),"appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            #endregion

            #region Capturar a connectionstring do arquivo appsettings.json
 
            var root = configurationBuilder.Build();
            var connectionString = root.GetSection("ConnectionString").GetSection("CentralDeUsuarios").Value;

            #endregion

            #region Fazendo as injeção de dependência do projeto de teste
 
            services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connectionString));
            
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();

            #endregion
        });
    }
}
