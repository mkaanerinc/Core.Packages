using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Encryption.Interfaces;

/// <summary>
/// Defines a method to create security keys used in token generation and validation.
/// </summary>
public interface ISecurityKeyHelper
{
    /// <summary>
    /// Creates a symmetric security key from the specified security key string.
    /// </summary>
    /// <param name="securityKey">The security key string to convert into a <see cref="SecurityKey"/>.</param>
    /// <returns>A <see cref="SecurityKey"/> instance representing the symmetric security key.</returns>
    SecurityKey CreateSecurityKey(string securityKey);
}
