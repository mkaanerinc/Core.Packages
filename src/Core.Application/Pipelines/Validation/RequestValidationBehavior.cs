using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

namespace Core.Application.Pipelines.Validation;

/// <summary>
/// A pipeline behavior that performs validation on the request using registered validators.
/// Throws a <see cref="ValidationException"/> if validation fails.
/// </summary>
/// <typeparam name="TRequest">The type of the request to validate.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The collection of validators to use for validating the request.</param>
    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Handles the request, performing validation before calling the next handler.
    /// If validation fails, throws a <see cref="ValidationException"/> with the validation errors.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    /// <param name="next">The next delegate to call in the pipeline.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response from the next handler in the pipeline.</returns>
    /// <exception cref="ValidationException">Thrown if validation fails.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new(request);

        IEnumerable<ValidationExceptionModel> errors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .GroupBy(
            keySelector: p => p.PropertyName,
            resultSelector: (propertyName, errors) =>
                new ValidationExceptionModel { Property = propertyName, Errors = errors.Select(e => e.ErrorMessage) }
            ).ToList();

        if (errors.Any())
            throw new ValidationException(errors);
        TResponse response = await next();
        return response;
    }
}
