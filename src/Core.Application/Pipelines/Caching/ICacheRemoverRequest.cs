using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching;

/// <summary>
/// Defines a contract for requests that trigger cache removal in a caching pipeline.
/// Implementing this interface allows a request to specify cache invalidation behavior.
/// </summary>
public interface ICacheRemoverRequest
{
    /// <summary>
    /// Gets the optional cache key to identify the specific cache entry to be removed.
    /// </summary>
    /// <value>
    /// A string representing the cache key to remove, or <c>null</c> if no specific key is targeted.
    /// </value>
    string? CacheKey { get; }

    /// <summary>
    /// Gets a value indicating whether cache removal should be bypassed for this request.
    /// </summary>
    /// <value>
    /// <c>true</c> if cache removal should be bypassed; otherwise, <c>false</c>.
    /// </value>
    bool BypassCache { get; }

    /// <summary>
    /// Gets the optional group key to identify a group of cache entries to be removed.
    /// </summary>
    /// <value>
    /// A string representing the cache group key, or <c>null</c> if no group key is specified.
    /// </value>
    string? CacheGroupKey { get; }
}