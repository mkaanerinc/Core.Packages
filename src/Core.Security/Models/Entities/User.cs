using Core.Infrastructure.Persistence.Repositories;
using Core.Security.Authenticators.Email.Models;
using Core.Security.Authenticators.OTP.Models;
using Core.Security.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Entities;

/// <summary>
/// Represents a user in the system with authentication and authorization details.
/// </summary>
public class User : Entity<int>
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the salt used for password hashing.
    /// </summary>
    public required byte[] PasswordSalt { get; set; }

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// </summary>
    public required byte[] PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the status of the user (e.g., Passive, Active, Suspended).
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The collection of operation claims associated with this user.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single user can be associated with multiple operation claims.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique operation claims.
    /// </remarks>
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = new HashSet<UserOperationClaim>();

    /// <summary>
    /// The collection of refresh tokens associated with this user.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single user can be associated with multiple refresh tokens.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique refresh tokens.
    /// </remarks>
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();

    /// <summary>
    /// The collection of otp authenticators associated with this user.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single user can be associated with multiple otp authenticators.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique otp authenticators.
    /// </remarks>
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = new HashSet<OtpAuthenticator>();

    /// <summary>
    /// The collection of email authenticators associated with this user.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single user can be associated with multiple email authenticators.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique email authenticators.
    /// </remarks>
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = new HashSet<EmailAuthenticator>();

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with default values.
    /// This constructor is required by Entity Framework Core for materializing entities from the database.
    /// </summary>
    public User()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with the specified values.
    /// </summary>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="passwordSalt">The salt used for password hashing.</param>
    /// <param name="passwordHash">The hashed password of the user.</param>
    /// <param name="status">The status of the user (e.g., Passive, Active, Suspended).</param>
    public User(string firstName, string lastName, string email, byte[] passwordSalt,
        byte[] passwordHash, UserStatus status)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with the specified ID and values.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="passwordSalt">The salt used for password hashing.</param>
    /// <param name="passwordHash">The hashed password of the user.</param>
    /// <param name="status">The status of the user (e.g., Passive, Active, Suspended).</param>
    public User(int id, string firstName, string lastName, string email, byte[] passwordSalt, 
        byte[] passwordHash, UserStatus status) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
    }
}
