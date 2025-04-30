using Ambev.DeveloperEvaluation.Domain.Messaging;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace Ambev.DeveloperEvaluation.ORM.Messaging;
public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly IConnection _connection;

    public RabbitMqEventPublisher()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost", // ajuste conforme sua infra
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
    }

    public Task PublishAsync<T>(T @event, string routingKey) where T : class
    {
        using var channel = _connection.CreateModel();

        channel.ExchangeDeclare(exchange: "sales_exchange", type: ExchangeType.Topic);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "sales_exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body
        );

        return Task.CompletedTask;
    }
}
