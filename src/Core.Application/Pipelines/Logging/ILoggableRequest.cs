using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Logging;

/// <summary>
/// Represents a marker interface for requests that should be logged.
/// Classes implementing this interface will be processed by logging mechanisms.
/// </summary>
public interface ILoggableRequest
{
}