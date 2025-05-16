using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Authenticators.OTP.Interfaces;

/// <summary>
/// Defines helper operations for managing OTP (One-Time Password) authenticators.
/// </summary>
public interface IOtpAuthenticatorHelper
{
    /// <summary>
    /// Generates a new secret key used for OTP generation.
    /// </summary>
    /// <returns>A byte array representing the generated secret key.</returns>
    public Task<byte[]> GenerateSecretKey();

    /// <summary>
    /// Converts a byte array secret key into a Base32-encoded string.
    /// </summary>
    /// <param name="secretKey">The secret key in byte array format.</param>
    /// <returns>A Base32 string representation of the secret key.</returns>
    public Task<string> ConvertSecretKeyToString(byte[] secretKey);

    /// <summary>
    /// Verifies whether the provided OTP code is valid for the given secret key.
    /// </summary>
    /// <param name="secretKey">The secret key in byte array format.</param>
    /// <param name="code">The OTP code to verify.</param>
    /// <returns>True if the code is valid; otherwise, false.</returns>
    public Task<bool> VerifyCode(byte[] secretKey, string code);

}
