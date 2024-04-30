using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Helpers;

namespace CentralDeUsuarios.Infra.Data.Repositories;

/// <summary>
/// Classe de implementação do repositório de usuários.
/// </summary>
public class UsuarioRepository : IUsuarioRepository
{
    private readonly SqlServerContext _context;

    public UsuarioRepository(SqlServerContext context)
    {
        _context = context;
    }

    public void Create(Usuario entity)
    {
        entity.Senha = MD5Helper.Encrypt(entity.Senha);
        _context.Usuario.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Usuario entity)
    {
        entity.Senha = MD5Helper.Encrypt(entity.Senha);
        _context.Usuario.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Usuario entity)
    {
        _context.Usuario.Remove(entity);
        _context.SaveChanges();
    }

    public List<Usuario> GetAll()
    {
        return _context.Usuario.ToList();
    }

    public Usuario GetByEmail(string email)
    {
        return _context.Usuario.FirstOrDefault(e => e.Email.Equals(email));
    }

    public Usuario GetById(Guid id)
    {
        return _context.Usuario.Find(id);
    }
}
