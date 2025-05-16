using Core.Security.Encryption.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Encryption;

/// <summary>
/// Provides helper methods for creating signing credentials used in token signing operations.
/// </summary>
public class SigningCredentialsHelper : ISigningCredentialsHelper
{
    /// <summary>
    /// Creates signing credentials using the specified security key with HMAC SHA512 signature algorithm.
    /// </summary>
    /// <param name="securityKey">The security key to use for signing.</param>
    /// <returns>A <see cref="SigningCredentials"/> instance configured with the specified security key and HMAC SHA512 algorithm.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the security key is null.</exception>
    public SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        if (securityKey == null)
            throw new ArgumentNullException(nameof(securityKey), "Security key cannot be null.");

        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
    }
}
