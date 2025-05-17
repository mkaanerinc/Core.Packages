using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Constants;

/// <summary>
/// Contains general operation claims used across the application for authorization purposes.
/// </summary>
public class GeneralOperationClaims
{
    /// <summary>
    /// Represents administrative privileges.
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    /// Represents basic user privileges.
    /// </summary>
    public const string User = "User";

    /// <summary>
    /// Represents moderator-level access.
    /// </summary>
    public const string Moderator = "Moderator";

    /// <summary>
    /// Represents highest-level system access.
    /// </summary>
    public const string SuperAdmin = "SuperAdmin";

    /// <summary>
    /// Represents a verified email address.
    /// </summary>
    public const string EmailVerified = "EmailVerified";

    /// <summary>
    /// Indicates that the user has enabled two-factor authentication.
    /// </summary>
    public const string TwoFactorEnabled = "TwoFactorEnabled";

    /// <summary>
    /// Allows managing user accounts and permissions.
    /// </summary>
    public const string CanManageUsers = "CanManageUsers";

    /// <summary>
    /// Grants access to reporting modules or data exports.
    /// </summary>
    public const string CanAccessReports = "CanAccessReports";

    /// <summary>
    /// Grants access to critical system configurations.
    /// </summary>
    public const string CanConfigureSystem = "CanConfigureSystem";

    /// <summary>
    /// Represents a developer or technical user with internal access.
    /// </summary>
    public const string Developer = "Developer";

    /// <summary>
    /// Grants read-only access for auditing purposes.
    /// </summary>
    public const string Auditor = "Auditor";
}
