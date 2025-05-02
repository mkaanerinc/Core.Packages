using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching;

/// <summary>
/// Represents configuration settings for caching behavior in a caching pipeline.
/// </summary>
public class CacheSettings
{
    /// <summary>
    /// Gets or sets the sliding expiration time (in seconds) for cached items.
    /// </summary>
    /// <value>
    /// An integer representing the sliding expiration duration in seconds. A value of zero or negative indicates no expiration.
    /// </value>
    public int SlidingExpiration { get; set; }
}