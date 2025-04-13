using Core.Infrastructure.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models.Responses;

/// <summary>
/// Represents a generic paginated response model containing a list of items and pagination metadata.
/// </summary>
/// <typeparam name="T">The type of items in the response.</typeparam>
public class GetListResponse<T> : BasePageableModel
{
    /// <summary>
    /// The backing field for the <see cref="Items"/> property.
    /// </summary>
    private IList<T> _items = new List<T>();

    /// <summary>
    /// Gets or sets the list of items in the paginated response.
    /// </summary>
    public IList<T> Items { 
        get => _items;
        set => _items = value; 
    }
}
