using Core.Security.Encryption.Interfaces;
using Core.Security.Extensions;
using Core.Security.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT;

/// <summary>
/// Provides functionality for creating JSON Web Tokens (JWT) and refresh tokens.
/// </summary>
public class JwtHelper : ITokenHelper
{
    /// <summary>
    /// Gets the application configuration.
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Provides functionality to create a security key.
    /// </summary>
    private readonly ISecurityKeyHelper _securityKeyHelper;

    /// <summary>
    /// Provides functionality to create signing credentials.
    /// </summary>
    private readonly ISigningCredentialsHelper _signingCredentialsHelper;

    /// <summary>
    /// Gets the token-related configuration options.
    /// </summary>
    private readonly TokenOptions _tokenOptions;

    /// <summary>
    /// Holds the calculated expiration time for the current access token.
    /// </summary>
    private DateTimeOffset _accessTokenExpiration;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtHelper"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="securityKeyHelper">Helper to generate the security key.</param>
    /// <param name="signingCredentialsHelper">Helper to generate signing credentials.</param>
    public JwtHelper(IConfiguration configuration, ISecurityKeyHelper securityKeyHelper, ISigningCredentialsHelper signingCredentialsHelper)
    {
        _configuration = configuration;
        _securityKeyHelper = securityKeyHelper;
        _signingCredentialsHelper = signingCredentialsHelper;

        const string configurationSection = "TokenOptions";
        _tokenOptions = _configuration.GetSection(configurationSection).Get<TokenOptions>()
            ?? throw new InvalidOperationException("TokenOptions configuration section is missing or invalid."); ;
    }

    /// <summary>
    /// Creates a new refresh token for the specified user and IP address.
    /// </summary>
    /// <param name="user">The user to whom the refresh token belongs.</param>
    /// <param name="ipAddress">The IP address from which the token is created.</param>
    /// <returns>A new <see cref="RefreshToken"/> instance.</returns>
    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = new()
        {
            UserId = user.Id,
            Token = RandomRefreshToken(),
            Expires = DateTimeOffset.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL),
            CreatedByIp = ipAddress
        };

        return refreshToken;
    }

    /// <summary>
    /// Creates a new access token (JWT) for the specified user and their claims.
    /// </summary>
    /// <param name="user">The user for whom the token is created.</param>
    /// <param name="operationClaims">The claims associated with the user.</param>
    /// <returns>An <see cref="AccessToken"/> containing the token and expiration.</returns>
    public AccessToken CreateToken(User user, IList<OperationClaim> operationClaims)
    {
        _accessTokenExpiration = DateTimeOffset.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = _securityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = _signingCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }

    /// <summary>
    /// Builds the <see cref="JwtSecurityToken"/> object based on the provided parameters.
    /// </summary>
    /// <param name="tokenOptions">The token configuration options.</param>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <param name="signingCredentials">The signing credentials used for signing the token.</param>
    /// <param name="operationClaims">The user's operation claims.</param>
    /// <returns>A configured <see cref="JwtSecurityToken"/> object.</returns>
    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
        SigningCredentials signingCredentials, IList<OperationClaim> operationClaims)
    {
        JwtSecurityToken jwt = new
            (
               issuer: tokenOptions.Issuer,
               audience: tokenOptions.Audience,
               expires: _accessTokenExpiration.UtcDateTime,
               notBefore: DateTimeOffset.UtcNow.UtcDateTime,
               claims: SetClaims(user, operationClaims),
               signingCredentials: signingCredentials
            );
        return jwt;
    }

    /// <summary>
    /// Generates a cryptographically secure random refresh token string.
    /// </summary>
    /// <returns>A base64-encoded random token.</returns>
    private string RandomRefreshToken()
    {
        byte[] numberByte = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }

    /// <summary>
    /// Sets the claims for a user based on their identity and operation claims.
    /// </summary>
    /// <param name="user">The user for whom claims are set.</param>
    /// <param name="operationClaims">The operation claims associated with the user.</param>
    /// <returns>A collection of <see cref="Claim"/> objects.</returns>
    private IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }
}
