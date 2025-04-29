using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Models;

public class BrokerConfig
{
    public required string ExchangeName { get; set; }
    public required string RoutingKey { get; set; }
    public required string QueueName { get; set; }
}