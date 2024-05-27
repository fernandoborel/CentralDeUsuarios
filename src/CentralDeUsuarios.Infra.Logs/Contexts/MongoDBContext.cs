using CentralDeUsuarios.Infra.Logs.Models;
using CentralDeUsuarios.Infra.Logs.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Authentication;

namespace CentralDeUsuarios.Infra.Logs.Contexts;

public class MongoDBContext
{
    private readonly MongoDBSettings? _settings;
    private readonly IMongoDatabase? _database;

    public MongoDBContext(IOptions<MongoDBSettings> settings)
    {
        _settings = settings.Value;

        #region Conectando no Banco

        var client = MongoClientSettings.FromUrl(new MongoUrl(_settings.Host));
        if (_settings.IsSSL)
        {
            client.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
        }

        _database = new MongoClient(client).GetDatabase(_settings.Name);

        #endregion
    }

    /// <summary>
    /// Collection dos Logs de Usuários
    /// </summary>
    public IMongoCollection<LogUsuarioModel> LogUsuarios
        => _database.GetCollection<LogUsuarioModel>("LogUsuarios");
}
