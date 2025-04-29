using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using Core.Infrastructure.Messaging.RabbitMQ.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Extensions;

/// <summary>
/// Provides extension methods for registering RabbitMQ-related services in the dependency injection container.
/// </summary>
public static class RabbitMQServiceCollectionExtensions
{
    /// <summary>
    /// Registers RabbitMQ services and dependencies to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the RabbitMQ services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    /// <remarks>
    /// This method registers the <see cref="IRabbitMQConnectionFactory"/> and <see cref="RabbitMQClientService"/> as singletons.
    /// </remarks>
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        services.AddSingleton<IRabbitMQConnectionFactory, RabbitMQConnectionFactory>();
        services.AddSingleton<RabbitMQClientService>();
        return services;
    }
}