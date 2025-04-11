using Core.Infrastructure.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the base repository interface for performing CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
public interface IRepository<TEntity,TEntityId> : IQuery<TEntity>
    where TEntity : Entity<TEntityId>
{

    /// <summary>
    /// Retrieves a single entity matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The filter expression to apply.</param>
    /// <param name="include">The related entities to include in the query.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities.</param>
    /// <param name="enableTracking">Whether to enable change tracking.</param>
    /// <returns>The matching entity, or null if none is found.</returns>
    TEntity? Get(
    Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
    bool withDeleted = false,
    bool enableTracking = true);

    /// <summary>
    /// Retrieves a paginated list of entities matching the specified criteria.
    /// </summary>
    /// <param name="predicate">The filter expression to apply.</param>
    /// <param name="orderBy">The sorting function to apply.</param>
    /// <param name="include">The related entities to include in the query.</param>
    /// <param name="index">The index of the page to retrieve (zero-based).</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities.</param>
    /// <param name="enableTracking">Whether to enable change tracking.</param>
    /// <returns>A paginated list of matching entities.</returns>
    Paginate<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true);

    /// <summary>
    /// Determines whether any entities match the specified predicate.
    /// </summary>
    /// <param name="predicate">The filter expression to apply.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities.</param>
    /// <param name="enableTracking">Whether to enable change tracking.</param>
    /// <returns><c>true</c> if any entities match; otherwise, <c>false</c>.</returns>
    bool Any(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Adds a collection of entities to the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <returns>The added entities.</returns>
    ICollection<TEntity> AddRange(ICollection<TEntity> entities);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The updated entity.</returns>
    TEntity Update(TEntity entity);

    /// <summary>
    /// Updates a collection of entities in the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to update.</param>
    /// <returns>The updated entities.</returns>
    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="permanent">Whether to permanently delete the entity (bypassing soft delete).</param>
    /// <returns>The deleted entity.</returns>
    TEntity Delete(TEntity entity, bool permanent = false);

    /// <summary>
    /// Deletes a collection of entities from the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to delete.</param>
    /// <param name="permanent">Whether to permanently delete the entities (bypassing soft delete).</param>
    /// <returns>The deleted entities.</returns>
    ICollection<TEntity> DeleteRange(ICollection<TEntity> entities, bool permanent = false);
}
