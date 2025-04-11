using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents a queryable repository interface that provides access to the underlying <see cref="IQueryable{T}"/> data source.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IQuery<T>
{
    /// <summary>
    /// Returns an <see cref="IQueryable{T}"/> that can be used to build LINQ queries against the data source.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> representing the queryable data source.</returns>
    IQueryable<T> Query();
}
