using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Models;

public class MessageBrokerOptions
{
    public List<BrokerConfig> Brokers { get; set; } = new List<BrokerConfig>();
}