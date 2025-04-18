using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Constants;

/// <summary>
/// Contains constant messages related to Serilog logging operations.
/// </summary>
public static class SerilogMessages
{
    /// <summary>
    /// Message displayed when a null or empty configuration value is provided.
    /// </summary>
    public static string NullOptionsMessage => "You have sent a blank value! Something went wrong. Please try again.";

    /// <summary>
    /// Message displayed when the Serilog logger instance is null.
    /// </summary>
    public static string LoggerIsNullMessage => "Serilog Logger is null.";
}