using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Paging;

/// <summary>
/// A generic class used for pagination operations.
/// </summary>
/// <typeparam name="T">The type of data being paginated.</typeparam>
public class Paginate<T>
{
    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// The current page index (zero-based).
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// The total number of items.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// The total number of pages.
    /// </summary>
    public int Pages { get; set; }

    /// <summary>
    /// The list of items retrieved for the current page.
    /// </summary>
    public IList<T> Items { get; set; }

    /// <summary>
    /// Indicates whether there is a previous page.
    /// </summary>
    public bool HasPrevious => Index > 0;

    /// <summary>
    /// Indicates whether there is a next page.
    /// </summary>
    public bool HasNext => Index + 1 < Pages;

    /// <summary>
    /// Parameterless constructor required for ORM tools such as Entity Framework Core.
    /// The <see cref="Items"/> property is set to <see cref="Array.Empty{T}"/> 
    /// to avoid null references and unnecessary memory allocation.
    /// </summary>
    public Paginate()
    {
        Items = Array.Empty<T>();
    }
}