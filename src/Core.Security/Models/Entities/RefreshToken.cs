using Core.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Entities;

/// <summary>
/// Represents a refresh token used for renewing authentication sessions for a user.
/// </summary>
public class RefreshToken : Entity<int>
{
    /// <summary>
    /// Gets or sets the ID of the user associated with this refresh token.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the refresh token value.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Gets or sets the expiration date and time of the refresh token.
    /// </summary>
    public DateTimeOffset Expires { get; set; }

    /// <summary>
    /// Gets or sets the IP address from which the refresh token was created.
    /// </summary>
    public required string CreatedByIp { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the refresh token was revoked, if applicable.
    /// </summary>
    public DateTimeOffset? Revoked { get; set; }

    /// <summary>
    /// Gets or sets the IP address from which the refresh token was revoked, if applicable.
    /// </summary>
    public string? RevokedByIp { get; set; }

    /// <summary>
    /// Gets or sets the token that replaced this refresh token, if applicable.
    /// </summary>
    public string? ReplacedByToken { get; set; }

    /// <summary>
    /// Gets or sets the reason for revoking the refresh token, if applicable.
    /// </summary>
    public string? ReasonRevoked { get; set; }

    /// <summary>
    /// The user associated with this refresh token.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a many-to-one relationship where a refresh token is associated with a single user.
    /// The property corresponds to the <see cref="UserId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshToken"/> class with default values.
    /// This constructor is required by Entity Framework Core for materializing entities from the database.
    /// </summary>
    public RefreshToken()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshToken"/> class with the specified values.
    /// </summary>
    /// <param name="userId">The ID of the user associated with this refresh token.</param>
    /// <param name="token">The refresh token value.</param>
    /// <param name="expires">The expiration date and time of the refresh token.</param>
    /// <param name="createdByIp">The IP address from which the refresh token was created.</param>
    public RefreshToken(int userId, string token, DateTimeOffset expires, string createdByIp)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
        CreatedByIp = createdByIp;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshToken"/> class with the specified ID and values.
    /// </summary>
    /// <param name="id">The unique identifier of the refresh token.</param>
    /// <param name="userId">The ID of the user associated with this refresh token.</param>
    /// <param name="token">The refresh token value.</param>
    /// <param name="expires">The expiration date and time of the refresh token.</param>
    /// <param name="createdByIp">The IP address from which the refresh token was created.</param>
    public RefreshToken(int id, int userId, string token, DateTimeOffset expires, string createdByIp)
        : base(id)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
        CreatedByIp = createdByIp;
    }
}
