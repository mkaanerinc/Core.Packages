using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Models;

/// <summary>
/// Specifies the type of connection configuration for RabbitMQ.
/// </summary>
public enum ConnectionType
{
    /// <summary>
    /// Represents a URI-based connection configuration (e.g., for CloudAMQP).
    /// </summary>
    Uri,

    /// <summary>
    /// Represents a connection configuration using individual settings (e.g., hostname, port, username, password).
    /// </summary>
    IndividualSettings
}