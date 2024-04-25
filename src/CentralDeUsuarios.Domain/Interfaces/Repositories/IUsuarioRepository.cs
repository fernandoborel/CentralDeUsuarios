using CentralDeUsuarios.Domain.Core;
using CentralDeUsuarios.Domain.Entities;

namespace CentralDeUsuarios.Domain.Interfaces.Repositories;

/// <summary>
/// Interface de repositório para usuário
/// </summary>
public interface IUsuarioRepository : IBaseRepository<Usuario, Guid>
{
    Usuario GetByEmail(string email);
}
