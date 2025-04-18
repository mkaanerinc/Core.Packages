using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Models;

/// <summary>
/// Represents detailed information for a log entry, including class, method, user, parameters, and exception details.
/// </summary>
public class LogDetailWithException : LogDetail
{
    /// <summary>
    /// The exception message related to the log entry.
    /// </summary>
    public string ExceptionMessage { get; set; }

    /// <summary>
    /// The type of the exception related to the log entry.
    /// </summary>
    public string ExceptionType { get; set; }

    /// <summary>
    /// The stack trace related to the exception, useful for debugging.
    /// </summary>
    public string StackTrace { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogDetailWithException"/> class with default values.
    /// </summary>
    public LogDetailWithException()
    {
        ExceptionMessage = string.Empty;
        ExceptionType = string.Empty;
        StackTrace = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogDetailWithException"/> class with specified values.
    /// </summary>
    /// <param name="fullClassName">The fully qualified name of the class (including namespace).</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="user">The user initiating the action.</param>
    /// <param name="logParameters">The list of parameters to log.</param>
    /// <param name="exceptionMessage">The exception message to log.</param>
    /// <param name="exceptionType">The exception type to log.</param>
    /// <param name="stackTrace">The stack trace related to the exception to log.</param>
    public LogDetailWithException(string fullClassName, string methodName, string user, List<LogParameter> logParameters,
        string exceptionMessage, string exceptionType, string stackTrace)
        : base(fullClassName, methodName, user, logParameters)
    {
        ExceptionMessage = exceptionMessage;
        ExceptionType = exceptionType;
        StackTrace = stackTrace;
    }
}