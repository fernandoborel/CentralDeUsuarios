using AutoMapper;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Producers;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
using FluentValidation;
using Newtonsoft.Json;

namespace CentralDeUsuarios.Application.Services;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioDomainService _usuarioDomainService;
    private readonly MessageQueueProducer _messageQueueProducer;
    private readonly IMapper _mapper;


    public UsuarioAppService(IUsuarioDomainService usuarioDomainService, MessageQueueProducer messageQueueProducer, IMapper mapper)
    {
        _usuarioDomainService = usuarioDomainService;
        _messageQueueProducer = messageQueueProducer;
        _mapper = mapper;
    }

    public void CriarUsuario(CriarUsuarioCommand command)
    {
        #region Criando e validando usuário

        var usuario = _mapper.Map<Usuario>(command);

        var validate = usuario.Validate;
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

        _usuarioDomainService.CriarUsuario(usuario);

        #endregion

        #region Mensageria

        var _messageQueueModel = new MessageQueueModel
        {
            Tipo = TipoMensagem.CONFIRMACAO_DE_CADASTRO,
            Conteudo = JsonConvert.SerializeObject(new UsuariosMessageVO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            })
        };

        _messageQueueProducer.Create(_messageQueueModel);

        #endregion
    }

    public void Dispose()
    {
        _usuarioDomainService.Dispose();
    }
}
