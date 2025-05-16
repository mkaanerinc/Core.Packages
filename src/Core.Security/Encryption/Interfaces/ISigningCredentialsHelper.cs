using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Encryption.Interfaces;

/// <summary>
/// Defines a method to create signing credentials for use in token signing operations.
/// </summary>
public interface ISigningCredentialsHelper
{
    /// <summary>
    /// Creates signing credentials using the specified security key and signature algorithm.
    /// </summary>
    /// <param name="securityKey">The security key to use for signing.</param>
    /// <returns>
    /// A <see cref="SigningCredentials"/> instance configured with the specified security key
    /// and a secure signing algorithm.
    /// </returns>
    SigningCredentials CreateSigningCredentials(SecurityKey securityKey);
}
