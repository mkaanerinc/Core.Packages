﻿using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

/// <summary>
/// Handles exceptions and writes standardized HTTP responses using <see cref="HttpResponse"/>.
/// </summary>
public class HttpExceptionHandler : ExceptionHandler
{
    private HttpResponse? _response;

    /// <summary>
    /// Gets or sets the <see cref="HttpResponse"/> object used to write the error response.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if accessed before being set.</exception>
    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }

    /// <summary>
    /// Handles general exceptions by writing an HTTP 500 Internal Server Error response.
    /// </summary>
    /// <param name="exception">The exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation of writing the response.</returns>
    protected override Task HandleException(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        string details = new InternalServerErrorProblemDetails(exception.Message).AsJson();
        return Response.WriteAsync(details);
    }

    /// <summary>
    /// Handles <see cref="ValidationException"/> instances by writing an HTTP 400 Bad Request response.
    /// </summary>
    /// <param name="validationException">The validation exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation of writing the response.</returns>
    protected override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new ValidationProblemDetails(validationException.Errors).AsJson();
        return Response.WriteAsync(details);
    }
}
