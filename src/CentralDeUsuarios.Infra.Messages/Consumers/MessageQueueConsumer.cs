using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Settings;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CentralDeUsuarios.Infra.Messages.Consumers;

/// <summary>
/// Classe para consumir e processar msgs da fila do RabbitMQ
/// </summary>
public class MessageQueueConsumer : BackgroundService
{
    private readonly MessageSettings? _messageSettings;
    private readonly IServiceProvider? _serviceProvider;
    private readonly MailHelper? _mailHelper;
    private readonly IConnection? _connection;
    private readonly IModel? _model;

    public MessageQueueConsumer(IOptions<MessageSettings>? messageSettings, IServiceProvider? serviceProvider, MailHelper? mailHelper)
    {
        _messageSettings = messageSettings.Value;
        _serviceProvider = serviceProvider;
        _mailHelper = mailHelper;

        #region Conectando no servidor de Mensageria

        var connectionFactory = new ConnectionFactory
        {
            HostName = _messageSettings.Host,
            UserName = _messageSettings.Username,
            Password = _messageSettings.Password,
        };

        _connection = connectionFactory.CreateConnection();
        _model = _connection.CreateModel();
        _model.QueueDeclare(
            queue: _messageSettings.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        #endregion
    }

    /// <summary>
    /// Método para ler a fila do RabbitMQ
    /// </summary>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //componente para realizar a leitura
        var consumer = new EventingBasicConsumer(_model);

        //fazendo a leitura da fila
        consumer.Received += (sender, args) =>
        {
            //ler o conteúdo da msg
            var contentArray = args.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);

            //deserialize
            var messageQueueModel = JsonConvert.DeserializeObject<MessageQueueModel>(contentString);

            //verificar o tipo da msg
            switch (messageQueueModel.Tipo)
            {
                case TipoMensagem.CONFIRMACAO_DE_CADASTRO:
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var usuariosMessageVO = JsonConvert.DeserializeObject<UsuariosMessageVO>(messageQueueModel.Conteudo);

                        //enviar email
                        EnviarMensagemDeConfirmacaoDeCadastro(usuariosMessageVO);

                        //comunicar ao RabbitMQ que a msg foi processada
                        _model.BasicAck(args.DeliveryTag, false);
                    }

                    break;

                case TipoMensagem.RECUPERACAO_DE_SENHA:
                    //a fazer
                    break;
            }
        };

        //executando o consumidor
        _model.BasicConsume(_messageSettings.Queue, false, consumer);

        return Task.CompletedTask;
    }

    private void EnviarMensagemDeConfirmacaoDeCadastro(UsuariosMessageVO usuariosMessageVO)
    {
        var mailTo = usuariosMessageVO.Email;
        var subject = $"Confirmação de cadastro de usuário. ID: {usuariosMessageVO.Id}";
        var body = $@"
            Olá {usuariosMessageVO.Nome},
            <br>
            <br>
            <strong>Parabéns, sua conta de usuário foi criada com sucesso!</strong>
            <br>
            <br>
            ID:<strong> {usuariosMessageVO.Id}</strong><br>
            Nome:<strong> {usuariosMessageVO.Nome}</strong><br>
            <br>
            Atenciosamente,<br>
            Equipe Central de Usuários
        ";

        _mailHelper.Send(mailTo, subject, body);
    }
}
