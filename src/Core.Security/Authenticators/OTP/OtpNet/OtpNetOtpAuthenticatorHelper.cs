using Core.Security.Authenticators.OTP.Interfaces;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Authenticators.OTP.OtpNet;

/// <summary>
/// Provides OTP authentication helper methods using the OtpNet library.
/// </summary>
public class OtpNetOtpAuthenticatorHelper : IOtpAuthenticatorHelper
{
    /// <summary>
    /// Converts a secret key byte array to a Base32-encoded string.
    /// </summary>
    /// <param name="secretKey">The secret key as a byte array.</param>
    /// <returns>A task representing the asynchronous operation. The result contains the Base32-encoded string.</returns>
    public Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        string base32String = Base32Encoding.ToString(secretKey);
        return Task.FromResult(base32String);
    }

    /// <summary>
    /// Generates a new 20-byte random secret key and returns its Base32-decoded form.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The result contains a byte array representing the secret key.
    /// </returns>
    /// <remarks>
    /// The method generates a Base32 string from the key and decodes it again to ensure correct format.
    /// </remarks>
    public Task<byte[]> GenerateSecretKey()
    {
        byte[] key = KeyGeneration.GenerateRandomKey(20);

        string base32String = Base32Encoding.ToString(key);
        byte[] base32Bytes = Base32Encoding.ToBytes(base32String);

        return Task.FromResult(base32Bytes);
    }

    /// <summary>
    /// Verifies the provided OTP code using the given secret key.
    /// </summary>
    /// <param name="secretKey">The secret key used to generate the OTP.</param>
    /// <param name="code">The OTP code to verify.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The result is <c>true</c> if the code is valid; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Allows for a 30-second window before and after the current time to account for clock drift.
    /// </remarks>
    public Task<bool> VerifyCode(byte[] secretKey, string code)
    {
        Totp totp = new(secretKey);
        bool result = totp.VerifyTotp(code, out _, new VerificationWindow(previous: 1, future: 1));

        return Task.FromResult(result);
    }
}
