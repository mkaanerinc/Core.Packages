using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Constants;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

/// <summary>
/// Represents a file-based logger implementation using Serilog.
/// </summary>
public class FileLogger : SerilogLoggerService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileLogger"/> class with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration object containing the log settings (from appsettings.json or another configuration source).</param>
    public FileLogger(IConfiguration configuration) : base(BuildLogger(configuration))
    {

    }

    /// <summary>
    /// Builds the Serilog logger with the provided configuration settings.
    /// </summary>
    /// <param name="configuration">The configuration object containing the log settings (from appsettings.json or another configuration source).</param>
    /// <returns>Returns a configured Serilog <see cref="ILogger"/> instance.</returns>
    /// <exception cref="Exception">Thrown when the configuration settings are invalid or missing.</exception>
    private static ILogger BuildLogger(IConfiguration configuration)
    {      
        FileLogConfiguration logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
            .Get<FileLogConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfig.FolderPath, arg1: logConfig.FileName);

        return new LoggerConfiguration()
            .WriteTo.File(

                path: logFilePath,
                rollingInterval: Enum.TryParse<RollingInterval>(logConfig.RollingInterval, out var interval)
                    ? interval
                    : RollingInterval.Day,
                retainedFileCountLimit: logConfig.RetainedFileCountLimit,
                fileSizeLimitBytes: logConfig.FileSizeLimitBytes,
                rollOnFileSizeLimit: logConfig.RollOnFileSizeLimit,
                shared: logConfig.Shared,
                outputTemplate: logConfig.OutputTemplate)
            .CreateLogger();
    }
}