using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Constants;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

/// <summary>
/// Represents a logger implementation that logs events to an MS SQL Server database using Serilog.
/// </summary>
public class MsSqlLogger : SerilogLoggerService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MsSqlLogger"/> class with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration object containing the log settings (from appsettings.json or another configuration source).</param>
    public MsSqlLogger(IConfiguration configuration) : base(BuildLogger(configuration))
    {
        
    }

    /// <summary>
    /// Builds the Serilog logger that logs to an MS SQL Server.
    /// </summary>
    /// <param name="configuration">The configuration object containing the log settings.</param>
    /// <returns>A configured Serilog <see cref="ILogger"/> instance.</returns>
    private static ILogger BuildLogger(IConfiguration configuration)
    {
        MsSqlConfiguration logConfig = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration")
            .Get<MsSqlConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = logConfig.TableName,
            AutoCreateSqlTable = logConfig.AutoCreateSqlTable,
        };

        ColumnOptions columnOptions = new();

        return new LoggerConfiguration()
            .WriteTo.MSSqlServer(
                connectionString: logConfig.ConnectionString,
                sinkOptions: sinkOptions,
                columnOptions: columnOptions)
            .CreateLogger();
    }  
}