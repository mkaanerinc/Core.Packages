using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT;

/// <summary>
/// Represents a JSON Web Token (JWT) access token.
/// </summary>
public class AccessToken
{
    /// <summary>
    /// The encoded JWT token string.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// The expiration date and time of the access token.
    /// </summary>
    public DateTimeOffset Expiration { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessToken"/> class.
    /// </summary>
    public AccessToken()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessToken"/> class with a token and expiration.
    /// </summary>
    /// <param name="token">The JWT token string.</param>
    /// <param name="expiration">The expiration time of the token.</param>
    public AccessToken(string token, DateTimeOffset expiration)
    {
        Token = token;
        Expiration = expiration;
    }
}
