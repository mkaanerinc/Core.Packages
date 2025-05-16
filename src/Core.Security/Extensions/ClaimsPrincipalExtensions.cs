using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Extensions;

/// <summary>
/// Provides extension methods for <see cref="ClaimsPrincipal"/> to simplify accessing claim values.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Retrieves all claim values of the specified claim type from the <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> to retrieve claims from.</param>
    /// <param name="claimType">The type of claim to retrieve.</param>
    /// <returns>A list of claim values for the specified claim type, or an empty list if none are found or if the principal is null.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the claim type is null or empty.</exception>
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        if (string.IsNullOrEmpty(claimType))
            throw new ArgumentNullException(nameof(claimType), "Claim type cannot be null or empty.");

        var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList() ?? new List<string>();

        return result;
    }

    /// <summary>
    /// Retrieves all role claims from the <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> to retrieve roles from.</param>
    /// <returns>A list of role claim values, or an empty list if none are found or if the principal is null.</returns>
    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Role) ?? new List<string>();
    }

    /// <summary>
    /// Retrieves the user ID from the <see cref="ClaimsPrincipal"/> using the NameIdentifier claim.
    /// </summary>
    /// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> to retrieve the user ID from.</param>
    /// <returns>The user ID as an integer.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the user ID claim is missing or cannot be converted to an integer.</exception>
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdClaim = claimsPrincipal?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault();

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            throw new InvalidOperationException("User ID claim is missing or invalid.");

        return userId;
    }
}
