using Core.Infrastructure.Mailing;
using Core.Security.Authenticators.Email.Interfaces;
using Core.Security.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Authenticators.Email;

/// <summary>
/// Provides helper methods for email authenticator operations, focused on activation key management and email sending.
/// </summary>
public class EmailAuthenticatorHelper : IEmailAuthenticatorHelper
{
    private readonly IMailService _mailService;

    /// <summary>
    /// Initializes a new instance of the EmailAuthenticatorHelper class.
    /// </summary>
    /// <param name="mailService">The mail service for sending activation emails.</param>
    public EmailAuthenticatorHelper(IMailService mailService)
    {
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
    }

    /// <inheritdoc />
    public async Task<string> GenerateActivationKey()
    {
        // Generate a random 6-digit activation key
        byte[] randomBytes = new byte[4];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        int activationCode = BitConverter.ToInt32(randomBytes, 0) % 900000 + 100000; // Ensures 6 digits
        string activationKey = activationCode.ToString("D6"); // Pad with leading zeros if needed

        return await Task.FromResult(activationKey);
    }

    /// <inheritdoc />
    public Task<bool> VerifyActivationKey(string activationKey, string expectedKey)
    {
        if (string.IsNullOrEmpty(activationKey) || string.IsNullOrEmpty(expectedKey))
        {
            return Task.FromResult(false);
        }

        // Compare the provided activation key with the expected key
        bool isValid = string.Equals(activationKey.Trim(), expectedKey.Trim(), StringComparison.Ordinal);
        return Task.FromResult(isValid);
    }

    /// <inheritdoc />
    public async Task SendActivationEmailAsync(User user, string activationKey)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ArgumentException("User email cannot be empty.", nameof(user));
        }

        // Send activation key via email
        string body = $"<h1>Email Verification</h1><p>Your activation code is: <strong>{activationKey}</strong></p>" +
                      "<p>Please use this code to verify your email address.</p>";
        await _mailService.SendEmailAsync(user.Email, "Email Verification", body);
    }
}
