using Core.Security.Encryption.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Encryption;

/// <summary>
/// Provides helper methods for creating security keys used in token generation and validation.
/// </summary>
public class SecurityKeyHelper : ISecurityKeyHelper
{
    /// <summary>
    /// Creates a symmetric security key from the specified security key string.
    /// </summary>
    /// <param name="securityKey">The security key string to convert into a <see cref="SecurityKey"/>.</param>
    /// <returns>A <see cref="SecurityKey"/> instance representing the symmetric security key.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the security key is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when the security key is shorter than 32 bytes (256 bits).</exception>
    public SecurityKey CreateSecurityKey(string securityKey)
    {
        if (string.IsNullOrEmpty(securityKey))
            throw new ArgumentNullException(nameof(securityKey), "Security key cannot be null or empty.");

        byte[] keyBytes = Encoding.UTF8.GetBytes(securityKey);
        if (keyBytes.Length < 32)
            throw new ArgumentException("Security key must be at least 32 bytes (256 bits) long.", nameof(securityKey));

        return new SymmetricSecurityKey(keyBytes);
    }
}
