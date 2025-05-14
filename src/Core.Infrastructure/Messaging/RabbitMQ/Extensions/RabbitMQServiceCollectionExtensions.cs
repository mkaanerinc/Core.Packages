using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using Core.Infrastructure.Messaging.RabbitMQ.Models;
using Core.Infrastructure.Messaging.RabbitMQ.Services;
using Microsoft.Extensions.Configuration;
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
    /// Registers RabbitMQ services and dependencies to the <see cref="IServiceCollection"/> using the provided configuration.
    /// </summary>
    /// <param name="services">The service collection to which the RabbitMQ services will be added.</param>
    /// <param name="configuration">The configuration object containing RabbitMQ settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if RabbitMQ settings configuration is missing.</exception>
    /// <remarks>
    /// This method registers the <see cref="IRabbitMQConnectionFactory"/>, <see cref="RabbitMQClientService"/>,
    /// <see cref="IRabbitMQProducer"/>, and <see cref="IRabbitMQConsumer"/> as singletons, along with <see cref="RabbitMQSettings"/> from the configuration.
    /// </remarks>
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSettings = configuration.GetSection("RabbitMQ:RabbitMQSettings").Get<RabbitMQSettings>()
            ?? throw new InvalidOperationException("RabbitMQ settings configuration is missing.");
        services.AddSingleton(rabbitMQSettings);

        services.AddSingleton<IRabbitMQConnectionFactory, RabbitMQConnectionFactory>();
        services.AddSingleton<RabbitMQClientService>();
        return services;
    }
}