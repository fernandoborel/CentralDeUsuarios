using CentralDeUsuarios.Application.Commands;

namespace CentralDeUsuarios.Application.Interfaces;

/// <summary>
/// Interface para abstração dos métodos da camada de aplicação para usuário
/// </summary>
public interface IUsuarioAppService : IDisposable
{
    void CriarUsuario(CriarUsuarioCommand command);
}
