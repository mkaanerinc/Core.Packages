using Core.Security.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Authenticators.Email.Interfaces;

/// <summary>
/// Defines the contract for email authenticator helper operations.
/// </summary>
public interface IEmailAuthenticatorHelper
{
    /// <summary>
    /// Generates a new activation key for email verification.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the generated activation key.</returns>
    Task<string> GenerateActivationKey();

    /// <summary>
    /// Verifies the provided activation key against the expected value.
    /// </summary>
    /// <param name="activationKey">The activation key to verify.</param>
    /// <param name="expectedKey">The expected activation key to compare against.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the activation key is valid.</returns>
    Task<bool> VerifyActivationKey(string activationKey, string expectedKey);

    /// <summary>
    /// Sends an activation email to the specified user with the provided activation key.
    /// </summary>
    /// <param name="user">The user to send the email to.</param>
    /// <param name="activationKey">The activation key to include in the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendActivationEmailAsync(User user, string activationKey);
}
