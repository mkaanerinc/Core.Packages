using Core.CrossCuttingConcerns.Logging;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching;

/// <summary>
/// A MediatR pipeline behavior that removes cache entries for requests implementing <see cref="ICacheRemoverRequest"/>.
/// Removes specific cache entries or groups of entries based on the request's cache key or group key.
/// </summary>
/// <typeparam name="TRequest">The type of the request. Must implement <see cref="IRequest{TResponse}"/> and <see cref="ICacheRemoverRequest"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    private readonly IDistributedCache _cache;
    private readonly ILoggerService _loggerServiceBase;

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheRemovingBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="cache">The distributed cache service for removing cache entries.</param>
    /// <param name="loggerServiceBase">The logger to log cache removal operations and errors.</param>
    public CacheRemovingBehavior(IDistributedCache cache, ILoggerService loggerServiceBase)
    {
        _cache = cache;
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Handles the request by executing the next handler and removing cache entries based on the request's cache key or group key.
    /// If caching is bypassed, no cache removal is performed.
    /// </summary>
    /// <param name="request">The MediatR request to handle.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response from the next handler in the pipeline.</returns>
    /// <exception cref="JsonException">Thrown when deserialization of cache group data fails.</exception>
    /// <exception cref="InvalidOperationException">Thrown when cache removal operations fail unexpectedly.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
        {
            _loggerServiceBase.LogInformation($"Bypassing cache removal for request with key: {request.CacheKey}");
            return await next();
        }

        TResponse response;

        try
        {
            // Execute the next handler first
            response = await next();

            // Remove cache entries for the group key, if specified
            if (request.CacheGroupKey != null)
            {
                byte[]? cachedGroup = await _cache.GetAsync(request.CacheGroupKey, cancellationToken);

                if (cachedGroup != null)
                {
                    HashSet<string> keysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cachedGroup))!;

                    if (keysInGroup == null)
                    {
                        _loggerServiceBase.LogWarning($"Failed to deserialize cache group for key: {request.CacheGroupKey}. Skipping group removal.");
                    }
                    else
                    {
                        foreach (string key in keysInGroup)
                        {
                            await _cache.RemoveAsync(key, cancellationToken);
                            _loggerServiceBase.LogInformation($"Removed cache entry: {key}");
                        }

                        await _cache.RemoveAsync(request.CacheGroupKey, cancellationToken);
                        _loggerServiceBase.LogInformation($"Removed cache group: {request.CacheGroupKey}");

                        await _cache.RemoveAsync(key: $"{request.CacheGroupKey}SlidingExpiration", cancellationToken);
                        _loggerServiceBase.LogInformation($"Removed cache group sliding expiration: {request.CacheGroupKey}SlidingExpiration");
                    }
                }
                else
                {
                    _loggerServiceBase.LogInformation($"No cache group found for key: {request.CacheGroupKey}. Skipping group removal.");
                }
            }

            // Remove specific cache entry, if specified
            if (request.CacheKey != null)
            {
                await _cache.RemoveAsync(request.CacheKey, cancellationToken);
                _loggerServiceBase.LogInformation($"Removed cache entry: {request.CacheKey}");
            }
        }
        catch (JsonException ex)
        {
            _loggerServiceBase.LogError($"Failed to deserialize cache group data for key: {request.CacheGroupKey}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Error removing cache entries for key: {request.CacheKey} or group: {request.CacheGroupKey}", ex);
            throw new InvalidOperationException($"Cache removal failed for key: {request.CacheKey} or group: {request.CacheGroupKey}. See inner exception for details.", ex);
        }

        return response;
    }
}