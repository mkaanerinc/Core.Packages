using Core.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Entities;

/// <summary>
/// Represents an OTP (One-Time Password) authenticator used to manage two-factor authentication for a user.
/// </summary>
public class OtpAuthenticator : Entity<int>
{
    /// <summary>
    /// Gets or sets the ID of the user associated with this OTP authenticator.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the secret key used to generate one-time passwords.
    /// </summary>
    public required byte[] SecretKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the OTP authenticator has been verified.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// The user associated with this OTP authenticator.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a many-to-one relationship where an OTP authenticator is associated with a single user.
    /// The property corresponds to the <see cref="UserId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="OtpAuthenticator"/> class with default values.
    /// This constructor is required by Entity Framework Core for materializing entities from the database.
    /// </summary>
    public OtpAuthenticator()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OtpAuthenticator"/> class with the specified values.
    /// </summary>
    /// <param name="userId">The ID of the user associated with this OTP authenticator.</param>
    /// <param name="secretKey">The secret key used to generate one-time passwords.</param>
    /// <param name="isVerified">A value indicating whether the OTP authenticator has been verified.</param>
    public OtpAuthenticator(int userId, byte[] secretKey, bool isVerified)
    {
        UserId = userId;
        SecretKey = secretKey;
        IsVerified = isVerified;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OtpAuthenticator"/> class with the specified ID and values.
    /// </summary>
    /// <param name="id">The unique identifier of the OTP authenticator.</param>
    /// <param name="userId">The ID of the user associated with this OTP authenticator.</param>
    /// <param name="secretKey">The secret key used to generate one-time passwords.</param>
    /// <param name="isVerified">A value indicating whether the OTP authenticator has been verified.</param>
    public OtpAuthenticator(int id, int userId, byte[] secretKey, bool isVerified)
        : base(id)
    {
        UserId = userId;
        SecretKey = secretKey;
        IsVerified = isVerified;
    }
}
