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
            AutomaticRecoveryEnabled = true
        };

        ConfigureConnectionFactory(settings);
    }

    /// <summary>
    /// Configures the RabbitMQ <see cref="ConnectionFactory"/> based on the provided settings.
    /// </summary>
    /// <param name="settings">The RabbitMQ settings containing connection configuration.</param>
    /// <exception cref="ArgumentException">Thrown when the connection type is unsupported or required 
    /// settings are invalid.</exception>
    private void ConfigureConnectionFactory(RabbitMQSettings settings)
    {
        switch (settings.ConnectionType)
        {
            case ConnectionType.Uri:

                _connectionFactory.Uri = new Uri(settings.ConnectionString);
                _connectionFactory.Ssl.Enabled = true;
                break;

            case ConnectionType.IndividualSettings:

                _connectionFactory.HostName = settings.HostName;
                _connectionFactory.Port = settings.Port;
                _connectionFactory.UserName = settings.UserName;
                _connectionFactory.Password = settings.Password;
                break;

            default:
                throw new ArgumentException($"Unsupported ConnectionType: {settings.ConnectionType}.");
        }
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