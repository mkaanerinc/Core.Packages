using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core.Shared.Extensions;

/// <summary>
/// Provides extension methods for working with enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the display name of an enum value if the <see cref="DisplayAttribute"/> is defined;
    /// otherwise, returns the enum name itself.
    /// </summary>
    /// <param name="enumValue">The enum value.</param>
    /// <returns>The display name of the enum value.</returns>
    public static string GetDisplayName(this Enum enumValue)
    {
        var displayAttribute = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.Name ?? enumValue.ToString();
    }
}
