using CentralDeUsuarios.Domain.Core;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Services;

namespace CentralDeUsuarios.Domain.Services;

/// <summary>
/// Implementação dos serviços de domínio de usuários
/// </summary>
public class UsuarioDomainService : IUsuarioDomainService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioDomainService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public void CriarUsuario(Usuario usuario)
    {
        DomainException.When(
                _usuarioRepository.GetByEmail(usuario.Email) != null,
                $"O email {usuario.Email} já está cadastrado, tente outro."
            );

        _usuarioRepository.Create(usuario);
    }

    public void Dispose()
    {
        _usuarioRepository.Dispose();
    }
}
