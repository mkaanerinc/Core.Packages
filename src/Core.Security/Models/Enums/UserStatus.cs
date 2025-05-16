using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Enums;

/// <summary>
/// Defines the possible status values for a user in the system.
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// Indicates that the user account is passive and not active.
    /// </summary>
    Passive = 0,

    /// <summary>
    /// Indicates that the user account is active and can be used.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Indicates that the user account is suspended and temporarily disabled.
    /// </summary>
    Suspended = 2
}
