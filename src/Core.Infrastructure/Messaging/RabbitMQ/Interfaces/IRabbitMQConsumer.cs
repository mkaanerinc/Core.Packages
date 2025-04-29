using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Interfaces;

/// <summary>
/// Defines a contract for subscribing to messages from a RabbitMQ queue.
/// </summary>
public interface IRabbitMQConsumer
{
    /// <summary>
    /// Subscribes to the specified RabbitMQ queue and handles incoming messages using the provided callback.
    /// </summary>
    /// <typeparam name="T">The expected type of the message to be consumed.</typeparam>
    /// <param name="queueName">The name of the queue to subscribe to.</param>
    /// <param name="onMessageReceived">The asynchronous callback to handle received messages.</param>
    /// <returns>A task that represents the asynchronous subscription operation.</returns>
    Task Subscribe<T>(string queueName, Func<T, Task> onMessageReceived);

    /// <summary>
    /// Releases all resources used by the consumer.
    /// </summary>
    void Dispose();
}