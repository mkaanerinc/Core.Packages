using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Repositories;

/// <summary>
/// Defines timestamp properties for entities to track creation, update, and deletion times.
/// </summary>
internal interface IEntityTimestamps
{
    /// <summary>
    /// Gets or sets the creation timestamp of the entity.
    /// This value is set when the entity is initially created.
    /// </summary>
    DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the entity.
    /// This value is <see langword="null"/> if the entity has never been updated.
    /// </summary>
    DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the deletion timestamp of the entity.
    /// This value is <see langword="null"/> if the entity has not been deleted.
    /// </summary>
    DateTimeOffset? DeletedAt { get; set; }
}
