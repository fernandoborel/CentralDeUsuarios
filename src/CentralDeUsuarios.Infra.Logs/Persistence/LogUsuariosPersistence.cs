using CentralDeUsuarios.Infra.Logs.Contexts;
using CentralDeUsuarios.Infra.Logs.Interfaces;
using CentralDeUsuarios.Infra.Logs.Models;
using MongoDB.Driver;

namespace CentralDeUsuarios.Infra.Logs.Persistence;

public class LogUsuariosPersistence : ILogUsuariosPersistence
{
    private readonly MongoDBContext _context;

    public LogUsuariosPersistence(MongoDBContext context)
    {
        _context = context;
    }

    public void Create(LogUsuarioModel model)
    {
        _context.LogUsuarios.InsertOne(model);
    }

    public void Update(LogUsuarioModel model)
    {
        var filter = Builders<LogUsuarioModel>.Filter.Eq(x => x.Id, model.Id);
        _context.LogUsuarios.ReplaceOne(filter, model);
    }

    public void Delete(LogUsuarioModel model)
    {
        var filter = Builders<LogUsuarioModel>.Filter.Eq(x => x.Id, model.Id);
        _context.LogUsuarios.DeleteOne(filter);
    }

    public List<LogUsuarioModel> GetAll(DateTime dataMin, DateTime dataMax)
    {
        var filter = Builders<LogUsuarioModel>.Filter
            .Where(log => log.DataHora >= dataMin && log.DataHora <= dataMax);

        return _context.LogUsuarios
            .Find(filter)
            .SortByDescending(log => log.DataHora)
            .ToList();
    }

    public List<LogUsuarioModel> GetAll(Guid usuarioId)
    {
        var filter = Builders<LogUsuarioModel>.Filter
            .Eq(log => log.UsuarioId, usuarioId);

        return _context.LogUsuarios
            .Find(filter)
            .SortByDescending(log => log.DataHora)
            .ToList();
    }
}
