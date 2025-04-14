using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Types;

/// <summary>
/// Represents a custom exception type used for application-level validation errors.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Gets the collection of validation error details.
    /// </summary>
    public IEnumerable<ValidationExceptionModel> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with no message or inner exception.
    /// </summary>
    public ValidationException()
        : base()
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ValidationException(string? message)
        : base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified 
    /// error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ValidationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a list of validation errors.
    /// </summary>
    /// <param name="errors">The collection of validation error details.</param>
    public ValidationException(IEnumerable<ValidationExceptionModel> errors)
        : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    /// <summary>
    /// Builds a formatted error message from a list of validation errors.
    /// </summary>
    /// <param name="errors">The collection of validation error details.</param>
    /// <returns>A formatted error message string.</returns>
    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        IEnumerable<string> arr = errors.Select(
            x => $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, values: x.Errors ?? Array.Empty<string>())}"
        );
        return $"Validation failed: {string.Join(string.Empty, arr)}";
    }
}

/// <summary>
/// Represents a validation failure for a specific property, including the property name and related error messages.
/// </summary>
public class ValidationExceptionModel
{
    /// <summary>
    /// Gets or sets the name of the property that failed validation.
    /// </summary>
    public string? Property { get; set; }

    /// <summary>
    /// Gets or sets the collection of error messages related to the property.
    /// </summary>
    public IEnumerable<string>? Errors { get; set; }
}
