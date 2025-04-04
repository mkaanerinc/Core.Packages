using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Paging;

/// <summary>
/// Represents the base model for pageable data, providing pagination information.
/// </summary>
public abstract class BasePageableModel
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
    /// Indicates whether there is a previous page.
    /// </summary>
    public bool HasPrevious { get; set; }

    /// <summary>
    /// Indicates whether there is a next page.
    /// </summary>
    public bool HasNext { get; set; }
}
