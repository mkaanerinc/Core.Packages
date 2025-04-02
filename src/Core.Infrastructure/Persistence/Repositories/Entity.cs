using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents a base entity with a unique identifier and timestamp properties.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier.</typeparam>
public class Entity<TId> : IEntityTimestamps
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public TId Id { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the entity.
    /// This value is set when the entity is initially created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the entity.
    /// This value is <see langword="null"/> if the entity has never been updated.
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the deletion timestamp of the entity.
    /// This value is <see langword="null"/> if the entity has not been deleted.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// Parameterless constructor required for ORM tools such as Entity Framework Core.
    /// </summary>
    public Entity()
    {
        Id = default!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class with a specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    public Entity(TId id)
    {
        Id = id;
    }
}