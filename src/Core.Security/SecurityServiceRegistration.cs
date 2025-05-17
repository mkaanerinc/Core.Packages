using Core.Security.Encryption.Interfaces;
using Core.Security.Encryption;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Authenticators.Email.Interfaces;
using Core.Security.Authenticators.Email;
using Core.Security.Authenticators.OTP.Interfaces;
using Core.Security.Authenticators.OTP.OtpNet;
using Core.Security.JWT;

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
        services.AddScoped<ITokenHelper, JwtHelper>();

        services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
        services.AddScoped<IOtpAuthenticatorHelper, OtpNetOtpAuthenticatorHelper>();

        return services;
    }
}
