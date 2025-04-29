using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Interfaces;

public interface IRabbitMQProducer
{
    Task PublishMessage<T>(T message, string exchangeName, string routingKey);
}