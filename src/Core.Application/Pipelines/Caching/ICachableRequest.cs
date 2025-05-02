using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching;

/// <summary>
/// Defines a contract for requests that can be cached in a caching pipeline.
/// Implementing this interface allows a request to specify caching behavior.
/// </summary>
public interface ICachableRequest
{
    /// <summary>
    /// Gets the unique key used to identify the cached response for this request.
    /// </summary>
    /// <value>
    /// A string representing the cache key.
    /// </value>
    string CacheKey { get; }

    /// <summary>
    /// Gets a value indicating whether caching should be bypassed for this request.
    /// </summary>
    /// <value>
    /// <c>true</c> if the cache should be bypassed; otherwise, <c>false</c>.
    /// </value>
    bool BypassCache { get; }

    /// <summary>
    /// Gets the optional group key to associate multiple cache entries for invalidation purposes.
    /// </summary>
    /// <value>
    /// A string representing the cache group key, or <c>null</c> if no group key is specified.
    /// </value>
    string? CacheGroupKey { get; }

    /// <summary>
    /// Gets the optional sliding expiration time for the cached response.
    /// </summary>
    /// <value>
    /// A <see cref="TimeSpan"/> representing the sliding expiration duration, or <c>null</c> if no expiration is specified.
    /// </value>
    TimeSpan? SlidingExpiration { get; }
}