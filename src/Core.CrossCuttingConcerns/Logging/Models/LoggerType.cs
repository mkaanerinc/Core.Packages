using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Models;

/// <summary>
/// Specifies the type of logger to be used for logging operations.
/// </summary>
public enum LoggerType
{
    /// <summary>
    /// Uses a file-based logger to write logs to a local or shared file.
    /// </summary>
    FileLogger,

    /// <summary>
    /// Uses a Microsoft SQL Server-based logger to write logs to a database table.
    /// </summary>
    MsSqlLogger
}