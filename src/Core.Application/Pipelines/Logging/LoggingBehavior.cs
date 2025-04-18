using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Logging;

/// <summary>
/// MediatR pipeline behavior to log request and response details.
/// </summary>
/// <typeparam name="TRequest">The type of the request being processed.</typeparam>
/// <typeparam name="TResponse">The type of the response being returned.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILoggerService _loggerServiceBase;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor for user identity.</param>
    /// <param name="loggerServiceBase">The logger service to log details.</param>
    public LoggingBehavior(IHttpContextAccessor httpContextAccessor, ILoggerService loggerServiceBase)
    {
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Handles logging the details of a request before proceeding to the next handler.
    /// </summary>
    /// <param name="request">The request object to log.</param>
    /// <param name="next">The next request handler delegate.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The response from the request handler.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter{ Type = request.GetType().Name, Value= request},
        };

        LogDetail logDetail = new()
        {
            FullClassName = request.GetType().FullName ?? "UnknownFullClassName",
            MethodName = next.Method.Name ?? "UnknownMethod",
            LogParameters = logParameters,
            User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "UnknownUser"
        };

        _loggerServiceBase.LogInformation(JsonSerializer.Serialize(logDetail));
        return await next();
    }
}