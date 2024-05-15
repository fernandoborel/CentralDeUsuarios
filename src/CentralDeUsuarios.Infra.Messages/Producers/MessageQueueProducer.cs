using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CentralDeUsuarios.Infra.Messages.Producers;

/// <summary>
/// Classe para a escrita de msgs na fila do RabbitMQ
/// </summary>
public class MessageQueueProducer
{
    private readonly MessageSettings _messageSettings;
    private readonly ConnectionFactory _connectionFactory;

    public MessageQueueProducer(IOptions<MessageSettings> messageSettings)
    {
        this._messageSettings = messageSettings.Value;

        _connectionFactory = new ConnectionFactory
        {
            HostName = _messageSettings.Host,
            UserName = _messageSettings.Username,
            Password = _messageSettings.Password,
        };
    }

    /// <summary>
    /// Escreve uma mensagem na fila
    /// </summary>
    public void Create(MessageQueueModel model)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            //obj na fila do RabbitMQ
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _messageSettings.Queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: _messageSettings.Queue,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))
                    );
            }
        }
    }
}
