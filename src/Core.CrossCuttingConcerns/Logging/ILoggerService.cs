using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging;

/// <summary>
/// Provides logging functionality for different log levels.
/// </summary>
public interface ILoggerService
{
    /// <summary>
    /// Log a verbose message (used for detailed, debugging logs).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogVerbose(string message);

    /// <summary>
    /// Log a fatal message (used for critical errors).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogFatal(string message);

    /// <summary>
    /// Log a simple information message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogInformation(string message);

    /// <summary>
    /// Log a warning message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogWarning(string message);

    /// <summary>
    /// Log an error message with exception details.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The exception details to log.</param>
    void LogError(string message, Exception exception);

    /// <summary>
    /// Log a debug message (used for debugging purposes).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogDebug(string message);
}