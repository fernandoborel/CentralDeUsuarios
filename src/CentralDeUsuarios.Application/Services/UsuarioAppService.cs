using AutoMapper;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Producers;
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
        var usuario = _mapper.Map<Usuario>(command);

        var validate = usuario.Validate;
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

        _usuarioDomainService.CriarUsuario(usuario);

        //conteúdo
        var _messageQueueModel = new MessageQueueModel
        {
            Conteudo = JsonConvert.SerializeObject(usuario)
        };

        // Enviar mensagem para a fila
        _messageQueueProducer.Create(_messageQueueModel);
    }

    public void Dispose()
    {
        _usuarioDomainService.Dispose();
    }
}
