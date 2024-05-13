using CentralDeUsuarios.Domain.Entities;

namespace CentralDeUsuarios.Domain.Interfaces.Services;

/// <summary>
/// Interface de serviço de domínio de usuário
/// </summary>
public interface IUsuarioDomainService : IDisposable
{
    void CriarUsuario(Usuario usuario);
}
