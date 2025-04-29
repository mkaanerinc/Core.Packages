using Core.CrossCuttingConcerns.Logging;
using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using Core.Infrastructure.Messaging.RabbitMQ.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Services;

public class RabbitMQClientService : IDisposable, IAsyncDisposable
{
    private readonly IRabbitMQConnectionFactory _connectionFactory;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly MessageBrokerOptions _brokerOptions;
    private readonly ILoggerService _loggerServiceBase;
    private bool _disposed;

    public MessageBrokerOptions BrokerOptions => _brokerOptions;

    public RabbitMQClientService(IConfiguration configuration, ILoggerService loggerServiceBase, IRabbitMQConnectionFactory connectionFactory)
    {
        _loggerServiceBase = loggerServiceBase;
        _connectionFactory = connectionFactory;
        _brokerOptions = configuration.GetSection("RabbitMQ:MessageBrokerOptions").Get<MessageBrokerOptions>()
            ?? throw new InvalidOperationException("RabbitMQ configuration is missing.");
    }

    public async Task<IChannel> ConnectAsync()
    {
        try
        {
            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            await EnsureConnectionAsync();

            _channel = await _connection!.CreateChannelAsync();

            foreach (var broker in _brokerOptions.Brokers)
            {
                await _channel.ExchangeDeclareAsync(
                    exchange: broker.ExchangeName,
                    type: "direct",
                    durable: true,
                    autoDelete: false
                );
                await _channel.QueueDeclareAsync(
                    queue: broker.QueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );
                await _channel.QueueBindAsync(
                    queue: broker.QueueName,
                    exchange: broker.ExchangeName,
                    routingKey: broker.RoutingKey
                );
            }

            _loggerServiceBase.LogInformation("RabbitMQ connection established.");
            return _channel;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError("Failed to establish RabbitMQ connection.", ex);
            throw;
        }
    }

    private async Task EnsureConnectionAsync()
    {
        if (_connection is { IsOpen: true }) return;

        try
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            _connection.ConnectionShutdownAsync += async (sender, args) =>
            {
                _loggerServiceBase.LogWarning("RabbitMQ connection shutdown. Attempting to reconnect...");
                await ConnectAsync();
            };
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError("Failed to create RabbitMQ connection.", ex);
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        DisposeAsync().GetAwaiter().GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        try
        {
            if (_channel != null)
            {
                await _channel.CloseAsync();
                _channel.Dispose();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync();
                _connection.Dispose();
            }

            _loggerServiceBase.LogInformation("RabbitMQ connection disposed.");
            _disposed = true;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError("Error while disposing RabbitMQ connection.", ex);
            throw;
        }
    }
}