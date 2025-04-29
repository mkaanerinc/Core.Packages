using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Interfaces;

/// <summary>
/// Defines a contract for publishing messages to a RabbitMQ exchange.
/// </summary>
public interface IRabbitMQProducer
{
    /// <summary>
    /// Publishes a message to the specified RabbitMQ exchange using the provided routing key.
    /// </summary>
    /// <typeparam name="T">The type of the message being published.</typeparam>
    /// <param name="message">The message payload to be sent.</param>
    /// <param name="exchangeName">The name of the exchange to publish the message to.</param>
    /// <param name="routingKey">The routing key used to route the message to the appropriate queue.</param>
    /// <returns>A task that represents the asynchronous publish operation.</returns>
    Task PublishMessage<T>(T message, string exchangeName, string routingKey);
}