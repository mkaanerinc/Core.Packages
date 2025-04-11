using Core.Infrastructure.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.Repositories;

/// <summary>
/// Base repository class for Entity Framework Core (EFCore) data access operations.
/// Provides basic CRUD operations such as Add, Delete, Update, Get, and GetList,
/// along with support for pagination and soft deletion.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
/// <typeparam name="TContext">The type of the DbContext used for database operations.</typeparam>
public class EFRepositoryBase<TEntity, TEntityId, TContext>
    : IAsyncRepository<TEntity, TEntityId>, IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TContext : DbContext
{

    protected readonly TContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EFRepositoryBase{TEntity, TEntityId, TContext}"/> class.
    /// </summary>
    /// <param name="context">The DbContext used to interact with the database.</param>
    public EFRepositoryBase(TContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new entity to the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task representing the asynchronous operation, with the added entity as the result.</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTimeOffset.UtcNow;
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Adds a collection of entities to the database asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <returns>A task representing the asynchronous operation, with the collection of added entities as the result.</returns>
    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedAt = DateTimeOffset.UtcNow;
        }
        await _context.Set<TEntity>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    /// <summary>
    /// Determines whether any entity matching the given predicate exists in the database.
    /// </summary>
    /// <param name="predicate">The condition to check for existence.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean value indicating whether any matching entity exists.</returns>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return await queryable.AnyAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a given entity from the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="permanent">Whether to permanently delete the entity (true) or perform a soft delete (false).</param>
    /// <returns>A task representing the asynchronous operation, with the deleted entity as the result.</returns>
    public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
    {
        await SetEntityAsDeletedAsync(entity, permanent);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes a collection of entities from the database asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="permanent">Whether to permanently delete the entities (true) or perform a soft delete (false).</param>
    /// <returns>A task representing the asynchronous operation, with the collection of deleted entities as the result.</returns>
    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
    {
        await SetEntityAsDeletedAsync(entities, permanent);
        await _context.SaveChangesAsync();
        return entities;
    }

    /// <summary>
    /// Retrieves a single entity that matches the given predicate from the database asynchronously.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <param name="include">A function to include related entities in the query.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with the entity that matches the predicate, or null if not found.</returns>
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of entities matching the given criteria from the database asynchronously.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <param name="orderBy">A function to order the results.</param>
    /// <param name="include">A function to include related entities in the query.</param>
    /// <param name="index">The index of the page to retrieve.</param>
    /// <param name="size">The number of entities per page.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with the paginated list of entities as the result.</returns>
    public async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, cancellationToken);
    }

    /// <summary>
    /// Updates an existing entity in the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation, with the updated entity as the result.</returns>
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Updates a collection of entities in the database asynchronously.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    /// <returns>A task representing the asynchronous operation, with the collection of updated entities as the result.</returns>
    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTimeOffset.UtcNow;
        }
        _context.Set<TEntity>().UpdateRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    /// <summary>
    /// Retrieves a queryable collection of entities from the database.
    /// </summary>
    /// <returns>An IQueryable of entities.</returns>
    public IQueryable<TEntity> Query()
    {
        return _context.Set<TEntity>();
    }

    /// <summary>
    /// Adds a new entity to the database synchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task representing the synchronous operation, with the added entity as the result.</returns>
    public TEntity Add(TEntity entity)
    {
        entity.CreatedAt = DateTimeOffset.UtcNow;
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Adds a collection of entities to the database synchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <returns>A task representing the synchronous operation, with the collection of added entities as the result.</returns>
    public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedAt = DateTimeOffset.UtcNow;
        }
        _context.Set<TEntity>().AddRange(entities);
        _context.SaveChanges();
        return entities;
    }

    /// <summary>
    /// Determines whether any entity matching the given predicate exists in the database.
    /// </summary>
    /// <param name="predicate">The condition to check for existence.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <returns>A task representing the synchronous operation, with a boolean value indicating whether any matching entity exists.</returns>
    public bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return queryable.Any();
    }

    /// <summary>
    /// Deletes a given entity from the database synchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="permanent">Whether to permanently delete the entity (true) or perform a soft delete (false).</param>
    /// <returns>A task representing the synchronous operation, with the deleted entity as the result.</returns>
    public TEntity Delete(TEntity entity, bool permanent = false)
    {
        SetEntityAsDeleted(entity, permanent);
        _context.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Deletes a collection of entities from the database synchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="permanent">Whether to permanently delete the entities (true) or perform a soft delete (false).</param>
    /// <returns>A task representing the synchronous operation, with the collection of deleted entities as the result.</returns>
    public ICollection<TEntity> DeleteRange(ICollection<TEntity> entities, bool permanent = false)
    {
        SetEntityAsDeleted(entities, permanent);
        _context.SaveChanges();
        return entities;
    }

    /// <summary>
    /// Retrieves a single entity that matches the given predicate from the database synchronously.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <param name="include">A function to include related entities in the query.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <returns>A task representing the synchronous operation, with the entity that matches the predicate, or null if not found.</returns>
    public TEntity? Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        return queryable.FirstOrDefault(predicate);
    }

    /// <summary>
    /// Retrieves a paginated list of entities matching the given criteria from the database synchronously.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <param name="orderBy">A function to order the results.</param>
    /// <param name="include">A function to include related entities in the query.</param>
    /// <param name="index">The index of the page to retrieve.</param>
    /// <param name="size">The number of entities per page.</param>
    /// <param name="withDeleted">Whether to include soft-deleted entities in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <returns>A task representing the synchronous operation, with the paginated list of entities as the result.</returns>
    public Paginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(index, size);
        return queryable.ToPaginate(index, size);
    }

    /// <summary>
    /// Updates an existing entity in the database synchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the synchronous operation, with the updated entity as the result.</returns>
    public TEntity Update(TEntity entity)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Updates a collection of entities in the database synchronously.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    /// <returns>A task representing the synchronous operation, with the collection of updated entities as the result.</returns>
    public ICollection<TEntity> UpdateRange(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTimeOffset.UtcNow;
        }
        _context.Set<TEntity>().UpdateRange(entities);
        _context.SaveChanges();
        return entities;
    }

    /// <summary>
    /// Marks an entity as deleted, either soft deleting or permanently removing it from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="permanent">Indicates whether the deletion is permanent (true) or a soft delete (false).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected async Task SetEntityAsDeletedAsync(TEntity entity, bool permanent)
    {
        if (!permanent)
        {
            CheckHasEntityHaveOneToOneRelation(entity);
            await setEntityAsSoftDeletedAsync(entity);
        }
        else
        {
            _context.Remove(entity);
        }
    }

    /// <summary>
    /// Marks a collection of entities as deleted, either soft deleting or permanently removing them from the database.
    /// </summary>
    /// <param name="entities">The collection of entities to delete.</param>
    /// <param name="permanent">Indicates whether the deletion is permanent (true) or a soft delete (false).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected async Task SetEntityAsDeletedAsync(IEnumerable<TEntity> entities, bool permanent)
    {
        foreach (var entity in entities)
            await SetEntityAsDeletedAsync(entity, permanent);
    }

    /// <summary>
    /// Marks an entity as deleted, either soft deleting or permanently removing it from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="permanent">Indicates whether the deletion is permanent (true) or a soft delete (false).</param>
    protected void SetEntityAsDeleted(TEntity entity, bool permanent)
    {
        if (!permanent)
        {
            CheckHasEntityHaveOneToOneRelation(entity);
            setEntityAsSoftDeleted(entity);
        }
        else
        {
            _context.Remove(entity);
        }
    }

    /// <summary>
    /// Marks a collection of entities as deleted, either soft deleting or permanently removing them from the database.
    /// </summary>
    /// <param name="entities">The collection of entities to delete.</param>
    /// <param name="permanent">Indicates whether the deletion is permanent (true) or a soft delete (false).</param>
    protected void SetEntityAsDeleted(IEnumerable<TEntity> entities, bool permanent)
    {
        foreach (var entity in entities)
            SetEntityAsDeleted(entity, permanent);
    }

    /// <summary>
    /// Checks if an entity has a one-to-one relationship, which can cause issues with soft deletes.
    /// </summary>
    /// <param name="entity">The entity to check for one-to-one relationships.</param>
    /// <exception cref="InvalidOperationException">Thrown when a one-to-one relationship is found, which can cause problems with soft deletes.</exception>
    protected void CheckHasEntityHaveOneToOneRelation(TEntity entity)
    {
        bool hasEntityHaveOneToOneRelation =
            _context
                .Entry(entity)
                .Metadata.GetForeignKeys()
                .All(
                    x =>
                        x.DependentToPrincipal?.IsCollection == true
                        || x.PrincipalToDependent?.IsCollection == true
                        || x.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == entity.GetType()
                ) == false;
        if (hasEntityHaveOneToOneRelation)
            throw new InvalidOperationException(
                "Entity has one-to-one relationship. Soft Delete causes problems if you try to create entry again by same foreign key."
            );
    }

    /// <summary>
    /// Soft deletes an entity by marking it with a deletion timestamp, cascading the delete to related entities if necessary.
    /// </summary>
    /// <param name="entity">The entity to soft delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task setEntityAsSoftDeletedAsync(IEntityTimestamps entity)
    {
        if (entity.DeletedAt.HasValue)
            return;
        entity.DeletedAt = DateTime.UtcNow;

        var navigations = _context
            .Entry(entity)
            .Metadata.GetNavigations()
            .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
            .ToList();
        foreach (INavigation? navigation in navigations)
        {
            if (navigation.TargetEntityType.IsOwned())
                continue;
            if (navigation.PropertyInfo == null)
                continue;

            object? navValue = navigation.PropertyInfo.GetValue(entity);
            if (navigation.IsCollection)
            {
                if (navValue == null)
                {
                    IQueryable query = _context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();
                    navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).ToListAsync();
                    if (navValue == null)
                        continue;
                }

                foreach (IEntityTimestamps navValueItem in (IEnumerable)navValue)
                    await setEntityAsSoftDeletedAsync(navValueItem);
            }
            else
            {
                if (navValue == null)
                {
                    IQueryable query = _context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                    navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType())
                        .FirstOrDefaultAsync();
                    if (navValue == null)
                        continue;
                }

                await setEntityAsSoftDeletedAsync((IEntityTimestamps)navValue);
            }
        }

        _context.Update(entity);
    }

    /// <summary>
    /// Soft deletes an entity by marking it with a deletion timestamp, cascading the delete to related entities if necessary.
    /// </summary>
    /// <param name="entity">The entity to soft delete.</param>
    private void setEntityAsSoftDeleted(IEntityTimestamps entity)
    {
        if (entity.DeletedAt.HasValue)
            return;
        entity.DeletedAt = DateTime.UtcNow;

        var navigations = _context
            .Entry(entity)
            .Metadata.GetNavigations()
            .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
            .ToList();
        foreach (INavigation? navigation in navigations)
        {
            if (navigation.TargetEntityType.IsOwned())
                continue;
            if (navigation.PropertyInfo == null)
                continue;

            object? navValue = navigation.PropertyInfo.GetValue(entity);
            if (navigation.IsCollection)
            {
                if (navValue == null)
                {
                    IQueryable query = _context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();
                    navValue = GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).ToList();
                    if (navValue == null)
                        continue;
                }

                foreach (IEntityTimestamps navValueItem in (IEnumerable)navValue)
                    setEntityAsSoftDeleted(navValueItem);
            }
            else
            {
                if (navValue == null)
                {
                    IQueryable query = _context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                    navValue = GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType())
                        .FirstOrDefault();
                    if (navValue == null)
                        continue;
                }

                setEntityAsSoftDeleted((IEntityTimestamps)navValue);
            }
        }

        _context.Update(entity);
    }

    /// <summary>
    /// Loads related entities based on the specified query and navigation property type, filtering out soft-deleted entities.
    /// </summary>
    /// <param name="query">The query to load related entities.</param>
    /// <param name="navigationPropertyType">The type of the navigation property to load.</param>
    /// <returns>A queryable collection of related entities, excluding soft-deleted ones.</returns>
    protected IQueryable<object> GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
    {
        Type queryProviderType = query.Provider.GetType();
        MethodInfo createQueryMethod =
            queryProviderType
                .GetMethods()
                .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                ?.MakeGenericMethod(navigationPropertyType)
            ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");
        var queryProviderQuery =
            (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: new object[] { query.Expression })!;
        return queryProviderQuery.Where(x => !((IEntityTimestamps)x).DeletedAt.HasValue);
    }
}
