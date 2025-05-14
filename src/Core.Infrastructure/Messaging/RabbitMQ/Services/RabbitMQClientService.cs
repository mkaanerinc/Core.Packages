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

/// <summary>
/// Represents a service that manages a RabbitMQ client connection and channel.
/// Implements <see cref="IDisposable"/> and <see cref="IAsyncDisposable"/> for proper resource management.
/// </summary>
public class RabbitMQClientService : IDisposable, IAsyncDisposable
{
    private readonly IRabbitMQConnectionFactory _connectionFactory;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly MessageBrokerOptions _brokerOptions;
    private readonly ILoggerService _loggerServiceBase;
    private bool _disposed;

    /// <summary>
    /// Gets the broker options used by the RabbitMQ client service.
    /// </summary>
    /// <value>
    /// A <see cref="MessageBrokerOptions"/> object that contains the broker settings.
    /// </value>
    public MessageBrokerOptions BrokerOptions => _brokerOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMQClientService"/> class.
    /// </summary>
    /// <param name="configuration">The application's configuration object.</param>
    /// <param name="loggerServiceBase">The logger service for logging RabbitMQ events.</param>
    /// <param name="connectionFactory">The factory used to create RabbitMQ connections.</param>
    public RabbitMQClientService(IConfiguration configuration, ILoggerService loggerServiceBase, IRabbitMQConnectionFactory connectionFactory)
    {
        _loggerServiceBase = loggerServiceBase;
        _connectionFactory = connectionFactory;
        _brokerOptions = configuration.GetSection("RabbitMQ:MessageBrokerOptions").Get<MessageBrokerOptions>()
            ?? new MessageBrokerOptions();
    }

    /// <summary>
    /// Asynchronously connects to RabbitMQ and prepares the channels and queues for use.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{IChannel}"/> that represents the asynchronous operation. The task result contains the created <see cref="IChannel"/>.
    /// </returns>
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

            if (!_brokerOptions.Brokers.Any())
            {
                _loggerServiceBase.LogWarning("No broker configurations found in MessageBrokerOptions. Skipping exchange and queue declarations.");
            }

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

    /// <summary>
    /// Ensures that a valid connection to RabbitMQ is established. If no valid connection exists, it will attempt to create one.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
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

    /// <summary>
    /// Disposes the resources used by the <see cref="RabbitMQClientService"/> synchronously.
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;
        DisposeAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Asynchronously disposes the resources used by the <see cref="RabbitMQClientService"/>.
    /// </summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
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