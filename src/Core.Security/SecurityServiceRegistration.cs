using Core.Security.Encryption.Interfaces;
using Core.Security.Encryption;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security;

/// <summary>
/// Provides extension methods to register security-related services into the dependency injection container.
/// </summary>
public static class SecurityServiceRegistration
{
    /// <summary>
    /// Registers the core security services such as <see cref="ISecurityKeyHelper"/> and <see cref="ISigningCredentialsHelper"/>
    /// to the specified <see cref="IServiceCollection"/> for dependency injection.
    /// </summary>
    /// <param name="services">The service collection to add the security services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the security services registered.</returns>
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ISecurityKeyHelper, SecurityKeyHelper>();
        services.AddScoped<ISigningCredentialsHelper, SigningCredentialsHelper>();

        return services;
    }
}
