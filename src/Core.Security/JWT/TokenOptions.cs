using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT;

/// <summary>
/// Represents the configuration options required for token generation and validation.
/// </summary>
public class TokenOptions
{
    /// <summary>
    /// The audience that the token is intended for.
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// The issuer of the token.
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// The duration in minutes for which the access token is valid.
    /// </summary>
    public int AccessTokenExpiration { get; set; }

    /// <summary>
    /// The secret key used to sign the JWT token.
    /// </summary>
    public required string SecurityKey { get; set; }

    /// <summary>
    /// The time-to-live (in days) of the refresh token.
    /// </summary>
    public int RefreshTokenTTL { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenOptions"/> class.
    /// </summary>
    public TokenOptions()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenOptions"/> class with values.
    /// </summary>
    /// <param name="audience">The intended audience.</param>
    /// <param name="issuer">The token issuer.</param>
    /// <param name="accessTokenExpiration">The access token expiration in minutes.</param>
    /// <param name="securityKey">The secret key for signing the token.</param>
    /// <param name="refreshTokenTtl">The refresh token time-to-live in days.</param>
    public TokenOptions(string audience, string issuer, int accessTokenExpiration,
        string securityKey, int refreshTokenTtl)
    {
        Audience = audience;
        Issuer = issuer;
        AccessTokenExpiration = accessTokenExpiration;
        SecurityKey = securityKey;
        RefreshTokenTTL = refreshTokenTtl;
    }
}
