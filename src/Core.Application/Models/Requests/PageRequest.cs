using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models.Requests;

/// <summary>
/// Represents a request model for pagination containing the page index and page size.
/// </summary>
public class PageRequest
{
    /// <summary>
    /// Gets or sets the index of the page to be retrieved (1-based index).
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize { get; set; }
}
