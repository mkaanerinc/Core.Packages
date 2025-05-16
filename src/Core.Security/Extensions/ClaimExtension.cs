using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Extensions;

/// <summary>
/// Provides extension methods to add claims to an <see cref="ICollection{Claim}"/> for JWT token generation.
/// </summary>
public static class ClaimExtension
{
    /// <summary>
    /// Adds an email claim to the specified claims collection using the JWT registered claim name.
    /// </summary>
    /// <param name="claims">The collection of claims to add the email to.</param>
    /// <param name="email">The email address to add as a claim.</param>
    /// <exception cref="ArgumentNullException">Thrown when the claims collection or email is null.</exception>
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        if (claims == null)
            throw new ArgumentNullException(nameof(claims), "Claims collection cannot be null.");
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

        claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
    }

    /// <summary>
    /// Adds a name claim to the specified claims collection.
    /// </summary>
    /// <param name="claims">The collection of claims to add the name to.</param>
    /// <param name="name">The name to add as a claim.</param>
    /// <exception cref="ArgumentNullException">Thrown when the claims collection or name is null.</exception>
    public static void AddName(this ICollection<Claim> claims, string name)
    {
        if (claims == null)
            throw new ArgumentNullException(nameof(claims), "Claims collection cannot be null.");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "Name cannot be null or empty.");

        claims.Add(new Claim(ClaimTypes.Name, name));
    }

    /// <summary>
    /// Adds a name identifier claim to the specified claims collection.
    /// </summary>
    /// <param name="claims">The collection of claims to add the name identifier to.</param>
    /// <param name="nameIdentifier">The name identifier to add as a claim.</param>
    /// <exception cref="ArgumentNullException">Thrown when the claims collection or name identifier is null.</exception>
    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        if (claims == null)
            throw new ArgumentNullException(nameof(claims), "Claims collection cannot be null.");
        if (string.IsNullOrEmpty(nameIdentifier))
            throw new ArgumentNullException(nameof(nameIdentifier), "Name identifier cannot be null or empty.");

        claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
    }

    /// <summary>
    /// Adds multiple role claims to the specified claims collection.
    /// </summary>
    /// <param name="claims">The collection of claims to add the roles to.</param>
    /// <param name="roles">The array of roles to add as claims.</param>
    /// <exception cref="ArgumentNullException">Thrown when the claims collection is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the roles array is null or empty.</exception>
    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        if (claims == null)
            throw new ArgumentNullException(nameof(claims), "Claims collection cannot be null.");
        if (roles == null || !roles.Any())
            throw new ArgumentException("Roles cannot be null or empty.", nameof(roles));

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
    }
}
