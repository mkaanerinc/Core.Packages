using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Interfaces;

/// <summary>
/// Defines a contract for creating connections to a RabbitMQ message broker.
/// </summary>
public interface IRabbitMQConnectionFactory
{
    /// <summary>
    /// Asynchronously creates and returns a new RabbitMQ connection.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an established <see cref="IConnection"/> instance.
    /// </returns>
    Task<IConnection> CreateConnectionAsync();
}