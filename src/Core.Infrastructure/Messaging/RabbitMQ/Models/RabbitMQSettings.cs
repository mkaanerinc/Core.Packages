using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Models;

/// <summary>
/// Represents the settings required to connect to a RabbitMQ server.
/// </summary>
public class RabbitMQSettings
{
    /// <summary>
    /// Gets or sets the connection string for URI-based RabbitMQ connections (e.g., CloudAMQP).
    /// </summary>
    /// <value>The AMQP or AMQPS connection string, defaults to "amqps://myuser:mypass@lemur.cloudamqp.com/myvhost".</value>
    public string ConnectionString { get; set; } = "amqps://myuser:mypass@lemur.cloudamqp.com/myvhost";

    /// <summary>
    /// Gets or sets the hostname of the RabbitMQ server.
    /// </summary>
    /// <value>
    /// A string representing the hostname (default is "localhost").
    /// </value>
    public string HostName { get; set; } = "localhost";

    /// <summary>
    /// Gets or sets the port number used to connect to RabbitMQ.
    /// </summary>
    /// <value>
    /// An integer representing the port number (default is 5672).
    /// </value>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// Gets or sets the username for authenticating with RabbitMQ.
    /// </summary>
    /// <value>
    /// A string representing the username (default is "guest").
    /// </value>
    public string UserName { get; set; } = "guest";

    /// <summary>
    /// Gets or sets the password for authenticating with RabbitMQ.
    /// </summary>
    /// <value>
    /// A string representing the password (default is "guest").
    /// </value>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// Gets or sets the type of connection to use for RabbitMQ configuration.
    /// </summary>
    /// <value>The connection type, either <see cref="ConnectionType.Uri"/> or <see cref="ConnectionType.IndividualSettings"/>.</value>
    public ConnectionType ConnectionType { get; set; }
}