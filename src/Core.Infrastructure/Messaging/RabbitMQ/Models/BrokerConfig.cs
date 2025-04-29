using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Models;

/// <summary>
/// Represents the configuration required for defining a RabbitMQ broker setup,
/// including exchange, routing key, and queue names.
/// </summary>
public class BrokerConfig
{
    /// <summary>
    /// Gets or sets the name of the RabbitMQ exchange.
    /// </summary>
    public required string ExchangeName { get; set; }

    /// <summary>
    /// Gets or sets the routing key used for directing messages within the exchange.
    /// </summary>
    public required string RoutingKey { get; set; }

    /// <summary>
    /// Gets or sets the name of the queue to which messages will be routed.
    /// </summary>
    public required string QueueName { get; set; }
}