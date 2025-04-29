using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Models;

/// <summary>
/// Represents the configuration options for the message broker, including a list of broker configurations.
/// </summary>
public class MessageBrokerOptions
{
    /// <summary>
    /// Gets or sets the list of broker configurations.
    /// </summary>
    /// <value>
    /// A list of <see cref="BrokerConfig"/> objects representing the individual broker settings.
    /// </value>
    public List<BrokerConfig> Brokers { get; set; } = new List<BrokerConfig>();
}