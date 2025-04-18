using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions;

/// <summary>
/// Middleware that handles exceptions thrown during the request pipeline 
/// and returns appropriate HTTP responses.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILoggerService _loggerService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next delegate in the HTTP request pipeline.</param>
    /// <param name="contextAccessor">Provides access to the current HTTP context, which includes request and response details.</param>
    /// <param name="loggerService">An instance of the logger service used for logging exception details.</param>
    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor,
        ILoggerService loggerService)
    {
        _next = next;
        _httpExceptionHandler = new HttpExceptionHandler();
        _contextAccessor = contextAccessor;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Invokes the middleware to handle the HTTP request and catch any exceptions.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation of the middleware.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    /// <summary>
    /// Logs the details of an exception to the logging service.
    /// </summary>
    /// <param name="context">The HTTP context that triggered the exception.</param>
    /// <param name="exception">The exception that occurred during processing.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private Task LogException(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter{Type = context.GetType().Name, Value = exception.ToString()}
        };

        LogDetailWithException logDetail = new()
        {
            FullClassName = _next.GetType().FullName ?? "UnknownFullClassName",
            MethodName = _next.Method.Name ?? "UnknownMethod",
            LogParameters = logParameters,
            User = _contextAccessor.HttpContext.User.Identity?.Name ?? "UnknownUser",
            ExceptionMessage = exception.Message
        };

        _loggerService.LogError(JsonSerializer.Serialize(logDetail),exception);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the exception and writes an appropriate HTTP response.
    /// </summary>
    /// <param name="response">The HTTP response to write the error details to.</param>
    /// <param name="exception">The exception that was thrown.</param>
    /// <returns>A task representing the asynchronous operation of writing the response.</returns>
    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}