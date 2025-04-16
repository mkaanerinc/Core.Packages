using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Types;

/// <summary>
/// Represents a custom exception that is thrown when a business rule is violated.
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessException"/> class.
    /// </summary>
    public BusinessException()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessException"/> class with a custom error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BusinessException(string? message) : base(message)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessException"/> class with a custom error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public BusinessException(string? message, Exception? innerException) : base(message, innerException)
    {

    }
}
