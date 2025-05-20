using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Types;

/// <summary>
/// Represents errors that occur during authorization.
/// </summary>
public class AuthorizationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
    /// </summary>

    public AuthorizationException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public AuthorizationException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationException"/> class with a specified 
    /// error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public AuthorizationException(string? message, Exception? innerException) : base(message, innerException) { }
}
