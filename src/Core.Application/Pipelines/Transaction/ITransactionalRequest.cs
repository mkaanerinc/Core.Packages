using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Transaction;

/// <summary>
/// Marker interface to indicate that a MediatR request should be executed within a database transaction.
/// Only requests implementing this interface will be handled by <c>TransactionBehavior</c>.
/// </summary>
/// <remarks>
/// Typically implemented by commands that modify data, such as Create, Update, or Delete operations.
/// </remarks>
public interface ITransactionalRequest
{
}