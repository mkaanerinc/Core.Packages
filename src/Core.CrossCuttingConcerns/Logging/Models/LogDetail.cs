using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Models;

/// <summary>
/// Represents detailed information for a log entry, including full class name, method, user, and parameters.
/// </summary>
public class LogDetail
{
    /// <summary>
    /// The fully qualified class name (including namespace) where the log is recorded.
    /// </summary>
    public string FullClassName { get; set; }

    /// <summary>
    /// The name of the method where the log is recorded.
    /// </summary>
    public string MethodName { get; set; }

    /// <summary>
    /// The user who initiated the action being logged.
    /// </summary>
    public string User { get; set; }

    /// <summary>
    /// The list of parameters involved in the logged method call.
    /// </summary>
    public List<LogParameter> LogParameters { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogDetail"/> class with default values.
    /// </summary>
    public LogDetail()
    {
        FullClassName = string.Empty;
        MethodName = string.Empty;
        User = string.Empty;
        LogParameters = new List<LogParameter>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogDetail"/> class with specified values.
    /// </summary>
    /// <param name="fullClassName">The fully qualified name of the class (including namespace).</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="user">The user initiating the action.</param>
    /// <param name="logParameters">The list of parameters to log.</param>
    public LogDetail(string fullClassName, string methodName, string user, List<LogParameter> logParameters)
    {
        FullClassName = fullClassName;
        MethodName = methodName;
        User = user;
        LogParameters = logParameters ?? new List<LogParameter>();
    }
}
