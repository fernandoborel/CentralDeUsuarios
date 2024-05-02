using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Helpers;

namespace CentralDeUsuarios.Infra.Data.Repositories;

/// <summary>
/// Classe de implementação do repositório de usuários.
/// </summary>
public class UsuarioRepository : BaseRepository<Usuario, Guid>, IUsuarioRepository
{
    private readonly SqlServerContext _context;

    public UsuarioRepository(SqlServerContext context) : base(context)
    {
        _context = context;
    }

    public override void Create(Usuario entity)
    {
        entity.Senha = MD5Helper.Encrypt(entity.Senha);
        base.Create(entity);
    }

    public override void Update(Usuario entity)
    {
        entity.Senha = MD5Helper.Encrypt(entity.Senha);
        base.Update(entity);
    }

    public Usuario GetByEmail(string email)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email.Equals(email));
    }
}
