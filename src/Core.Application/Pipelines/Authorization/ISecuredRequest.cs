using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Authorization;

/// <summary>
/// Represents a request that requires role-based authorization.
/// </summary>
public interface ISecuredRequest
{
    /// <summary>
    /// Gets the roles required to execute the request.
    /// </summary>
    public string[] Roles { get; }
}
