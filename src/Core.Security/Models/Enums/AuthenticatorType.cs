using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Enums;

/// <summary>
/// Represents the type of authenticator used for user authentication.
/// </summary>
public enum AuthenticatorType
{
    /// <summary>
    /// No authenticator is assigned.
    /// </summary>
    None = 0,

    /// <summary>
    /// Email-based authenticator.
    /// </summary>
    Email = 1,

    /// <summary>
    /// One-time password (OTP) authenticator.
    /// </summary>
    Otp = 2
}
