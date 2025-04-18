using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Models;

/// <summary>
/// Represents a parameter used in method logging.
/// </summary>
public class LogParameter
{
    /// <summary>
    /// Name of the parameter.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Value of the parameter.
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Type of the parameter (e.g., string, int, bool).
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogParameter"/> class with default values.
    /// </summary>
    public LogParameter()
    {
        Name = string.Empty;
        Value = null;
        Type = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogParameter"/> class with specified name, value, and type.
    /// </summary>
    /// <param name="name">Name of the parameter.</param>
    /// <param name="value">Value of the parameter.</param>
    /// <param name="type">Type of the parameter.</param>
    public LogParameter(string name, object value, string type)
    {
        Name = name;
        Value = value;
        Type = type;
    }
}
