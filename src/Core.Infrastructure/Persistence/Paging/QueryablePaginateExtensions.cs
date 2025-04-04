using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Paging;

/// <summary>
/// Provides extension methods for paginating IQueryable collections.
/// </summary>
public static class QueryablePaginateExtensions
{
    /// <summary>
    /// Asynchronously paginates an <see cref="IQueryable{T}"/> collection.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="source">The source IQueryable collection.</param>
    /// <param name="index">The page index (zero-based).</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Paginate{T}"/> object containing the paginated data.</returns>
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
           this IQueryable<T> source,
           int index,
           int size,
           CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        List<T> items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

        Paginate<T> list = new()
        {
            Index = index,
            Count = count,
            Items = items,
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size)
        };

        return list;
    }

    /// <summary>
    /// Synchronously paginates an <see cref="IQueryable{T}"/> collection.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="source">The source IQueryable collection.</param>
    /// <param name="index">The page index (zero-based).</param>
    /// <param name="size">The number of items per page.</param>
    /// <returns>A <see cref="Paginate{T}"/> object containing the paginated data.</returns>
    public static Paginate<T> ToPaginate<T>(
        this IQueryable<T> source,
        int index,
        int size)
    {
        int count = source.Count();

        List<T> items = source.Skip(index * size).Take(size).ToList();

        Paginate<T> list = new()
        {
            Index = index,
            Count = count,
            Items = items,
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size)
        };

        return list;
    }
}
