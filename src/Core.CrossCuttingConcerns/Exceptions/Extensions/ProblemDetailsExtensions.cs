using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

/// <summary>
/// Provides extension methods for serializing <see cref="ProblemDetails"/> objects.
/// </summary>
public static class ProblemDetailsExtensions
{
    /// <summary>
    /// Serializes a <see cref="ProblemDetails"/> instance (or derived type) to its JSON representation.
    /// </summary>
    /// <typeparam name="TProblemDetail">The type of the problem details object.</typeparam>
    /// <param name="details">The problem details instance to serialize.</param>
    /// <returns>A JSON string representation of the problem details.</returns>
    public static string AsJson<TProblemDetail>(this TProblemDetail details)
        where TProblemDetail : ProblemDetails => JsonSerializer.Serialize(details);
}
