using CentralDeUsuarios.Domain.Core;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Services;

namespace CentralDeUsuarios.Domain.Services;

/// <summary>
/// Implementação do serviço de domínio de usuário
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
        //não permite criar usuário com E-MAIL já existente
        DomainException.When(
                _usuarioRepository.GetByEmail(usuario.Email) != null,
                $"Já existe um usuário com o E-MAIL {usuario.Email}, tente outro."
                );

        _usuarioRepository.Create(usuario);
    }
}
