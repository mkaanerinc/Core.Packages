using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Hashing;

/// <summary>
/// Provides helper methods for password hashing and verification using HMAC-SHA512 algorithm.
/// </summary>
public static class HashingHelper
{
    /// <summary>
    /// Creates a password hash and salt for the specified password using HMAC-SHA512.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <param name="passwordHash">The generated hash of the password.</param>
    /// <param name="passwordSalt">The generated salt used for hashing.</param>
    /// <exception cref="ArgumentNullException">Thrown when the password is null or empty.</exception>
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

        using HMACSHA512 hmac = new();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    /// <summary>
    /// Verifies a password against the provided hash and salt using HMAC-SHA512.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="passwordHash">The hash to compare against.</param>
    /// <param name="passwordSalt">The salt used to generate the hash.</param>
    /// <returns><c>true</c> if the password matches the hash; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the password, passwordHash, or passwordSalt is null.</exception>
    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
        if (passwordHash == null)
            throw new ArgumentNullException(nameof(passwordHash), "Password hash cannot be null.");
        if (passwordSalt == null)
            throw new ArgumentNullException(nameof(passwordSalt), "Password salt cannot be null.");

        using HMACSHA512 hmac = new(passwordSalt);

        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return CryptographicOperations.FixedTimeEquals(computedHash, passwordHash);
    }
}
