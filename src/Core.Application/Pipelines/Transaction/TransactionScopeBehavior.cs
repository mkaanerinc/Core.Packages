using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Pipelines.Transaction;

/// <summary>
/// Pipeline behavior that wraps the execution of a request in a <see cref="TransactionScope"/>.
/// Only applies to requests implementing <see cref="ITransactionalRequest"/>.
/// </summary>
/// <typeparam name="TRequest">The type of the request. Must implement <see cref="IRequest{TResponse}"/> and <see cref="ITransactionalRequest"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ITransactionalRequest
{
    private readonly ILoggerService _loggerServiceBase;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionScopeBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="loggerServiceBase">The logger service to log transaction status and errors.</param>
    public TransactionScopeBehavior(ILoggerService loggerServiceBase)
    {
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Handles the request within a transaction scope. If the request completes successfully, the transaction is committed.
    /// If an exception occurs, the transaction is rolled back and the exception is logged.
    /// </summary>
    /// <param name="request">The MediatR request to handle.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response from the next handler in the pipeline.</returns>
    /// <exception cref="BusinessException">Thrown when the transaction is aborted due to a <see cref="TransactionAbortedException"/>.</exception>
    /// <exception cref="BusinessException">Thrown when the transaction fails due to any other unhandled exception.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestTypeName = request.GetType().Name;

        var options = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.DefaultTimeout
        };

        using TransactionScope transactionScope = new(
            TransactionScopeOption.Required,
            options,
            TransactionScopeAsyncFlowOption.Enabled);

        TResponse response;

        try
        {
            _loggerServiceBase.LogInformation($"Transaction started for request: {requestTypeName}");

            response = await next();
            transactionScope.Complete();

            _loggerServiceBase.LogInformation($"Transaction completed successfully for request: {requestTypeName}");
        }
        catch (TransactionAbortedException tex)
        {
            _loggerServiceBase.LogError($"Transaction aborted for request: {requestTypeName}", tex);
           
            throw new BusinessException($"Transaction was aborted for request {requestTypeName}", tex);
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Transaction failed for request: {requestTypeName}", ex);

            throw new BusinessException($"Transaction failed for request {requestTypeName}: {ex.Message}", ex);
        }

        return response;
    }
}