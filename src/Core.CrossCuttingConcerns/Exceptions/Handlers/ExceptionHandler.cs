using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

/// <summary>
/// Provides a base implementation for handling exceptions in a consistent and extensible way.
/// </summary>
public abstract class ExceptionHandler
{
    /// <summary>
    /// Handles the given exception asynchronously by delegating to the appropriate specialized handler.
    /// </summary>
    /// <param name="exception">The exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task HandleExceptionAsync(Exception exception) =>
        exception switch
        {
            ValidationException validationException => HandleException(validationException),
            _ => HandleException(exception)
        };

    /// <summary>
    /// Handles a <see cref="BusinessException"/> asynchronously.
    /// </summary>
    /// <param name="businessException">The validation exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected abstract Task HandleException(BusinessException businessException);

    /// <summary>
    /// Handles a <see cref="ValidationException"/> asynchronously.
    /// </summary>
    /// <param name="validationException">The validation exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected abstract Task HandleException(ValidationException validationException);

    /// <summary>
    /// Handles a <see cref="NotFoundException"/> asynchronously.
    /// </summary>
    /// <param name="notFoundException">The not found exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected abstract Task HandleException(NotFoundException notFoundException);

    /// <summary>
    /// Handles a <see cref="AuthorizationException"/> asynchronously.
    /// </summary>
    /// <param name="authorizationException">The authorization exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected abstract Task HandleException(AuthorizationException authorizationException);

    /// <summary>
    /// Handles a general <see cref="Exception"/> asynchronously.
    /// </summary>
    /// <param name="exception">The exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected abstract Task HandleException(Exception exception);
}
