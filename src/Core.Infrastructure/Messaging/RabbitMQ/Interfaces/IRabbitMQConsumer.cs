using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Interfaces;

public interface IRabbitMQConsumer
{
    Task Subscribe<T>(string queueName, Func<T, Task> onMessageReceived);
    void Dispose();
}