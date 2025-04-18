using Core.CrossCuttingConcerns.Logging.Serilog.Constants;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog;

/// <summary>
/// Base class for Serilog logger implementations, providing core logging functionality.
/// </summary>
public abstract class SerilogLoggerService : ILoggerService
{
    /// <summary>
    /// The Serilog logger instance.
    /// </summary>
    protected ILogger Logger { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SerilogLoggerService"/> class with a given Serilog logger.
    /// </summary>
    /// <param name="logger">The Serilog logger to use.</param>
    /// <exception cref="Exception">Thrown when logger is null.</exception>
    protected SerilogLoggerService(ILogger logger)
    {
        Logger = logger ?? throw new Exception(SerilogMessages.LoggerIsNullMessage);
    }

    /// <summary>
    /// Logs a debug message for detailed diagnostic purposes.
    /// </summary>
    /// <param name="message">The debug message to log.</param>
    public void LogDebug(string message) => Logger.Debug(message);

    /// <summary>
    /// Logs an error message with associated exception details.
    /// </summary>
    /// <param name="message">The error message to log.</param>
    /// <param name="exception">The exception to include in the log.</param>
    public void LogError(string message, Exception exception) => Logger.Error(exception, message);

    /// <summary>
    /// Logs a fatal message for critical failures.
    /// </summary>
    /// <param name="message">The fatal message to log.</param>
    public void LogFatal(string message) => Logger.Fatal(message);

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The informational message to log.</param>
    public void LogInformation(string message) => Logger.Information(message);

    /// <summary>
    /// Logs a verbose message, typically used for detailed tracing.
    /// </summary>
    /// <param name="message">The verbose message to log.</param>
    public void LogVerbose(string message) => Logger.Verbose(message);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The warning message to log.</param>
    public void LogWarning(string message) => Logger.Warning(message);
}