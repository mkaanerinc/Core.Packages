using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Interfaces;

public interface IRabbitMQConnectionFactory
{
    Task<IConnection> CreateConnectionAsync();
}