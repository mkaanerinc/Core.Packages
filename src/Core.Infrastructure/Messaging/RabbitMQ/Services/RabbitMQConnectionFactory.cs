using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using Core.Infrastructure.Messaging.RabbitMQ.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Services;

/// <summary>
/// Provides functionality to create a RabbitMQ connection using the specified settings.
/// Implements <see cref="IRabbitMQConnectionFactory"/>.
/// </summary>
public class RabbitMQConnectionFactory : IRabbitMQConnectionFactory
{
    private readonly ConnectionFactory _connectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMQConnectionFactory"/> class.
    /// </summary>
    /// <param name="settings">The RabbitMQ settings used to configure the connection.</param>
    public RabbitMQConnectionFactory(RabbitMQSettings settings)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.Port,
            UserName = settings.UserName,
            Password = settings.Password,
            AutomaticRecoveryEnabled = true
        };
    }

    /// <summary>
    /// Asynchronously creates a new RabbitMQ connection.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{IConnection}"/> that represents the asynchronous operation. The task result contains the created <see cref="IConnection"/>.
    /// </returns>
    public Task<IConnection> CreateConnectionAsync()
    {
        return _connectionFactory.CreateConnectionAsync();
    }
}