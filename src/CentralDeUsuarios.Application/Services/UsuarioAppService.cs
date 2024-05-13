using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Domain.Interfaces.Services;

namespace CentralDeUsuarios.Application.Services;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioDomainService _usuarioDomainService;

    public UsuarioAppService(IUsuarioDomainService usuarioDomainService)
    {
        _usuarioDomainService = usuarioDomainService;
    }

    public void CriarUsuario(CriarUsuarioCommand command)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _usuarioDomainService.Dispose();
    }
}
