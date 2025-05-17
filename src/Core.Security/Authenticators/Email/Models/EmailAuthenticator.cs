using Core.Infrastructure.Persistence.Repositories;
using Core.Security.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Authenticators.Email.Models;

/// <summary>
/// Represents an email-based authenticator used to manage email verification for a user.
/// </summary>
public class EmailAuthenticator : Entity<int>
{
    /// <summary>
    /// Gets or sets the ID of the user associated with this email authenticator.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the activation key used for email verification, if applicable.
    /// </summary>
    public string? ActivationKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email has been verified.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// The user associated with this email authenticator.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a many-to-one relationship where an email authenticator is associated with a single user.
    /// The property corresponds to the <see cref="UserId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAuthenticator"/> class with default values.
    /// This constructor is required by Entity Framework Core for materializing entities from the database.
    /// </summary>
    public EmailAuthenticator()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAuthenticator"/> class with the specified values.
    /// </summary>
    /// <param name="userId">The ID of the user associated with this email authenticator.</param>
    /// <param name="isVerified">A value indicating whether the email has been verified.</param>
    public EmailAuthenticator(int userId, bool isVerified)
    {
        UserId = userId;
        IsVerified = isVerified;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAuthenticator"/> class with the specified values, including the activation key.
    /// </summary>
    /// <param name="userId">The ID of the user associated with this email authenticator.</param>
    /// <param name="activationKey">The activation key used for email verification, if applicable.</param>
    /// <param name="isVerified">A value indicating whether the email has been verified.</param>
    public EmailAuthenticator(int userId, string? activationKey, bool isVerified)
    {
        UserId = userId;
        ActivationKey = activationKey;
        IsVerified = isVerified;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAuthenticator"/> class with the specified ID and values.
    /// </summary>
    /// <param name="id">The unique identifier of the email authenticator.</param>
    /// <param name="userId">The ID of the user associated with this email authenticator.</param>
    /// <param name="isVerified">A value indicating whether the email has been verified.</param>
    public EmailAuthenticator(int id, int userId, bool isVerified)
        : base(id)
    {
        UserId = userId;
        IsVerified = isVerified;
    }
}
