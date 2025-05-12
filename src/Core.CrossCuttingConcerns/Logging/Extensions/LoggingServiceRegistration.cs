using Core.CrossCuttingConcerns.Logging.Models;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Extensions;

/// <summary>
/// Provides extension methods for registering logging services based on the specified logger type.
/// </summary>
public static class LoggingServiceRegistration
{
    /// <summary>
    /// Registers the appropriate <see cref="ILoggerService"/> implementation in the dependency injection container 
    /// based on the specified <see cref="LoggerType"/>.
    /// </summary>
    /// <param name="services">The service collection to which the logger service will be added.</param>
    /// <param name="loggerType">The type of logger to register. Defaults to <see cref="LoggerType.FileLogger"/>.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when an unsupported logger type is provided.</exception>
    public static IServiceCollection AddLoggingServices(this IServiceCollection services, LoggerType loggerType = LoggerType.FileLogger)
    {
        switch (loggerType)
        {
            case LoggerType.FileLogger:
                services.AddSingleton<ILoggerService, FileLogger>();
                break;
            case LoggerType.MsSqlLogger:
                services.AddSingleton<ILoggerService, MsSqlLogger>();
                break;
            default:
                throw new ArgumentException($"Unsupported logger type: {loggerType}", nameof(loggerType));
        }

        return services;
    }
}