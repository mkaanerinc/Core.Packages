using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mailing;

/// <summary>
/// Provides extension methods for registering mailing-related services into the dependency injection container.
/// </summary>
public static class MailingServiceRegistration
{
    /// <summary>
    /// Registers the mailing services such as <see cref="IMailService"/> and its configuration
    /// to the specified <see cref="IServiceCollection"/> for dependency injection.
    /// </summary>
    /// <param name="services">The service collection to which the mailing services will be added.</param>
    /// <param name="configuration">The application configuration object used to retrieve mail settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the mailing services registered.</returns>
    public static IServiceCollection AddMailingServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddScoped<IMailService, MailService>();

        return services;
    }
}
