using Core.CrossCuttingConcerns.Logging;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching;

/// <summary>
/// A MediatR pipeline behavior that implements caching for requests implementing <see cref="ICachableRequest"/>.
/// Caches the response if not already cached, or retrieves it from the cache if available.
/// </summary>
/// <typeparam name="TRequest">The type of the request. Must implement <see cref="IRequest{TResponse}"/> and <see cref="ICachableRequest"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly CacheSettings _cacheSettings;
    private readonly IDistributedCache _cache;
    private readonly ILoggerService _loggerServiceBase;

    /// <summary>
    /// Initializes a new instance of the <see cref="CachingBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="cache">The distributed cache service for storing and retrieving cached data.</param>
    /// <param name="logger">The logger to log caching operations and errors.</param>
    /// <param name="configuration">The configuration to retrieve cache settings.</param>
    /// <exception cref="InvalidOperationException">Thrown when cache settings cannot be retrieved from configuration.</exception>
    public CachingBehavior(IDistributedCache cache, ILoggerService loggerServiceBase, IConfiguration configuration)
    {
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();
        _cache = cache;
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Handles the request by checking the cache or executing the request and caching the response.
    /// If caching is bypassed or no cached response exists, the request is processed and cached.
    /// </summary>
    /// <param name="request">The MediatR request to handle.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response from the cache or the next handler in the pipeline.</returns>
    /// <exception cref="JsonException">Thrown when deserialization of cached data fails.</exception>
    /// <exception cref="InvalidOperationException">Thrown when cache operations fail unexpectedly.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
        {
            return await next();
        }

        TResponse response;

        try
        {
            byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);

            if (cachedResponse != null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse))
                    ?? throw new JsonException($"Failed to deserialize cached response for key: {request.CacheKey}");
                _loggerServiceBase.LogInformation($"Fetched from Cache => {request.CacheKey}");
            }
            else
            {
                response = await getResponseAndAddToCache(request, next, cancellationToken);
            }
        }
        catch (JsonException ex)
        {
            _loggerServiceBase.LogError($"Failed to deserialize cached response for key: {request.CacheKey}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Error accessing cache for key: {request.CacheKey}", ex);
            throw new InvalidOperationException($"Cache operation failed for key: {request.CacheKey}", ex);
        }

        return response;
    }

    /// <summary>
    /// Executes the request, caches the response, and optionally adds the cache key to a cache group.
    /// </summary>
    /// <param name="request">The MediatR request to process.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response from the next handler in the pipeline.</returns>
    /// <exception cref="JsonException">Thrown when serialization of the response fails.</exception>
    /// <exception cref="InvalidOperationException">Thrown when cache operations fail unexpectedly.</exception>
    private async Task<TResponse> getResponseAndAddToCache(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response = await next();

        TimeSpan slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);

        DistributedCacheEntryOptions cacheOptions = new()
        {
            SlidingExpiration = slidingExpiration,
        };

        try
        {
            byte[] serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

            await _cache.SetAsync(request.CacheKey, serializedData, cacheOptions, cancellationToken);
            _loggerServiceBase.LogInformation($"Added to Cache => {request.CacheKey} (SlidingExpiration: {slidingExpiration.TotalSeconds}s)");

            if (request.CacheGroupKey != null)
            {
                await addCacheKeyToGroup(request, slidingExpiration, cancellationToken);
            }
        }
        catch (JsonException ex)
        {
            _loggerServiceBase.LogError($"Failed to serialize response for cache key: {request.CacheKey}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Error adding to cache for key: {request.CacheKey}", ex);
            throw new InvalidOperationException($"Failed to cache response for key: {request.CacheKey}", ex);
        }

        return response;
    }

    /// <summary>
    /// Adds the cache key to a cache group for group-based invalidation and updates the group's sliding expiration.
    /// </summary>
    /// <param name="request">The MediatR request containing cache group information.</param>
    /// <param name="slidingExpiration">The sliding expiration duration for the cache group.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <exception cref="JsonException">Thrown when serialization or deserialization of cache group data fails.</exception>
    /// <exception cref="InvalidOperationException">Thrown when cache group operations fail unexpectedly.</exception>
    /// <remarks>
    /// This method is called internally when a cache group key is specified to manage group-based cache invalidation.
    /// </remarks>
    private async Task addCacheKeyToGroup(TRequest request, TimeSpan slidingExpiration, CancellationToken cancellationToken)
    {
        try
        {
            byte[]? cacheGroupCache = await _cache.GetAsync(key: request.CacheGroupKey!, cancellationToken);
            HashSet<string> cacheKeysInGroup;

            if (cacheGroupCache != null)
            {
                var deserialized = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cacheGroupCache));

                if (deserialized == null)
                {
                    _loggerServiceBase.LogWarning($"Failed to deserialize cache group for key: {request.CacheGroupKey}. Creating new group.");
                    cacheKeysInGroup = new HashSet<string>(new[] { request.CacheKey });
                }
                else
                {
                    cacheKeysInGroup = deserialized;
                    if (!cacheKeysInGroup.Contains(request.CacheKey))
                        cacheKeysInGroup.Add(request.CacheKey);
                }
            }
            else
            {
                cacheKeysInGroup = new HashSet<string>(new[] { request.CacheKey });
            }

            byte[] newCacheGroupCache = JsonSerializer.SerializeToUtf8Bytes(cacheKeysInGroup);

            byte[]? cacheGroupCacheSlidingExpirationCache = await _cache.GetAsync(
                key: $"{request.CacheGroupKey}SlidingExpiration",
                cancellationToken
            );
            int? cacheGroupCacheSlidingExpirationValue = null;
            if (cacheGroupCacheSlidingExpirationCache != null)
                cacheGroupCacheSlidingExpirationValue = Convert.ToInt32(Encoding.Default.GetString(cacheGroupCacheSlidingExpirationCache));
            if (cacheGroupCacheSlidingExpirationValue == null || slidingExpiration.TotalSeconds > cacheGroupCacheSlidingExpirationValue)
                cacheGroupCacheSlidingExpirationValue = Convert.ToInt32(slidingExpiration.TotalSeconds);
            
            byte[] serializeCachedGroupSlidingExpirationData = JsonSerializer.SerializeToUtf8Bytes(cacheGroupCacheSlidingExpirationValue);

            DistributedCacheEntryOptions cacheOptions =
                new() { SlidingExpiration = TimeSpan.FromSeconds(Convert.ToDouble(cacheGroupCacheSlidingExpirationValue)) };

            await _cache.SetAsync(key: request.CacheGroupKey!, newCacheGroupCache, cacheOptions, cancellationToken);
            _loggerServiceBase.LogInformation($"Added to Cache -> {request.CacheGroupKey} (SlidingExpiration: {slidingExpiration.TotalSeconds}s)");

            await _cache.SetAsync(
                key: $"{request.CacheGroupKey}SlidingExpiration",
                serializeCachedGroupSlidingExpirationData,
                cacheOptions,
                cancellationToken
            );
            _loggerServiceBase.LogInformation($"Added to Cache -> {request.CacheGroupKey}SlidingExpiration (SlidingExpiration: {slidingExpiration.TotalSeconds}s)");
        }
        catch (JsonException ex)
        {
            _loggerServiceBase.LogError($"Failed to serialize/deserialize cache group data for key: {request.CacheGroupKey}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Error managing cache group for key: {request.CacheGroupKey}", ex);
            throw new InvalidOperationException($"Failed to manage cache group for key: {request.CacheGroupKey}", ex);
        }
    }
}